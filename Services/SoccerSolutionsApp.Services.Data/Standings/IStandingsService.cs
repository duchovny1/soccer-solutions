using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerSolutionsApp.Services.Data.Standings
{
    public interface IStandingsService
    {
        int Create(ImportStandingsApi models);
    }
}
