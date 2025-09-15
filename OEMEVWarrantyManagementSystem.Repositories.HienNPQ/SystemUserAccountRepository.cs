using Microsoft.EntityFrameworkCore;
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
    public class SystemUserAccountRepository : GenericRepository<SystemUserAccount>
    {
        public SystemUserAccountRepository() { }
        public SystemUserAccountRepository(FA25_PRN232_SE1713_G5_OEMEVWarrantyManagementSystemContext context) => _context =context;
        public async Task<SystemUserAccount> GetUserAccount(string username, string password)
        {
            return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password && u.IsActive == true);
        }

    }
}
