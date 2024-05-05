using Automarket.Domain.ViewModel.Account;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Automarket.Controllers
{
    public class AccountController : Controller
    {
        IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login(bool afterReg = false, string name = "")
        {
            if (HttpContext.User.Identity?.IsAuthenticated == true)
            {
				return RedirectToAction("GetPage", "Car");
			}

            return View(new LoginViewModel { ShowMessageSuccessRegistration = afterReg, Name = name });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (HttpContext.User.Identity?.IsAuthenticated == true)
            {
				return RedirectToAction("GetPage", "Car");
			}

            var response = await _userService.Login(loginVM);
            
            switch (response.StatusCode)
            {
                case Domain.Enum.StatusCode.OK:
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, response.Data);
					return RedirectToAction("GetPage", "Car");
				case Domain.Enum.StatusCode.InvalidPassword:
                    ModelState.AddModelError("InvalidPassword", response.Description);
                    break;
                case Domain.Enum.StatusCode.InvalidUsername:
                    ModelState.AddModelError("InvalidUsername", response.Description);
                    break;
                case Domain.Enum.StatusCode.InternalServerError:
                    ModelState.AddModelError("InternalServerError", response.Description);
                    break;
            }
            
            return View(loginVM);
        }

        [HttpGet]
        public IActionResult Registration()
        {
            if (HttpContext.User.Identity?.IsAuthenticated == true)
            {
				return RedirectToAction("GetPage", "Car");
			}

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel registrationVM)
        {
            if (HttpContext.User.Identity?.IsAuthenticated == true)
            {
				return RedirectToAction("GetPage", "Car");
			}

            if (ModelState.IsValid)
            {
                var response = await _userService.CreateUser(registrationVM);

                switch (response.StatusCode)
                {
                    case Domain.Enum.StatusCode.OK:
                        return RedirectToAction(nameof(Login), new { afterReg = true, name = registrationVM.Name });
                    case Domain.Enum.StatusCode.InvalidPassword:
                        ModelState.AddModelError("InvalidPassword", response.Description);
                        break;
                    case Domain.Enum.StatusCode.UsernameIsNotUnique:
                        ModelState.AddModelError("UsernameIsNotUnique", response.Description);
                        break;
                    case Domain.Enum.StatusCode.InternalServerError:
                        ModelState.AddModelError("InternalServerError", response.Description);
                        break;

                }
            }

            return View();
        }


    }
}
