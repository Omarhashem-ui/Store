using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G02.Presentation.Attrubites;
using Store.G02.Services.Abstractions;
using Store.G02.Shared.ErrorModels;
using Store.G02.Shared.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentation
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IServicesManager _servicesManager;
        public ProductsController(IServicesManager servicesManager)
        {
            _servicesManager = servicesManager;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,Type =typeof(PaginationResponse<ProductResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type =typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError,Type =typeof(ErrorDetails))]
        [Cache(120)]
        public async Task<ActionResult<PaginationResponse<ProductResponse>>> GetAllProducts([FromQuery]ProductQueryParameters parameters)
        {
           var result = await _servicesManager.ProductServices.GetAllProductsAsync(parameters);
            if (result is null) BadRequest();
            return Ok(result);
        }
        [HttpGet ("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<ProductResponse>> GetProduct(int? id)
        {
            if(id == null) return BadRequest();
           var result = await _servicesManager.ProductServices.GetProductByIdAsync(id.Value);
           
            return Ok(result);
        }
        [HttpGet("brands")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BrandTypeResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<BrandTypeResponse>> GetAllBrands()
        {
           var result = await _servicesManager.ProductServices.GetAllBrandsAsync();
            if (result is null) BadRequest();
            return Ok(result);
        }
        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BrandTypeResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<BrandTypeResponse>> GetAllTypes()
        {
           var result = await _servicesManager.ProductServices.GetAllTypesAsync();
            if (result is null) BadRequest();
            return Ok(result);
        }
    }
}
