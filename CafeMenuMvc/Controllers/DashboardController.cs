﻿using CafeMenuMvc.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CafeMenuMvc.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetCounts()
        {
            var result = await _dashboardService.GetCounts();
            return Json(result);
        }
    }
}