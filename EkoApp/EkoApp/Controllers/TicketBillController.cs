using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
	public class TicketBillController : Controller
	{

		private readonly AppDbContext _appDbContext;
		private readonly SignInManager<UserDb> _signInManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		private readonly string _userId;
		public TicketBillController(AppDbContext appDbContext,
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
			return View(new TicketBill());
		}
		[HttpPost]
		public IActionResult Add(TicketBill ticketBill)
		{
			if (!ModelState.IsValid)
			{
				return View(ticketBill);
			}
			var bill = new TicketBill
			{
				Id = Guid.NewGuid(),
				DateTime = DateTime.Now,
				Price = ticketBill.Price,
				UserDbId = Guid.Parse(_userId)
			};

			_appDbContext.TicketBills.Add(bill);
			_appDbContext.SaveChanges();
			return View();
		}
		//[HttpGet("Edit/{billId}")]
		public IActionResult Edit(string billId)
		{
			if (string.IsNullOrWhiteSpace(billId))
			{
				return NotFound();
			}
			var bill = _appDbContext.TicketBills.FirstOrDefault(x => x.Id == Guid.Parse(billId));

			if (bill == null)
			{
				return NotFound();
			}
			return View(new TicketBillToEdit { Id = bill.Id, Price = bill.Price });
		}
		//[HttpPost("Update/{billId}")]
		[ValidateAntiForgeryToken]
		public IActionResult Update(TicketBillToEdit ticketBillToEdit)
		{
			if (ticketBillToEdit == null)
			{
				return NotFound();
			}
			var currBill = _appDbContext.TicketBills.FirstOrDefault(x => x.Id == ticketBillToEdit.Id);
			currBill.Price = ticketBillToEdit.Price;
			_appDbContext.TicketBills.Update(currBill);
			_appDbContext.SaveChanges();
			return RedirectToAction("AllBills", "Statistic");
		}

		public IActionResult Delete(string billId)
		{
			var id = Guid.Parse(billId);
			if (id != Guid.Empty)
			{
				var bill = _appDbContext.TicketBills.FirstOrDefault(x => x.Id == id);
				_appDbContext.TicketBills.Remove(bill);
				_appDbContext.SaveChanges();
				return RedirectToAction("AllBills", "Statistic");
			}
			else
			{
				return NotFound();
			}
		}
		public IActionResult Get(string billId)
		{
			var id = Guid.Parse(billId);
			if (id != Guid.Empty)
			{
				var billToReturn = _appDbContext.TicketBills.FirstOrDefault(x => x.Id == id);
				return View(billToReturn);
			}
			else
			{
				return NotFound();
			}
		}
	}
}

