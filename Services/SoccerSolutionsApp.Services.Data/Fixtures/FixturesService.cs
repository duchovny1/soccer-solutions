﻿namespace SoccerSolutionsApp.Services.Data.Fixtures
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

        public FixturesService(IDeletableEntityRepository<Fixture> fixturesRepository)
        {
            this.fixturesRepository = fixturesRepository;
        }

        public async Task CreateAsync(ImportFixturesApi model)
        {
            foreach (var fixture in model.Api.Fixtures)
            {

                var league = this.fixturesRepository.All().FirstOrDefaultAsync(x => x.LeagueId == fixture.LeagueId);
                var homeTeam = this.fixturesRepository.All().FirstOrDefaultAsync(x => x.Id == fixture.HomeTeamId);
                var awayTeam = this.fixturesRepository.All().FirstOrDefaultAsync(x => x.Id == fixture.AwayTeamId);

                Status status = fixture.Status == "Match Finished" ? (Status)1 : (Status)0;

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
                        HomeTeamId = fixture.HomeTeamId,
                        AwayTeamId = fixture.AwayTeamId,
                        GoalsHomeTeam = fixture.GoalsHomeTeam,
                        GoalsAwayTeam = fixture.GoalsAwayTeam,
                        Halftime = fixture.Score.HalfTime,
                        Fulltime = fixture.Score.FullTime,
                        Extratime = fixture.Score.Extratime,
                        Penalty = fixture.Score.Penalty,
                    };


                    await this.fixturesRepository.AddAsync(fixtureForDatabase);
                }
            }

            await this.fixturesRepository.SaveChangesAsync();
        }
    }
}
