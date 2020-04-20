using SoccerSolutionsApp.Web.ViewModels.Fixtures;
using System.Collections.Generic;

namespace SoccerSolutionsApp.Web.ViewModels.Teams
{
    public class TeamFixturesViewModel
    {
        public IEnumerable<PastFixturesViewModel> PastFixtures { get; set; }

        public IEnumerable<NextFixturesViewModel> NextFixtures { get; set; }
    }
}
