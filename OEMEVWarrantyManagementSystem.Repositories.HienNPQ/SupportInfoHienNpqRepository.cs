using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.Basic;
using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.DBContext;
using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OEMEVWarrantyManagementSystem.Repositories.HienNPQ
{
    public class SupportInfoHienNpqRepository : GenericRepository<SupportInfoHienNpq>
    {
        public SupportInfoHienNpqRepository() { }
        public SupportInfoHienNpqRepository(FA25_PRN232_SE1713_G5_OEMEVWarrantyManagementSystemContext context) => _context = context;
        
    }
}
