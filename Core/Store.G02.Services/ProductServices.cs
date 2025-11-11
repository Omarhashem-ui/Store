using AutoMapper;
using Store.G02.Domain.Contracts;
using Store.G02.Domain.Entities.Products;
using Store.G02.Domain.Exceptions.ProductsExceptions;
using Store.G02.Services.Abstractions.Products;
using Store.G02.Services.Specifications.Products;
using Store.G02.Shared.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services
{
    public class ProductServices(IUnitOfWork _unitOfWork,IMapper _mapper) : Abstractions.Products.IProductServices
    {
       

        public async Task<PaginationResponse<ProductResponse>> GetAllProductsAsync(ProductQueryParameters parameters)
        {
           
            var spec = new ProductsWithBrandAndTypesSpecifications(parameters);
            var products = await _unitOfWork.GetRepository<int, Product>().GetAllAsync(spec);
            var result= _mapper.Map<IEnumerable<ProductResponse>>(products);
            var specCount = new ProductPaginationSpecification(parameters);
            var count =await _unitOfWork.GetRepository<int,Product>().CountAsync(specCount);
            return new PaginationResponse<ProductResponse>(parameters.PageIndex,parameters.PageSize,count,result);
        }
        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            var spec = new ProductsWithBrandAndTypesSpecifications(id);
          var product = await _unitOfWork.GetRepository<int,Product>().GetAsync(spec);
            if (product is null) throw new ProductNotFoundException(id);
          var result =  _mapper.Map<ProductResponse>(product);
            return result;
        }
        public async Task<IEnumerable<BrandTypeResponse>> GetAllTypesAsync()
        {
            var Brands = await _unitOfWork.GetRepository<int, ProductBrand>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(Brands);
            return result;
        }
        public  async Task<IEnumerable<BrandTypeResponse>> GetAllBrandsAsync()
        {
            var types = await _unitOfWork.GetRepository<int, ProductType>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<BrandTypeResponse>>(types);
            return result;
        }

    }
}
