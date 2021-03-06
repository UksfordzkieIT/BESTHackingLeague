﻿using System;
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
	public class FuelBillController : Controller
	{ 
		private readonly AppDbContext _appDbContext;
		private readonly SignInManager<UserDb> _signInManager;
		private readonly IHttpContextAccessor _httpContextAccessor;

		private readonly string _userId;
		public FuelBillController(AppDbContext appDbContext,
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
			return View(new FuelBillToAdd());
		}
		[HttpPost]
		public IActionResult Add(FuelBillToAdd fuelBill)
		{
			if (!ModelState.IsValid)
			{
				return View(fuelBill);
			}
			try
			{
				var bill = new FuelBill
				{
					Id = Guid.NewGuid(),
					DateTime = DateTime.Now,
					TotalPrice = float.Parse(fuelBill.TotalPrice),
					Volume = float.Parse(fuelBill.Volume),
					UserDbId = Guid.Parse(_userId)
				};
				bill.PricePerLitr = bill.TotalPrice / bill.Volume;
				_appDbContext.FuelBills.Add(bill);
				_appDbContext.SaveChanges();
				return RedirectToAction("AllBills", "Statistic");
			}
			catch(Exception ex)
			{

			}

			return View();
		}
		//[HttpGet("Edit/{billId}")]
		public IActionResult Edit(string billId)
		{
			if (string.IsNullOrWhiteSpace(billId))
			{
				return NotFound();
			}
			var bill = _appDbContext.FuelBills.FirstOrDefault(x => x.Id == Guid.Parse(billId));

			if (bill == null)
			{
				return NotFound();
			}
			return View(new FuelBillToEdit { 
				Id = bill.Id,
				PricePerLitr = bill.PricePerLitr,
				TotalPrice = bill.TotalPrice.ToString(),
				Volume = bill.Volume.ToString()
			});
		}
		//[HttpPost("Update/{billId}")]
		[ValidateAntiForgeryToken]
		public IActionResult Update(FuelBillToEdit fuelBillToEdit)
		{
			if (fuelBillToEdit == null)
			{
				return NotFound();
			}
			try
			{
				var currBill = _appDbContext.FuelBills.FirstOrDefault(x => x.Id == fuelBillToEdit.Id);
				currBill.PricePerLitr = fuelBillToEdit.PricePerLitr;
				currBill.TotalPrice = float.Parse(fuelBillToEdit.TotalPrice);
				currBill.Volume = float.Parse(fuelBillToEdit.Volume);
				_appDbContext.FuelBills.Update(currBill);
				_appDbContext.SaveChanges();
			}
			catch(Exception ex)
			{

			}
			return RedirectToAction("AllBills", "Statistic");
		}

		public IActionResult Delete(string billId)
		{
			var id = Guid.Parse(billId);
			if (id != Guid.Empty)
			{
				var bill = _appDbContext.FuelBills.FirstOrDefault(x => x.Id == id);
				_appDbContext.FuelBills.Remove(bill);
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
				var billToReturn = _appDbContext.FuelBills.FirstOrDefault(x => x.Id == id);
				return View(billToReturn);
			}
			else
			{
				return NotFound();
			}
		}
	}
}
