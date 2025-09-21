using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.ModelExtensions;
using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OEMEVWarrantyManagementSystem.Repositories.HienNPQ.ModelExtensions.SearchRequest;

namespace OEMEVWarrantyManagementSystem.Service.HienNPQ
{
    public interface IBookingHienNpqService
    {
        Task<List<BookingHienNpq>> GetAllAsync();
        Task<BookingHienNpq> GetByIdAsync(int BookingHienNpqid);
        Task<bool> DeleteAsync(int BookingHienNpqid);
        Task<int> CreateAsync(BookingHienNpq booking);
        Task<int> UpdateAsync(BookingHienNpq booking);
        Task<List<BookingHienNpq>> SearchAsync(string StationName, int? BatteryCapacity, string LicensePlate);
        Task<PaginationResult<List<BookingHienNpq>>> SearchWithAsyncPaging(BookingHienNpqSearchRequest? SearchRequest);
    }
}
