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
            int teamLeaguesCount = await this.teamLeaguesRepository.All().CountAsync();

            if (league != null && country != null)
            {
                foreach (var model in models.Api.Teams)
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
                    };

                    TeamLeagues teamsLeague = new TeamLeagues
                    {
                        Id = ++teamLeaguesCount,
                        TeamId = team.Id,
                        LeagueId = leagueId,
                    };

                    await this.teamRepository.AddAsync(team);
                    await this.teamLeaguesRepository.AddAsync(teamsLeague);
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
