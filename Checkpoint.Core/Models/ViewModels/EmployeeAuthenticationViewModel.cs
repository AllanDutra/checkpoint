namespace Checkpoint.Core.Models.ViewModels
{
    public class EmployeeAuthenticationViewModel
    {
        public EmployeeAuthenticationViewModel(
            EmployeeClaimsViewModel employeeData,
            string jwtToken
        )
        {
            EmployeeData = employeeData;
            JwtToken = jwtToken;
        }

        public EmployeeClaimsViewModel EmployeeData { get; }
        public string JwtToken { get; }
    }
}
