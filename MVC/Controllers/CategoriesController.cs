using Microsoft.AspNetCore.Mvc;
using Models;

namespace MVC.Controllers
{
	public class CategoriesController : Controller
	{
		private readonly HttpClient _httpClient;

		public CategoriesController(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri("https://your-api-url.com/api/"); // ضع هنا رابط الـ API الأساسي
		}

		public async Task<IActionResult> Index()
		{
			var categories = await _httpClient.GetFromJsonAsync<List<Category>>("categories");
			return View(categories);
		}

		public async Task<IActionResult> Details(int id)
		{
			var category = await _httpClient.GetFromJsonAsync<Category>($"categories/{id}");
			return View(category);
		}
		
	}
}
