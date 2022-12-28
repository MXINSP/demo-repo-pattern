using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repository.Pattern.Domain
{
    public class Order : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; } = DateTime.Today;

        public List<Product> LineItems { get; set; } = new();
    }
}