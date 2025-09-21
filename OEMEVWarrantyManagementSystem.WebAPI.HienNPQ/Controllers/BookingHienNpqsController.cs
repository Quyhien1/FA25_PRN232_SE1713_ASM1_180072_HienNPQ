using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.ModelExtensions;
using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.Models;
using OEMEVWarrantyManagementSystem.Service.HienNPQ;
using static OEMEVWarrantyManagementSystem.Repositories.HienNPQ.ModelExtensions.SearchRequest;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OEMEVWarrantyManagementSystem.WebAPI.HienNPQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingHienNpqsController : ControllerBase
    {
        private readonly IBookingHienNpqService _bookingHienNpqService;
        public BookingHienNpqsController(IBookingHienNpqService bookingHienNpqService)
        {
            _bookingHienNpqService = bookingHienNpqService;
        }
        // GET: api/<BookingHienNpqsController>
        [Authorize(Roles ="1,2")]
        [HttpGet]
        public async Task<IEnumerable<BookingHienNpq>> Get()
        {
            return await _bookingHienNpqService.GetAllAsync();
        }

        // GET api/BookingHienNpqs/Search?StationName=...&BatteryCapacity=...&LicensePlate=...
        [Authorize(Roles = "1,2")]
        [HttpGet("Search")]
        public async Task<IEnumerable<BookingHienNpq>> Search(string? StationName, int? BatteryCapacity, string? LicensePlate)
        {
            // Pass nullable BatteryCapacity directly to avoid .Value when null
            return await _bookingHienNpqService.SearchAsync(StationName, BatteryCapacity, LicensePlate);
        }

        // Change to POST to allow a request body (fetch/axios/etc. cannot send body with GET)
        [Authorize(Roles = "1,2")]
        [HttpPost("SearchWithPaging")]
        public async Task<PaginationResult<List<BookingHienNpq>>> SearchWithPaging([FromBody] BookingHienNpqSearchRequest? bookingHienNpqSearchRequest)
        {
            // Provide safe defaults if client did not send a body
            if (bookingHienNpqSearchRequest == null)
            {
                bookingHienNpqSearchRequest = new BookingHienNpqSearchRequest
                {
                    currentPage = 1,
                    pageSize = 10,
                    StationName = null,
                    BatteryCapacity = null,
                    LicensePlate = null
                };
            }

            return await _bookingHienNpqService.SearchWithAsyncPaging(bookingHienNpqSearchRequest);
        }

        // GET api/<BookingHienNpqsController>/5
        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public async Task<BookingHienNpq> GetByIdAsync(int id)
        {
            return await _bookingHienNpqService.GetByIdAsync(id);
        }

        // POST api/<BookingHienNpqsController>
        [HttpPost]
        public async Task<int> Post(BookingHienNpq bookingHienNpq)
        {
            if (ModelState.IsValid)
                return await _bookingHienNpqService.CreateAsync(bookingHienNpq);
            return 0;
        }

        // PUT api/<BookingHienNpqsController>/5
        [Authorize(Roles = "1,2")]
        [HttpPut]
        public async Task<int> Put(BookingHienNpq bookingHienNpq)
        {
            if (ModelState.IsValid)
                return await _bookingHienNpqService.UpdateAsync(bookingHienNpq);
            return 0;
        }

        // DELETE api/<BookingHienNpqsController>/5
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _bookingHienNpqService.DeleteAsync(id);
        }
    }
}
