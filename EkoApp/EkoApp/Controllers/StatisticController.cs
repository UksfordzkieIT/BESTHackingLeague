using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EkoApp.Data;
using EkoApp.Entities;
using EkoApp.ViewsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EkoApp.Controllers
{
	[Authorize]
	public class StatisticController : Controller
	{
		private readonly AppDbContext _appDbContext;
		private readonly SignInManager<UserDb> _signInManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		private readonly string _userId;
		public StatisticController(AppDbContext appDbContext,
			SignInManager<UserDb> signInManager,
			IHttpContextAccessor httpContextAccessor)
		{
			_appDbContext = appDbContext;
			_signInManager = signInManager;
			_httpContextAccessor = httpContextAccessor;
			_userId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
		}
		public IActionResult ShowRanking()
		{
			return View();
		}
		public IActionResult AllBills()
		{
			var bills = new AllBillsModel();
			bills.WaterBills = _appDbContext.WaterBills.Where(x => x.UserDbId == Guid.Parse(_userId)).ToList();
			bills.FuelBills = _appDbContext.FuelBills.Where(x => x.UserDbId == Guid.Parse(_userId)).ToList();
			bills.TicketBills = _appDbContext.TicketBills.Where(x => x.UserDbId == Guid.Parse(_userId)).ToList();
			return View(bills);
		}
	}
}
