using System.ComponentModel.DataAnnotations;

namespace Ecom.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5), MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }
    }
}
