using Microsoft.EntityFrameworkCore;
using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.Basic;
using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.DBContext;
using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.ModelExtensions;
using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OEMEVWarrantyManagementSystem.Repositories.HienNPQ
{
    public class BookingHienNpqRepository : GenericRepository <BookingHienNpq>
    {
        public BookingHienNpqRepository() { }
        public BookingHienNpqRepository(FA25_PRN232_SE1713_G5_OEMEVWarrantyManagementSystemContext context) => _context = context;
        public async Task<List<BookingHienNpq>> GetAllAsync()
        {
            var item = await _context.BookingHienNpqs.Include(c => c.SupportInfoHienNpq).ToListAsync();
            return item ?? new List<BookingHienNpq>();
        }
        public async Task<BookingHienNpq> GetByIdAsync(int BookingHienNpqid)
        {
            var item = await _context.BookingHienNpqs.Include(c => c.SupportInfoHienNpq).FirstOrDefaultAsync(b => b.BookingHienNpqid == BookingHienNpqid);
            return item ?? new BookingHienNpq();
        }
        public async Task<List<BookingHienNpq>> SearchAsync(string StationName,int? BatteryCapacity,string LicensePlate)
        {
            var item = await _context.BookingHienNpqs.Include(c => c.SupportInfoHienNpq)
                .Where(c =>
                    (c.StationName.Contains(StationName) || string.IsNullOrEmpty(StationName))
                    && (c.BatteryCapacity == BatteryCapacity || BatteryCapacity == 0 || BatteryCapacity == null)
                    && (string.IsNullOrEmpty(LicensePlate) || (c.SupportInfoHienNpq != null && c.SupportInfoHienNpq.LicensePlate.Contains(LicensePlate)))
                ).OrderByDescending(c => c.StartTime)
                .ToListAsync();
            return item ?? new List<BookingHienNpq>();
        }
        public async Task<PaginationResult<List<BookingHienNpq>>> SearchWithAsyncPaging(string StationName, int? BatteryCapacity, string LicensePlate,int currentPage, int pageSize)
        {
            // Await the search task to get the list, then operate on the list synchronously.
            var items = await this.SearchAsync(StationName, BatteryCapacity, LicensePlate);
            var totalItems = items.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var pagedItems = items.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<BookingHienNpq>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                Items = pagedItems
            };
            return result ?? new PaginationResult<List<BookingHienNpq>>();
        }
    }
}
