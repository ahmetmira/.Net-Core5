using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRTrkrar.Entities.Model
{
    public class Category : BaseEntities
    {
        [Required]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
