using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TokenBasedAuthentication.API.Domain.Responses;
using TokenBasedAuthentication.API.Domain.Services;
using TokenBasedAuthentication.API.Resources;
using TokenBasedAuthentication.API.Extensions;
using TokenBasedAuthentication.API.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace TokenBasedAuthentication.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IGenericService<Product> _productService;
        private readonly IMapper _mapper;

        public ProductController(IGenericService<Product> productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            BaseResponse<IEnumerable<Product>> productListResponse = await _productService.GetWhere(x=>x.Id>0);

            if (productListResponse.Success)
            {
                return Ok(productListResponse.Model);
            }

            else
            {
                return BadRequest(productListResponse.Message);
            }
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFindById(int id)
        {
            BaseResponse<Product> productListResponse = await _productService.GetById(id);

            if (productListResponse.Success)
            {
                return Ok(productListResponse.Model);
            }

            else
            {
                return BadRequest(productListResponse.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductResource productResource)
        {

            if (ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            else
            {
                var product = _mapper.Map<ProductResource, Product>(productResource);

                var productResponse = await _productService.Add(product);

                if (productResponse.Success)
                {
                    return Ok(productResponse.Model);
                }
                else
                {
                    return BadRequest(productResponse.Message);
                }
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(ProductResource productResource, int id)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            else
            {
                var product = _mapper.Map<ProductResource, Product>(productResource);
                product.Id=id;

                var productResponse = await _productService.Update(product);

                if (productResponse.Success)
                {
                    return Ok(productResponse.Model);
                }
                else
                {
                    return BadRequest(productResponse.Message);
                }
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var productResponse = await _productService.Delete(id);

            if (productResponse.Success)
            {
                return Ok(productResponse.Model);
            }
            else
            {
                return BadRequest(productResponse.Message);
            }
        }

    }
}