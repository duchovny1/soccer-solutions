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
    using SoccerSolutionsApp.Web.ViewModels.Fixtures;
    using SoccerSolutionsApp.Web.ViewModels.Teams;

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

        public int Create(ImportTeamsApi models, int leagueId)
        {
            int totalTeams = 0;

            League league = this.leagueRepository.All().FirstOrDefault(x => x.Id == leagueId);
            Country country = this.countryRepository.All().FirstOrDefault(x => x.Name == models.Api.Teams[0].Country);


            if (league != null && country != null)
            {
                foreach (var model in models.Api.Teams)
                {
                    Team teamToCheck = this.teamRepository.All().FirstOrDefault(x => x.Id == model.TeamId);

                    // if the teams already exists
                    if (teamToCheck != null)
                    {
                        int teamId = teamToCheck.Id;

                        // checking if the current team is being add in the mapping table
                        TeamLeagues teamLeague = this.teamLeaguesRepository
                             .All().FirstOrDefault(x => x.TeamId == teamId && x.LeagueId == leagueId);

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

                            this.teamLeaguesRepository.Add(teamsLeague);
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

                        this.teamRepository.Add(team);
                        this.teamLeaguesRepository.Add(teamsLeague);
                        totalTeams++;
                    }
                }

                this.teamRepository.SaveChanges();
                this.teamLeaguesRepository.SaveChanges();

            }

            return totalTeams;
        }

        public IEnumerable<T> GetAll<T>()
        {
            IQueryable<Team> query = this.teamRepository.All();

            return query.To<T>()
                .ToList();

        }

        public async Task<T> GetTeamByIdAsync<T>(int id)
              => await this.teamRepository.All().Where(x => x.Id == id)
                                          .To<T>().FirstOrDefaultAsync();
    }
}
