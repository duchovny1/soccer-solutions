namespace SoccerSolutionsApp.Services.Data.Predictions
{
    using System.Collections.Generic;

    public interface IPredictionsService
    {
        IEnumerable<T> GetAll<T>();
    }
}
