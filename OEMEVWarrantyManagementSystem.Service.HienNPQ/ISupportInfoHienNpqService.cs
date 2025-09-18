using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OEMEVWarrantyManagementSystem.Service.HienNPQ
{
    public interface ISupportInfoHienNpqService
    {
        Task<List<SupportInfoHienNpq>> GetAllAsync();
    }
}
