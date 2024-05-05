using Automarket.Domain.Response;
using Automarket.Domain.ViewModel.Account;
using System.Security.Claims;

namespace Automarket.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<bool>> CreateUser(RegistrationViewModel registrationVM);
        Task<IBaseResponse<ClaimsPrincipal>> Login(LoginViewModel registrationVM);
    }
}
