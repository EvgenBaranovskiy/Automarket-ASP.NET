
using System.ComponentModel.DataAnnotations;

namespace Automarket.Domain.ViewModel.Account
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Вкажіть логін!")]
        [MaxLength (20, ErrorMessage = "Логін повинен бути до 20 символів довжиною!")]
        [MinLength (3, ErrorMessage = "Логін повинен бути мінімум від 3 символів довжиною!")]
        public string Name { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Вкажіть пароль!")]
        [MinLength(6, ErrorMessage = "Пароль має бути мінімум від 6 символів довжиною!")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Вкажіть пароль повторно!")]
        [Compare("Password", ErrorMessage = "Ви вказали різні паролі!")]
        public string PasswordConfirm { get; set; }
    }
}
