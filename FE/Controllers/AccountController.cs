using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace FE.Controllers
{
	public class AccountController : Controller
	{
		private readonly HttpClient _httpClient;

		public AccountController()
		{
			_httpClient = new HttpClient();

		}
			[HttpPost]
		public async Task<IActionResult> GetUser(string Taikhoan,string Matkhau)
		{
			var url = $"http://localhost:58573/api/Users?Taikhoan={Taikhoan}";
			if (string.IsNullOrEmpty(Taikhoan) || string.IsNullOrEmpty(Matkhau))
			{
				 TempData["EROR"] ="Tài khoản hoặc mật khẩu không được để trống.";
				return RedirectToAction("Login");
			}
			
			try
			{
				var loginUser = new User { Taikhoan = Taikhoan, Matkhau = Matkhau };
				var content = new StringContent(JsonSerializer.Serialize(loginUser), Encoding.UTF8, "application/json");

				var response = await _httpClient.PostAsync(url,content);

				response.EnsureSuccessStatusCode();

				if (!response.IsSuccessStatusCode)
				{
					return StatusCode((int)response.StatusCode, $"API lỗi: {response.ReasonPhrase}");
				}
				// Đọc dữ liệu từ API
				var jsonData = await response.Content.ReadAsStringAsync();

				var user = JsonSerializer.Deserialize<User>(jsonData, new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true 
				});
				if (user != null) {
					if (user.Matkhau.Equals(Matkhau) && user.Taikhoan.Equals(Taikhoan))
					{
						//return RedirectToAction("Information", "Account");
						return View("Information", user);

					}
					else
					{
						 TempData["EROR"] = "Sai mật khẩu" ;
						return RedirectToAction("Login");
					}
					

				}
				else
				{
					TempData["EROR"] = " Không tồn tại tài khoản người dùng";
					return RedirectToAction("Login");
				}
				
			}
			catch (HttpRequestException ex)
			{
				return StatusCode(500, $"Lỗi khi gọi API: {ex.Message}");
			}
		}
		public IActionResult Login()
		{
			return View();
		}
		public IActionResult Information(User user)
		{

			return View(user);
		}
	}
}
