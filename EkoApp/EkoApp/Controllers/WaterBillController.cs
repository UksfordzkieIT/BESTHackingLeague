using System;
using System.Linq;
using System.Security.Claims;
using EkoApp.Data;
using EkoApp.Entities;
using EkoApp.Models;
using EkoApp.ViewsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EkoApp.Controllers
{
	[Authorize]
	public class WaterBillController : Controller
	{
		private readonly AppDbContext _appDbContext;
		private readonly SignInManager<UserDb> _signInManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly string _userId;
		public WaterBillController(AppDbContext appDbContext,
			SignInManager<UserDb> signInManager,
			IHttpContextAccessor httpContextAccessor)
		{
			_appDbContext = appDbContext;
			_signInManager = signInManager;
			_httpContextAccessor = httpContextAccessor;
			_userId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult Add()
		{
			return View(new WaterBill());
		}
		[HttpPost]
		public IActionResult Add(WaterBill waterBill)
		{
			if (!ModelState.IsValid)
			{
				return View(waterBill);
			}
			var bill = new WaterBill
			{
				Id = Guid.NewGuid(),
				DateTime = DateTime.Now,
				Price = waterBill.Price,
				Volume = waterBill.Volume,
				UserDbId = Guid.Parse(_userId)
			};

			_appDbContext.WaterBills.Add(bill);
			_appDbContext.SaveChanges();
			return View();
		}
		[HttpGet("Edit/{billId}")]
		public IActionResult Edit(string billId)
		{
			if (string.IsNullOrWhiteSpace(billId))
			{
				return NotFound();
			}
			var bill = _appDbContext.WaterBills.FirstOrDefault(x => x.Id == Guid.Parse(billId));

			if (bill == null)
			{
				return NotFound();
			}
			return View(new WaterBillToEdit { Id = bill.Id, Price = bill.Price, Volume = bill.Volume });
		}
		[HttpPost("Update/{billId}")]
		[ValidateAntiForgeryToken]
		public IActionResult Update(WaterBillToEdit waterBillToEdit)
		{
			if (waterBillToEdit == null)
			{
				return NotFound();
			}
			var currBill = _appDbContext.WaterBills.FirstOrDefault(x => x.Id == waterBillToEdit.Id);
			currBill.Volume = waterBillToEdit.Volume;
			currBill.Price = waterBillToEdit.Price;
			_appDbContext.WaterBills.Update(currBill);
			_appDbContext.SaveChanges();
			return RedirectToAction(nameof(Index), "Profile");
		}

		public IActionResult Delete()
		{
			return View();
		}
		[HttpGet("{billId}")]
		public IActionResult Get(string billId)
		{
			var id = Guid.Parse(billId);
			var billToReturn = _appDbContext.WaterBills.FirstOrDefault(x => x.Id == id);

			return View(billToReturn);
		}
	}
}
