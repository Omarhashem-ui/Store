using Store.G02.Domain.Entities.Products;
using Store.G02.Shared.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Specifications.Products
{
    public class ProductsWithBrandAndTypesSpecifications : BaseSpecifications<int,Product>
    {
        public ProductsWithBrandAndTypesSpecifications(int id) :base(P=>P.Id==id)
        {
            ApplyIncludes();

        }
        public ProductsWithBrandAndTypesSpecifications(ProductQueryParameters parameters) :base
            (
              P=>(
              (!parameters.BrandId.HasValue||P.BrandId==parameters.BrandId)
              &&
              (!parameters.TypeId.HasValue || P.TypeId == parameters.TypeId)
              &&
              (string.IsNullOrEmpty(parameters.Search)||P.Name.ToLower().Contains(parameters.Search.ToLower()))
              )

            )
        {
               ApplySorting(parameters.Sorting);
               ApplyIncludes();
            ApplyPaginations(parameters.PageSize, parameters.PageIndex);
        }
        private void ApplySorting(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "priceasc":
                        OrderBy = P => P.Price;
                        break;
                    case "pricedesc":
                        OrderByDescending = P => P.Price;
                        break;
                    default:
                        OrderBy = P => P.Name;
                        break;
                }
            }
            else
            {
                OrderBy = P => P.Name;
            }
        }
        private void ApplyIncludes()
        {
            Include.Add(P => P.Brand);
            Include.Add(P => P.Type);
        }
    }
}
