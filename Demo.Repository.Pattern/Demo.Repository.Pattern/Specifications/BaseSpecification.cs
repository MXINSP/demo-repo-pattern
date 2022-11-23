using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repository.Pattern.Specifications
{
    public abstract class BaseSpecification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy(T entity)
        {
            var predicate = this.ToExpression().Compile();
            return predicate(entity);
        }

        public BaseSpecification<T> And(BaseSpecification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }

        public BaseSpecification<T> Or(BaseSpecification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }
    }
}
