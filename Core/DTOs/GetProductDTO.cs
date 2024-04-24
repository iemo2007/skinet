using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public  class GetProductDTO: BaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }


        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }

        public int ProductBrandId { get; set; }
        public string ProductBrandName { get; set; }
    }
}
