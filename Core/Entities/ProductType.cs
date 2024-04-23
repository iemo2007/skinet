using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    public class ProductType: BaseEntity
    {
        public ProductType()
        {
            Products = new List<Product>();
        }
        public string Name { get; set; }

        [JsonIgnore]
        public List<Product> Products { get; set; }
    }
}