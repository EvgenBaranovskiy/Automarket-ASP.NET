using System.ComponentModel.DataAnnotations;

namespace Automarket.Domain.Enum
{
    public enum CarSortType
    {
        [Display(Name = "Сортування за зростанням ціни")]
        SortByPriceAsc,
        [Display(Name = "Сортування за спаданням ціни")]
        SortByPriceDesc,
        [Display(Name = "Сортування за зростанням швидкості")]
        SortBySpeedAsc,
        [Display(Name = "Сортування за спаданням швидкості")]
        SortBySpeedDesc
    }
}
