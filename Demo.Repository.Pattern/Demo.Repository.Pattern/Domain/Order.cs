using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repository.Pattern.Domain
{
    public class Order : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public DateTime OrderDate { get; set; }

        public List<Product> LineItems { get; set; } = new();
    }
}
