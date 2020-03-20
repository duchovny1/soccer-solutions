namespace SoccerSolutionsApp.Web.Areas.Administration.Controllers
{
    using SoccerSolutionsApp.Common;
    using SoccerSolutionsApp.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
