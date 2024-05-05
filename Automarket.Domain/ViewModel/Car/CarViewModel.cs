using Automarket.Domain.Entity;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Automarket.Domain.ViewModel.Car
{
    public class CarViewModel
    {
        public const string CAR_NO_AVATAR = "/img/CarPreviewAvatars/no_avatar.png";
        public int Id { get; set; }
        [Required(ErrorMessage = "Не вказано значення для {0}")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Довжина {0} має бути від 1 до 50 символів")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не вказано значення для {0}")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Довжина {0} має бути від 1 до 50 символів")]
        public string Model { get; set; }
		[Required(ErrorMessage = "Не вказано значення для {0}")]
		[StringLength(250, ErrorMessage = "Довжина {0} має бути до {1} символів")]
        public string? ShortDescription { get; set; }
        [StringLength(750, ErrorMessage = "Довжина {0} має бути до {1} символів")]
        public string? FullDescription { get; set; }
        [StringLength(100, ErrorMessage = "Довжина {0} має бути до {1} символів")]
        public string? AvatarImgPath { get; set; }
        public List<CarImage>? SliderPathImages { get; set; }
        public IFormFileCollection? SliderImages { get; set; }
        public IFormFile? AvatarImage { get; set; }
        [Required(ErrorMessage = "Не вказано значення для {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Значення {0} має бути більше 0.")]
        public int Price { get; set; }
        [Required(ErrorMessage = "Не вказано значення для {0}")]
        [Range(1, int.MaxValue, ErrorMessage = "Значення {0} має бути більше 0.")]
        public double Speed { get; set; }
        public DateTime DateCreate { get; set; }
        public string TypeCar { get; set; }

        public CarViewModel()
        {
            AvatarImgPath = CAR_NO_AVATAR;
        }
        public CarViewModel(Entity.Car car)
        {
            Id = car.Id;
            Name = car.Name;
            Model = car.Model;
            ShortDescription = car.ShortDescription;
            FullDescription = car.FullDescription;
            AvatarImgPath = String.IsNullOrEmpty(car.AvatarImgUrl) ? CAR_NO_AVATAR : car.AvatarImgUrl;
            SliderPathImages = car.Images;
            Speed = car.Speed;
            Price = (int)car.Price;
            DateCreate = DateTime.Now;
            TypeCar = car.TypeCar.ToString();
        }
	}
}