using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EkoApp.Controllers
{
	public class CalendarController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult GetCurrentMonth()
		{
			return View();
		}
		public IActionResult GetNextMonth()
		{
			return View();
		}
		public IActionResult GetPreviousMonth()
		{
			return View();
		}
		public IActionResult GetDay()
		{
			return View();
		}

	}
}
