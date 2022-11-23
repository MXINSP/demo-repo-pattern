using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repository.Pattern.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double UnitPrice { get; set; }
    }
}
