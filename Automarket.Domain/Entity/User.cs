using Automarket.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Automarket.Domain.Entity {
public class User {
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public UserRole Role { get; set; }
}
}
