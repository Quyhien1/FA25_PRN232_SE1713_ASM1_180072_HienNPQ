using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.DBContext;
using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.Models;
using OEMEVWarrantyManagementSystem.Service.HienNPQ;
using System.Threading.Tasks;

namespace OEMEVWarrantyManagementSystem.MVCWebApp.HienNPQ.Controllers
{
    [Authorize] // Adjust roles if needed: [Authorize(Roles="1,2")]
    public class BookingHienNpqsController : Controller
    {
        private readonly FA25_PRN232_SE1713_G5_OEMEVWarrantyManagementSystemContext _context;
        private readonly IBookingHienNpqService _service;

        public BookingHienNpqsController(
            FA25_PRN232_SE1713_G5_OEMEVWarrantyManagementSystemContext context,
            IBookingHienNpqService service)
        {
            _context = context;
            _service = service;
        }

        // GET: /BookingHienNpqs
        public async Task<IActionResult> Index()
        {
            var list = await _context.BookingHienNpqs
                .Include(b => b.SupportInfoHienNpq)
                .AsNoTracking()
                .ToListAsync();
            return View(list);
        }

        // GET: /BookingHienNpqs/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.BookingHienNpqs
                .Include(b => b.SupportInfoHienNpq)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookingHienNpqid == id);

            if (item == null) return NotFound();
            return View(item);
        }

        // GET: /BookingHienNpqs/Create
        public async Task<IActionResult> Create()
        {
            await LoadSupportInfosAsync();
            return View(new BookingHienNpq { Status = "Pending", StartTime = DateTime.UtcNow });
        }

        // POST: /BookingHienNpqs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingHienNpq model)
        {
            if (!ModelState.IsValid)
            {
                await LoadSupportInfosAsync(model.SupportInfoHienNpqid);
                return View(model);
            }
            await _service.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: /BookingHienNpqs/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _context.BookingHienNpqs
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookingHienNpqid == id);
            if (booking == null) return NotFound();

            await LoadSupportInfosAsync(booking.SupportInfoHienNpqid);
            return View(booking);
        }

        // POST: /BookingHienNpqs/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookingHienNpq model)
        {
                if (!ModelState.IsValid)
            {
                await LoadSupportInfosAsync(model.SupportInfoHienNpqid);
                return View(model);
            }

            // Since global NoTracking is enabled, just update the detached model
            _context.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: /BookingHienNpqs/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.BookingHienNpqs
                .Include(b => b.SupportInfoHienNpq)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BookingHienNpqid == id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: /BookingHienNpqs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadSupportInfosAsync(int? selectedId = null)
        {
            var infos = await _context.SupportInfoHienNpqs
                .AsNoTracking()
                .OrderBy(s => s.LicensePlate)
                .ToListAsync();

            ViewBag.SupportInfos = new SelectList(infos, "SupportInfoHienNpqid", "LicensePlate", selectedId);
        }
    }
}
