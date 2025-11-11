using Store.G02.Domain;
using Store.G02.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services
{
    public class BaseSpecifications<TKey, TEntity> : ISpecifications<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        public List<Expression<Func<TEntity, object>>> Include { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, bool>>? Critetia { get ; set ; }
        public Expression<Func<TEntity, object>>? OrderBy { get ; set ; }
        public Expression<Func<TEntity, object>>? OrderByDescending { get ; set ; }
        public bool IsPagination { get; set ; }
        public int Take { get; set; } = 5;
        public int Skip { get; set; } = 1;

        public BaseSpecifications(Expression<Func<TEntity, bool>>? expression)
        {
            Critetia= expression;
        }
        public void ApplyPaginations(int pageSize, int pageIndex)
        {
            IsPagination=true;
            Take= pageSize;
            Skip= (pageIndex-1)*pageSize;
        }
    }
}
