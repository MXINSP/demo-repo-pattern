using Demo.Repository.Pattern.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repository.Pattern.Specifications
{
    internal class ExpensiveProductSpec : BaseSpecification<Product>
    {
        public override Expression<Func<Product, bool>> ToExpression()
        {
            return product => product.UnitPrice > 100;
        }
    }
}
