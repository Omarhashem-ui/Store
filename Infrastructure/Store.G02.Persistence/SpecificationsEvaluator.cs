using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Store.G02.Domain;
using Store.G02.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence
{
    public static class SpecificationsEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TKey, TEntity>(IQueryable<TEntity> inputQuery, ISpecifications<TKey, TEntity> spec) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery; //_context.Set<TEntity>()

            if (spec.Critetia is not null)
            {
                query = query.Where(spec.Critetia);
            }
            if(spec.OrderBy is not null)
            {
                query= query.OrderBy(spec.OrderBy);
            }
            else if(spec.OrderByDescending is not null)
            {
                query=query.OrderByDescending(spec.OrderByDescending);
            }
            if (spec.IsPagination)
            {
                query=query.Skip(spec.Skip).Take(spec.Take);
            }


                query = spec.Include.Aggregate(query, (query, includeExpression) => query.Include(includeExpression));
            return query;

        }
    }
}
