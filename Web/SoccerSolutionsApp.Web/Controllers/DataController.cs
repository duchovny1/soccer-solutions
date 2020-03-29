namespace SoccerSolutionsApp.Web.Controllers
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using SoccerSolutionsApp.Data;
  

    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public DataController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet("getleagues")]
        public ActionResult<SelectList> GetLeagues(int countryId)
        {
            var leagues = new SelectList(this.db.Leagues.Where(c => c.CountryId == countryId).ToList(), "Id", "Name");

            return leagues;
        }

    }
}