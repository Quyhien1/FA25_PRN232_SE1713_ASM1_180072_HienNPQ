using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OEMEVWarrantyManagementSystem.Repositories.HienNPQ.ModelExtensions
{
    public class SearchRequest
    {
        public int? currentPage { get; set; }
        public int? pageSize { get; set; }
        public class BookingHienNpqSearchRequest : SearchRequest
        {
            public string? StationName { get; set; }
            public int? BatteryCapacity { get; set; }
            public string? LicensePlate { get; set; }
        }
    }
}
