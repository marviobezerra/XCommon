using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace XCommon.Test.Web.Site.Controllers
{
	[Route("api/v1/[controller]")]
    public class AccountController : Controller
	{
		[HttpGet]
		public List<int> Get()
		{
			return new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		}

		[HttpGet("a")]
		[Authorize]
		public bool GetA()
		{
			return true;
		}
	}
}
