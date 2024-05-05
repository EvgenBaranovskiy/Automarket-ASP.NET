using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Automarket.Domain.Entity
{
	public class CarImage
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
		[Required]
		public string ImgPath { get; set; }
		[ForeignKey("Car")] 
		public int CarId { get; set; }
	}
}
