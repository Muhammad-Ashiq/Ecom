using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Ecom.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(222)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
