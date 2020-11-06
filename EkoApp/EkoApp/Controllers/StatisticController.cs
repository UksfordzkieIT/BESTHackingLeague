using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EkoApp.Controllers
{
	public class StatisticController : Controller
	{
		public IActionResult ShowRanking()
		{
			return View();
		}
	}
}
