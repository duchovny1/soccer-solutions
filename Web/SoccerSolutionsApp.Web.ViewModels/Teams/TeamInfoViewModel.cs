namespace SoccerSolutionsApp.Web.ViewModels.Teams
{
    using System.Collections.Generic;

    using SoccerSolutionsApp.Data.Models;
    using SoccerSolutionsApp.Services.Mapping;
    using SoccerSolutionsApp.Web.ViewModels.Fixtures;

    public class TeamInfoViewModel : IMapFrom<Team>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public int Founded { get; set; }

        public string VenueName { get; set; }

        public string VenueCapacity { get; set; }

        public IEnumerable<PastFixturesViewModel> PastFixtures { get; set; }

        public IEnumerable<NextFixturesViewModel> NextFixtures { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }
    }
}
