using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EkoApp.Models;
using Microsoft.AspNetCore.Identity;
using EkoApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using EkoApp.Data;
using EkoApp.ViewsModel;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace EkoApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly SignInManager<UserDb> _signInManager;
		private readonly UserManager<UserDb> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly string _userId;
		private readonly AppDbContext _appDbContext;
		private readonly IConfiguration _config;

		public AccountController(SignInManager<UserDb> signInManager,
			UserManager<UserDb> userManager,
			IHttpContextAccessor httpContextAccessor,
			AppDbContext appDbContext,
			IConfiguration config)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			if (User != null)
			{
				_httpContextAccessor = httpContextAccessor;
				_userId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
			}
			_appDbContext = appDbContext;
			_config = config;
		}
		[HttpGet]
		[Authorize]
		public IActionResult Home()
		{
			return View();
		}
		[HttpGet]
		public IActionResult Login(string returnUrl)
		{
			return View(new LoginViewModel { ReturnUrl = returnUrl });
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(loginViewModel.Login, loginViewModel.Password, false, false);
				if (result.Succeeded)
				{
					var x = User.Claims.FirstOrDefault(x => x.Type == "LastName");
					return Redirect(loginViewModel.ReturnUrl);
				}
			}
			return View(new LoginViewModel { ReturnUrl = loginViewModel.ReturnUrl });
		}

		[HttpGet]
		public IActionResult Register(string returnUrl)
		{
			return View(new RegisterViewModel { ReturnUrl = returnUrl });
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
		{

			if (!ModelState.IsValid)
			{
				return View(registerViewModel);
			}
			var user = new UserDb
			{
				Id = Guid.NewGuid(),
				UserName = registerViewModel.Login,
				Email = registerViewModel.Email,
				FirstName = registerViewModel.FirstName,
				LastName = registerViewModel.LastName
			};

			var result = await _userManager.CreateAsync(user, registerViewModel.Password);

			return RedirectToAction("Login");
		}
		public IActionResult LogOut()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		[HttpGet("/Image/{image}")]
		public IActionResult Image(string image)
		{
			string mime = image.Substring(image.LastIndexOf('.') + 1);

			var _imagePath = _config["Path:Images"];
			var x =  new FileStream(Path.Combine(_imagePath, image), FileMode.Open, FileAccess.Read);

			return new FileStreamResult(x, $"images/{mime}");	
		}

	}
}
