using OEMEVWarrantyManagementSystem.Repositories.HienNPQ;
using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.ModelExtensions;
using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OEMEVWarrantyManagementSystem.Service.HienNPQ
{
    public class BookingHienNpqService : IBookingHienNpqService
    {
        private readonly BookingHienNpqRepository _repository;
        public BookingHienNpqService() => _repository = new BookingHienNpqRepository();
        public async Task<int> CreateAsync(BookingHienNpq booking)
        {
            try
            {
                var result = await _repository.CreateAsync(booking);
                return result;
            }
            catch (Exception ex)
            {
            }
            return 0;
        }

        public async Task<bool> DeleteAsync(int BookingHienNpqid)
        {
            try
            {
                var item = await _repository.GetByIdAsync(BookingHienNpqid);
                if (item != null)
                {
                    var result = await _repository.RemoveAsync(item);
                    return result;
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public async Task<List<BookingHienNpq>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                // return new List<BookingHienNpq>();
                throw new Exception(ex.Message);
            }
        }

        public async Task<BookingHienNpq> GetByIdAsync(int BookingHienNpqid)
        {
            try
            {
                return await _repository.GetByIdAsync(BookingHienNpqid);
            }
            catch (Exception ex)
            {
                return new BookingHienNpq();
            }
        }

        public async Task<List<BookingHienNpq>> SearchAsync(string StationName, int? BatteryCapacity, string LicensePlate)
        {
            try
            {
                return await _repository.SearchAsync(StationName, BatteryCapacity, LicensePlate);
            }
            catch (Exception ex)
            {
                return new List<BookingHienNpq>();
            }
        }

        public async Task<PaginationResult<List<BookingHienNpq>>> SearchWithAsyncPaging(SearchRequest.BookingHienNpqSearchRequest? SearchRequest)
        {
            try
            {
                return await _repository.SearchWithAsyncPaging(SearchRequest.StationName, SearchRequest.BatteryCapacity.Value, SearchRequest.LicensePlate, SearchRequest.currentPage.Value, SearchRequest.pageSize.Value);
            }
            catch (Exception ex)
            {
                return new PaginationResult<List<BookingHienNpq>>();
            }
        }

        public async Task<int> UpdateAsync(BookingHienNpq booking)
        {
            try
            {
                var result = await _repository.UpdateAsync(booking);
                return result;
            }
            catch (Exception ex)
            {
            }
            return 0;
        }
    }
}
