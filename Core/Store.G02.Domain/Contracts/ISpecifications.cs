using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Domain.Contracts
{
    public interface ISpecifications<Tkey,TEntity> where TEntity: BaseEntity<Tkey>
    {
        List<Expression<Func<TEntity,object>>> Include { get; set; }
        Expression<Func<TEntity,bool>>? Critetia { get; set; }
        Expression<Func<TEntity, object>>? OrderBy { get; set; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; set; }
        bool IsPagination { get; set; }
        int Take { get; set; }
        int Skip { get; set; }
    }
}
