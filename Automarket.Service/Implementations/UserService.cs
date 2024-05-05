using Automarket.DAL.Interfaces;
using Automarket.Domain.Response;
using Automarket.Domain.Entity;
using Automarket.Domain.ViewModel.Account;
using Automarket.Service.Interfaces;
using Automarket.Domain.Enum;
using System.Security.Claims;

namespace Automarket.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository) { this.userRepository = userRepository; }
        public async Task<IBaseResponse<bool>> CreateUser(RegistrationViewModel registrationVM)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var newUser = new User
                {
                    Name = registrationVM.Name,
                    Password = registrationVM.Password,
                    Role = UserRole.Buyer
                };

                //Перевірка на унікальність логіну
                if (!(await userRepository.IsUsernameUniqueAsync(newUser.Name)))
                {
                    response.Data = false;
                    response.Description = "Користувач з таким логіном вже існує!";
                    response.StatusCode = StatusCode.UsernameIsNotUnique;
                }
                //Перевірка на збіжність та довжину паролів
                else if (registrationVM.Password.Length < 6 || !registrationVM.Password.Equals(registrationVM.PasswordConfirm))
                {
                    response.Data = false;
                    response.Description = "Паролі мають бути однаковими та не менше 6 символів!";
                    response.StatusCode = StatusCode.InvalidPassword;
                }
                else
                {
                    //Все ок
                    response.Data = await userRepository.CreateAsync(newUser) != 0;
                    response.Description = "Користувач успішно зареєстрований!";
                    response.StatusCode = StatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                response.Data = false;
                response.Description = $"[CreateUser] {ex.Message}";
                response.StatusCode = StatusCode.InternalServerError;
            }

            return response;
        }
        public async Task<IBaseResponse<ClaimsPrincipal>> Login(LoginViewModel loginVM)
        {
            var response = new BaseResponse<ClaimsPrincipal>();
            try
            {
                //Перевіркв існування користувача за логіном
                var user = await userRepository.GetUserByNameAsync(loginVM.Name);
                if (user == null)
                {
                    response.Description = "Користувача за таким логіном не знайдено.";
                    response.StatusCode = StatusCode.InvalidUsername;
                    return response;
                }

                //Перевірка валідності пароля
                if (!loginVM.Password.Equals(user.Password))
                {
                    response.Description = "Невірно вказаний пароль!";
                    response.StatusCode = StatusCode.InvalidPassword;
                    return response;
                }

                //Авторизація ОК
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, 
                                                             authenticationType: "Cockie", 
                                                             ClaimsIdentity.DefaultNameClaimType, 
                                                             ClaimsIdentity.DefaultRoleClaimType);
                response.Description = "Авторизація успішна!";
                response.StatusCode = StatusCode.OK;
                response.Data = new ClaimsPrincipal(identity);
                return response;
            }
            catch (Exception ex)
            {
                response.Description = $"[Login] {ex.Message}";
                response.StatusCode = StatusCode.InternalServerError;
                return response;
            }
        }
    }
}
