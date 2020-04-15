namespace SoccerSolutionsApp.Services.Data.TeamsServices
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data.Common.Repositories;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Data.Teams;
    using SoccerSolutionsApp.Services.Mapping;

    public class TeamsService : ITeamsService
    {
        private readonly IDeletableEntityRepository<Team> teamRepository;
        private readonly IDeletableEntityRepository<League> leagueRepository;
        private readonly IDeletableEntityRepository<Country> countryRepository;
        private readonly IDeletableEntityRepository<TeamLeagues> teamLeaguesRepository;


        public TeamsService(
            IDeletableEntityRepository<Team> teamRepository,
            IDeletableEntityRepository<League> leagueRepository,
            IDeletableEntityRepository<Country> countryRepository,
            IDeletableEntityRepository<TeamLeagues> teamLeaguesRepository)
        {
            this.teamRepository = teamRepository;
            this.leagueRepository = leagueRepository;
            this.countryRepository = countryRepository;
            this.teamLeaguesRepository = teamLeaguesRepository;
        }

        public async Task CreateAsync(ImportTeamsApi models, int leagueId)
        {
            League league = await this.leagueRepository.All().FirstOrDefaultAsync(x => x.Id == leagueId);
            Country country = await this.countryRepository.All().FirstOrDefaultAsync(x => x.Name == models.Api.Teams[0].Country);


            if (league != null && country != null)
            {
                foreach (var model in models.Api.Teams)
                {
                    Team teamToCheck = await this.teamRepository.All().FirstOrDefaultAsync(x => x.Id == model.TeamId);

                    // if the teams already exists
                    if (teamToCheck != null)
                    {
                        int teamId = teamToCheck.Id;

                        // checking if the current team is being add in the mapping table
                        TeamLeagues teamLeague = await this.teamLeaguesRepository
                             .All().FirstOrDefaultAsync(x => x.TeamId == teamId && x.LeagueId == leagueId);

                        // if its being added, doin nothing
                        if (teamLeague != null)
                        {
                            continue;
                        }

                        // if its not, create new mapping table
                        else
                        {
                            TeamLeagues teamsLeague = new TeamLeagues
                            {
                                TeamId = teamId,
                                LeagueId = leagueId,
                            };

                            await this.teamLeaguesRepository.AddAsync(teamsLeague);
                        }
                    }
                    // if the team does not exists
                    else
                    {
                        Team team = new Team()
                        {
                            Id = model.TeamId,
                            Name = model.Name,
                            Code = model.Code,
                            Logo = model.Logo,
                            IsNational = model.IsNational,
                            Founded = model.Founded,
                            VenueName = model.VenueName,
                            VenueCapacity = model.VenueCapacity,
                            CountryId = country.Id,
                        };

                        TeamLeagues teamsLeague = new TeamLeagues
                        {
                            TeamId = team.Id,
                            LeagueId = leagueId,
                        };

                        await this.teamRepository.AddAsync(team);
                        await this.teamLeaguesRepository.AddAsync(teamsLeague);
                    }
                }

                await this.teamRepository.SaveChangesAsync();
                await this.teamLeaguesRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Team> query = this.teamRepository.All();

            return query.To<T>()
                .ToList();
            //return query.Tйo<T>.ToList();
        }
    }
}
