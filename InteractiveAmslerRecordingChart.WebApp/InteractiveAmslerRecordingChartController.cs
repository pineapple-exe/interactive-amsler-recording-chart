using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InteractiveAmslerRecordingChart.WebApp
{
    public class InteractiveAmslerRecordingChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
