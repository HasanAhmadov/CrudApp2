using System.ComponentModel.DataAnnotations;

namespace CrudApp2.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Created Car")]
        public int CarId { get; set; }

        public Car? Car { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Display(Name = "Created User")]
        public int CreatedUserId { get; set; }

        public User? CreatedUser { get; set; }
    }
}
