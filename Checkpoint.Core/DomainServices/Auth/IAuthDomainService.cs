using System.Security.Claims;
using Checkpoint.Core.Models.ViewModels;

namespace Checkpoint.Core.DomainServices.Auth
{
    public interface IAuthDomainService
    {
        string GenerateJwtToken(EmployeeClaimsViewModel employeeClaims);
        EmployeeClaimsViewModel ReadUserClaims(IEnumerable<Claim> userClaims);
        string GenerateEmailConfirmationCode();
    }
}
