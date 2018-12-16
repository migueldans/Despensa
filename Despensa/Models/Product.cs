using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Despensa.Models
{
    public class Product
    {
        [Key]
        public string Product_Name { get; set; }//TODO si lo vuelvesa a hacer hazte un id de [key]
        public string Name_Product { get; set; }
        public string Image_front_url { get; set; }
        [JsonProperty(PropertyName = "nova_group")]
        public int NovaGroup { get; set; }
        public despensa despensa { get; set; }

    }
}
