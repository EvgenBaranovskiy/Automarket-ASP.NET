using Automarket.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Automarket.Domain.Entity
{
    public class Car
    {
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Model { get; set; }
		[Required]
		public string ShortDescription { get; set; }
        public string? FullDescription { get; set; }
        public string? AvatarImgUrl { get; set; }
		[Required]
		public double Speed { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public DateTime DateCreate { get; set; }
		[Required]
		public TypeCar TypeCar { get; set; }
		public List<CarImage> Images { get; set; }
	}
}