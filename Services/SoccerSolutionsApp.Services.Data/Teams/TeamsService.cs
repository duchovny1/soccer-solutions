namespace SoccerSolutionsApp.Services.Data.TeamsServices
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data.Common.Repositories;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Data.Teams;
    using SoccerSolutionsApp.Services.Mapping;
    using SoccerSolutionsApp.Web.ViewModels.Fixtures;
    using SoccerSolutionsApp.Web.ViewModels.Teams;

    public class TeamsService : ITeamsService, ITeamsStatisticsService
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


        public string FilePath { get; } = Path.Combine(Environment.CurrentDirectory + "\\" + "TeamsExceptions.txt");

        public void AddStatistics()
        {
            throw new NotImplementedException();
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
                    try
                    {
                        Team teamToCheck = this.teamRepository.All().FirstOrDefault(x => x.Id == model.TeamId);

                        // if the team already exists
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
                    catch (Exception ex)
                    {
                        StringBuilder sb = new StringBuilder();
                        using StreamWriter sw = new StreamWriter(this.FilePath, true);

                        sb.AppendLine($"An exception occured at {DateTime.UtcNow}");
                        sb.AppendLine($"There is some problem with deserializating teams from json");
                        sb.AppendLine($"Original exception message: ");
                        sb.AppendLine(ex.Message);

                        if (ex.InnerException != null)
                        {
                            sb.AppendLine(ex.InnerException.ToString());
                        }

                        sb.AppendLine("Team Object: ");
                        sb.AppendLine(model.ToString());
                        sb.AppendLine("End of exception here.");
                        sb.AppendLine("--------------");
                        sb.AppendLine("---------------");
                        sw.Write(sb.ToString());
                        sw.Close();
                    }
                }

                this.teamRepository.SaveChanges();
                this.teamLeaguesRepository.SaveChanges();
            }

            return totalTeams;
        }

        public async Task<IEnumerable<T>> GetAll<T>()
         => await this.teamRepository.All().To<T>().ToListAsync();

        public async Task<T> GetTeamByIdAsync<T>(int id)
              => await this.teamRepository.All().Where(x => x.Id == id)
                                          .To<T>().FirstOrDefaultAsync();
    }
}
