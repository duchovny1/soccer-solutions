namespace SoccerSolutionsApp.Services.Data.Fixtures
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data.Common.Repositories;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Data.Models.Enums;

    public class FixturesService : IFixturesService
    {
        private readonly IDeletableEntityRepository<Fixture> fixturesRepository;
        private readonly IDeletableEntityRepository<League> leaguesRepository;
        private readonly IDeletableEntityRepository<Team> teamsRepository;

        public FixturesService(
            IDeletableEntityRepository<Fixture> fixturesRepository,
            IDeletableEntityRepository<League> leaguesRepository,
            IDeletableEntityRepository<Team> teamsRepository)
        {
            this.fixturesRepository = fixturesRepository;
            this.leaguesRepository = leaguesRepository;
            this.teamsRepository = teamsRepository;
        }

        public async Task CreateAsync(ImportFixturesApi model)
        {
            foreach (var fixture in model.Api.Fixtures)
            {
                var isExists = await this.fixturesRepository.All().AnyAsync(x => x.Id == fixture.FixtureId);

                if (isExists)
                {
                    continue;
                }

                int? homeTeamId = fixture.HomeTeam.TeamId;
                int? awayTeamId = fixture.AwayTeam.TeamId;
                int? goalsHomeTeam = fixture.GoalsHomeTeam;
                int? goalsAwayTeam = fixture.GoalsAwayTeam;

                if (homeTeamId == null || awayTeamId == null || goalsHomeTeam == null || goalsAwayTeam == null)
                {
                    continue;
                }

                var league = await this.leaguesRepository.All().FirstOrDefaultAsync(x => x.Id == fixture.LeagueId);
                var homeTeam = await this.teamsRepository.All().FirstOrDefaultAsync(x => x.Id == homeTeamId);
                var awayTeam = await this.teamsRepository.All().FirstOrDefaultAsync(x => x.Id == awayTeamId);

                Status status = fixture.Status == "Match Finished" ? (Status)1 : 0;

                if (league != null && homeTeam != null && awayTeam != null)
                {
                    Fixture fixtureForDatabase = new Fixture()
                    {
                        Id = fixture.FixtureId,
                        Status = status,
                        KickOff = fixture.EventDate,
                        Round = fixture.Round,
                        StatusShort = fixture.StatusShort,
                        Elapsed = fixture.Elapsed,
                        Venue = fixture.Venue,
                        Referee = fixture.Referee,
                        HomeTeamId = (int)homeTeamId,
                        AwayTeamId = (int)awayTeamId,
                        GoalsHomeTeam = (int)goalsHomeTeam,
                        GoalsAwayTeam = (int)goalsAwayTeam,
                        Halftime = fixture.Score.HalfTime,
                        Fulltime = fixture.Score.FullTime,
                        Extratime = fixture.Score.Extratime,
                        Penalty = fixture.Score.Penalty,
                        LeagueId = league.Id,
                    };

                    await this.fixturesRepository.AddAsync(fixtureForDatabase);
                }
            }

            await this.fixturesRepository.SaveChangesAsync();
        }
    }
}
