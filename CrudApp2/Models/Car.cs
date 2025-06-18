using System.ComponentModel.DataAnnotations;

namespace CrudApp2.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Car Brand")]
        public string Brand { get; set; } = string.Empty;
    }
}
