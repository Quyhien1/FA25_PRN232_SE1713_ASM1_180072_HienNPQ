using OEMEVWarrantyManagementSystem.Repositories.HienNPQ;
using OEMEVWarrantyManagementSystem.Repositories.HienNPQ.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace OEMEVWarrantyManagementSystem.Service.HienNPQ
{
    public class SystemUserAccountService
    {
        private readonly SystemUserAccountRepository _repository;
        private readonly ILogger<SystemUserAccountService> _logger;

        public SystemUserAccountService(SystemUserAccountRepository repository,
                                        ILogger<SystemUserAccountService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task<SystemUserAccount> GetUserAccount(string username, string password)
        {
            // (Optional) Add normalization: username = username.Trim();
            return _repository.GetUserAccount(username, password);
        }
    }
}
