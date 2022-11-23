using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repository.Pattern.Specifications
{
    public sealed class OrSpecification<T> : BaseSpecification<T>
    {
        private readonly BaseSpecification<T> _left;

        private readonly BaseSpecification<T> _right;

        public OrSpecification(BaseSpecification<T> left, BaseSpecification<T> right)
        {
            this._right = right;
            this._left = left;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = this._left.ToExpression();
            var rightExpression = this._right.ToExpression();

            var paramExpression = Expression.Parameter(typeof(T));
            var expressionBody = Expression.OrElse(leftExpression.Body, rightExpression.Body);
            expressionBody = (BinaryExpression)new ParameterReplacer(paramExpression).Visit(expressionBody);
            var finalExpression = Expression.Lambda<Func<T, bool>>(expressionBody ?? throw new InvalidOperationException($"{nameof(expressionBody)} cannot be null."), paramExpression);

            return finalExpression;
        }
    }
}
