using Microsoft.AspNetCore.Mvc;

namespace FE.Controllers
{
	public class BlogController : Controller
	{
		public IActionResult Blog()
		{
			return View();
		}
	}
}
