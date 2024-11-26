using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace FE.Controllers
{
	public class InformationController : Controller
	{
		private readonly HttpClient httpClient;

		public InformationController()
		{
			httpClient = new HttpClient();

		}
		public IActionResult Information()
		{

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> GetUserLogin(string Taikhoan)
		{
			if (string.IsNullOrEmpty(Taikhoan))
			{
				return BadRequest("Tài khoản không được để trống.");
			}

			var url = $"http://localhost:58573/api/Users?Taikhoan={Taikhoan}";

			try
			{
				using (var client = new HttpClient())
				{
					// Gửi yêu cầu GET tới API
					var response = await client.GetAsync(url);

					if (response.IsSuccessStatusCode)
					{
						// Đọc dữ liệu trả về từ API
						var json = await response.Content.ReadAsStringAsync();
						var user = JsonConvert.DeserializeObject<User>(json); // Chuyển đổi JSON thành object

						// Truyền thông tin người dùng vào View
						return View("UserDetails", user);
					}
					else
					{
						return StatusCode((int)response.StatusCode, "Không tìm thấy người dùng.");
					}
				}
			}
			catch (HttpRequestException ex)
			{
				return StatusCode(500, $"Lỗi khi gọi API: {ex.Message}");
			}
		}

	}
}
