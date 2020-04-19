﻿namespace SoccerSolutionsApp.Services.Data.Fixtures
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SoccerSolutionsApp.Data.Common.Repositories;
    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Data.Models.Enums;
    using SoccerSolutionsApp.Services.Mapping;
    using SoccerSolutionsApp.Web.ViewModels.Fixtures;
    using SoccerSolutionsApp.Web.ViewModels.Main;

    public class FixturesService : IFixturesService
    {
        private readonly IDeletableEntityRepository<Fixture> fixturesRepository;
        private readonly IDeletableEntityRepository<League> leaguesRepository;
        private readonly IDeletableEntityRepository<Team> teamsRepository;
        private readonly IDeletableEntityRepository<TeamLeagues> teamLeaguesRepository;

        public FixturesService(
            IDeletableEntityRepository<Fixture> fixturesRepository,
            IDeletableEntityRepository<League> leaguesRepository,
            IDeletableEntityRepository<Team> teamsRepository,
            IDeletableEntityRepository<TeamLeagues> teamLeaguesRepository)
        {
            this.fixturesRepository = fixturesRepository;
            this.leaguesRepository = leaguesRepository;
            this.teamsRepository = teamsRepository;
            this.teamLeaguesRepository = teamLeaguesRepository;
        }

        public async Task CreateAsync(ImportFixturesApi model)
        {
            foreach (var fixture in model.Api.Fixtures)
            {
                var isExists = this.fixturesRepository.All().Any(x => x.Id == fixture.FixtureId);

                if (isExists)
                {
                    continue;
                }

                int? homeTeamId = fixture.HomeTeam.TeamId;
                int? awayTeamId = fixture.AwayTeam.TeamId;
                int? goalsHomeTeam = fixture.GoalsHomeTeam;
                int? goalsAwayTeam = fixture.GoalsAwayTeam;

                if (homeTeamId == null || awayTeamId == null)
                {
                    continue;
                }

                var league = this.leaguesRepository.All().FirstOrDefault(x => x.Id == fixture.LeagueId);
                var homeTeam = this.teamsRepository.All().FirstOrDefault(x => x.Id == homeTeamId);
                var awayTeam = this.teamsRepository.All().FirstOrDefault(x => x.Id == awayTeamId);

                Status status = fixture.Status switch
                {
                    "Match Finished" => Status.MatchFinished,
                    "Not Started" => Status.NotStarted,
                    "Match Postponed" => Status.Postponed,
                    _ => throw new System.NotImplementedException(),
                };

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
                        GoalsHomeTeam = goalsHomeTeam,
                        GoalsAwayTeam = goalsAwayTeam,
                        Halftime = fixture.Score.HalfTime,
                        Fulltime = fixture.Score.FullTime,
                        Extratime = fixture.Score.Extratime,
                        Penalty = fixture.Score.Penalty,
                        LeagueId = league.Id,
                    };

                    if (status == Status.MatchFinished && goalsHomeTeam != null && goalsAwayTeam != null)
                    {
                        FullTimeExit fullTimeExit;
                        int? result = goalsHomeTeam - goalsAwayTeam;
                        if (result > 0)
                        {
                            fullTimeExit = FullTimeExit.HomeWin;
                        }
                        else if (result < 0)
                        {
                            fullTimeExit = FullTimeExit.AwayWin;
                        }
                        else
                        {
                            fullTimeExit = FullTimeExit.Draw;
                        }

                        fixtureForDatabase.FullTimeExit = fullTimeExit;
                    }

                    await this.fixturesRepository.AddAsync(fixtureForDatabase);
                }
            }

            await this.fixturesRepository.SaveChangesAsync();
        }

        public IEnumerable<FixtureViewModel> GetFixturesByDate(FixturesByDateInputModel model)
        {
            var fixtures = this.fixturesRepository.All().Where(x => x.KickOff == model.Date);

            return fixtures.To<FixtureViewModel>().ToList();
        }

        public async Task<IEnumerable<FixturesListingViewModel>> GetNextFixturesByIdAsync(int leagueId)
        {
            // im taking next game in the league and take its game week(round)
            // then im taking all fixtures that are in the same game week(round)
            Fixture nextGame = await this.fixturesRepository.All()
                              .FirstOrDefaultAsync(x => x.LeagueId == leagueId && x.KickOff > DateTime.UtcNow);

            if (nextGame == null)
            {
                return null;
            }

            string round = nextGame.Round;

            IQueryable<Fixture> nextFixtures = this.fixturesRepository.All().Where(x => x.LeagueId == leagueId && x.Round == round);

            return await nextFixtures.To<FixturesListingViewModel>().ToListAsync();
        }

        public async Task<IEnumerable<PastFixturesViewModel>> GetPastFixturesForTeamById(int teamId, int take)
            => await this.fixturesRepository.All().Where(x => x.Status == Status.MatchFinished)
                .OrderByDescending(x => x.KickOff)
                .Take(take)
                .To<PastFixturesViewModel>()
                .ToListAsync();

        public async Task<IEnumerable<NextFixturesViewModel>> GetNexTFixturesForTeamByIdAsync(int teamId, int take)
            => await this.fixturesRepository.All().Where(x => x.Status != Status.MatchFinished)
                .OrderBy(x => x.KickOff)
                .Take(take)
                .To<NextFixturesViewModel>()
                .ToListAsync();

    }
}
