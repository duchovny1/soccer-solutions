namespace SoccerSolutionsApp.Services.Predictions
{
    using System.Collections.Generic;

    public interface IPredictionsService
    {
        IEnumerable<T> GetAll<T>();
    }
}
