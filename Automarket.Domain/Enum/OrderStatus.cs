using System.ComponentModel.DataAnnotations;

namespace Automarket.Domain.Enum
{
    public enum OrderStatus
    {
        [Display(Name = "На опрацюванні")]
        InProcessing,
        [Display(Name = "Обробленно")]
        Proccessed,
        [Display(Name = "Відхилено")]
        Declined
    }
}
