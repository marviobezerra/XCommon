using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using XCommon.Application.Authentication;
using XCommon.Application.Executes;
using XCommon.Patterns.Ioc;

namespace XCommon.Test.Web.Site.Controllers
{
	[Route("api/v1/[controller]")]
	public class AccountController : Controller
	{
		public AccountController()
		{
			Kernel.Resolve(this);
		}

		[Inject]
		private ITicketManager TicketManager { get; set; }

		[HttpGet("login")]
		public async Task LoginAsync()
		{
			HttpContext.Request.PathBase = new PathString("");
			await HttpContext.ChallengeAsync("Google", new AuthenticationProperties() {  RedirectUri = "/" });
		}

		[HttpGet]
		public List<int> Get()
		{
			return new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		}


		[HttpGet("ok/{key}")]
		[AllowAnonymous]
		public IActionResult Sim(string key)
		{
			return Ok(true);
		}

		[HttpGet("a")]
		[Authorize]
		public ExecuteUser GetA()
		{
			return TicketManager.User;
		}
	}
}
