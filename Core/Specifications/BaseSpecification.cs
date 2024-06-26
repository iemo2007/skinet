﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
            this.Includes = new List<Expression<Func<T, object>>>();
        }

        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            this.Criteria = criteria;
            this.Includes = new List<Expression<Func<T, object>>>();
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        { 
            this.Includes.Add(includeExpression);
        }
    }
}
