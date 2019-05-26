using System.ComponentModel.DataAnnotations;

namespace Ecom.Models
{
    public class Config
    {
        [Key]
        public string Key { get; set; }

        public string Value { get; set; }
    }
}
