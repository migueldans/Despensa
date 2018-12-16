using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Despensa.Models
{
    public class SearchProduct
    {
        [Key]
        public int Page_size { get; set; }
        public int Page { get; set; }
        public int Count { get; set; }
        public int Skip { get; set; }
        public List<Product> Products { get; set; }
    }
}
