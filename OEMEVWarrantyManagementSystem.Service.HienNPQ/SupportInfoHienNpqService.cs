using OEMEVWarrantyManagementSystem.Repositories.HienNPQ;
using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OEMEVWarrantyManagementSystem.Service.HienNPQ
{
    public class SupportInfoHienNpqService : ISupportInfoHienNpqService
    {
        private readonly SupportInfoHienNpqRepository _repository;
        public SupportInfoHienNpqService() => _repository = new SupportInfoHienNpqRepository();
        public async Task<List<SupportInfoHienNpq>>GetAllAsync()
        {
                return await _repository.GetAllAsync();
        }
    }
}
