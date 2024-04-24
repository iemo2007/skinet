using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
        {
            IQueryable<T> query = inputQuery;
            
            // If there is a criteria
            if (spec.Criteria != null) 
            {
                query = query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate(query, (entity, include) => entity.Include(include));

            return query;
        }
    }
}
