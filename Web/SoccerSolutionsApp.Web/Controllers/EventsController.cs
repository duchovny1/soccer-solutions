﻿namespace SoccerSolutionsApp.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class EventsController : Controller
    {
        public IActionResult Add()
        {
            return View();
        }
    }
}