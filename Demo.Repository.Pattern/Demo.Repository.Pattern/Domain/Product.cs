using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repository.Pattern.Domain
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public double UnitPrice { get; set; }
    }
}
