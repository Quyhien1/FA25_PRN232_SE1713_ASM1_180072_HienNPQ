namespace OEMEVWarrantyManagementSystem.MVCWebApp.HienNPQ.Models
{
    public class LoginRequest
    {
        /*
         UserName = {userName, email, phone, employeeCode}
         */
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
