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
    public class SystemUserAccountRepository
    {
        private readonly FA25_PRN232_SE1713_G5_OEMEVWarrantyManagementSystemContext _context;

        public SystemUserAccountRepository(FA25_PRN232_SE1713_G5_OEMEVWarrantyManagementSystemContext context)
        {
            _context = context;
        }

        public Task<SystemUserAccount> GetUserAccount(string username, string password)
        {
            return _context.SystemUserAccounts
                .FirstOrDefaultAsync(u => u.UserName == username
                                          && u.Password == password
                                          && u.IsActive);
        }
    }
}
