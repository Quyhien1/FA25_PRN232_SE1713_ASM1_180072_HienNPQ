using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace OEMEVWarrantyManagementSystem.MVCWebApp.HienNPQ.Controllers
{
    [Authorize]
    public class BookingHienNpqsController : Controller
    {
        private readonly HttpClient _apiClient;
        private readonly ILogger<BookingHienNpqsController> _logger;

        public BookingHienNpqsController(IHttpClientFactory httpClientFactory, ILogger<BookingHienNpqsController> logger)
        {
            _apiClient = httpClientFactory.CreateClient("ApiClient");
            _logger = logger;
        }

        // Initial page shell (data loaded via AJAX)
        public IActionResult Index()
        {
            return View();
        }

        private bool EnsureToken()
        {
            var token = HttpContext.Session.GetString("JwtToken");
            if (string.IsNullOrWhiteSpace(token)) return false;
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
        }

        // AJAX endpoint that proxies to Web API using server-stored JWT
        [HttpGet]
        public async Task<IActionResult> Data()
        {
            if (!EnsureToken()) return Unauthorized();
            try
            {
                var bookings = await _apiClient.GetFromJsonAsync<List<BookingHienNpq>>("BookingHienNpqs");
                return Json(new { success = true, data = bookings });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching bookings via API");
                return Json(new { success = false, error = "Failed to load data" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            if (!EnsureToken()) return Unauthorized();
            try
            {
                var booking = await _apiClient.GetFromJsonAsync<BookingHienNpq>($"BookingHienNpqs/{id}");
                if (booking == null) return NotFound();
                return Json(new { success = true, data = booking });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching booking {Id}", id);
                return Json(new { success = false, error = "Failed to load booking" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingHienNpq model)
        {
            if (!EnsureToken()) return Unauthorized();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var resp = await _apiClient.PostAsJsonAsync("BookingHienNpqs", model);
                if (!resp.IsSuccessStatusCode)
                    return Json(new { success = false, error = "API create failed" });
                var newId = await resp.Content.ReadFromJsonAsync<int>();
                return Json(new { success = true, id = newId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating booking");
                return Json(new { success = false, error = "Create failed" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] BookingHienNpq model)
        {
            if (!EnsureToken()) return Unauthorized();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var resp = await _apiClient.PutAsJsonAsync("BookingHienNpqs", model);
                if (!resp.IsSuccessStatusCode)
                    return Json(new { success = false, error = "API update failed" });
                var result = await resp.Content.ReadFromJsonAsync<int>();
                return Json(new { success = true, affected = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating booking {Id}", model.BookingHienNpqid);
                return Json(new { success = false, error = "Update failed" });
            }
        }
    }
}
