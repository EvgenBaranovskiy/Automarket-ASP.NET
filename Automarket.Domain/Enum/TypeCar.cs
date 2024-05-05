using System.ComponentModel.DataAnnotations;

namespace Automarket.Domain.Enum
{
    public enum TypeCar
    {
        [Display(Name = "Легковий автомобіль")]
        PassengerCar = 0,
        [Display(Name = "Седан")]
        Sedan = 1,
        [Display(Name = "Мінівєн")]
        Minivan = 2,
        [Display(Name = "Спортивний автомобіль")]
        SportCar = 3,
        [Display(Name = "Позашляховик")]
        Suv = 4,
    }
}
