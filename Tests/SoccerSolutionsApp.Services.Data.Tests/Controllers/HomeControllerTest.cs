namespace SoccerSolutionsApp.Services.Data.Tests.Controllers
{
    using MyTested.AspNetCore.Mvc;
    using SoccerSolutionsApp.Web.Controllers;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnStatusCode200AndDefaultView()
        {
            MyController<HomeController>
                .Calling(x => x.Index(1))
                .ShouldHave()
                .HttpResponse(x => x.WithStatusCode(200))
                .AndAlso()
                .ShouldReturn()
                .View();
        }
    }
}
