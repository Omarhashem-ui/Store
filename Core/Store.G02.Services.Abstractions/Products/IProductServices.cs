using Store.G02.Shared.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Abstractions.Products
{
    public interface IProductServices
    {
        Task<PaginationResponse<ProductResponse>> GetAllProductsAsync(ProductQueryParameters parameters);
        Task<ProductResponse> GetProductByIdAsync(int id);
       Task<IEnumerable<BrandTypeResponse>>  GetAllTypesAsync();
       Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync();
    }
}
