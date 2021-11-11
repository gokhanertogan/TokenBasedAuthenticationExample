using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TokenBasedAuthentication.API.Domain.Responses;
using TokenBasedAuthentication.API.Domain.Services;
using TokenBasedAuthentication.API.Resources;
using TokenBasedAuthentication.API.Extensions;
using TokenBasedAuthentication.API.Domain.Model;

namespace TokenBasedAuthentication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            ProductListResponse productListResponse = await _productService.ListAsync();

            if (productListResponse.Success)
            {
                return Ok(productListResponse.ProductList);
            }

            else
            {
                return BadRequest(productListResponse.Message);
            }
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFindById(int id)
        {
            ProductResponse productListResponse = await _productService.FindByIdAsync(id);

            if (productListResponse.Success)
            {
                return Ok(productListResponse.Product);
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

                var productResponse = await _productService.AddProduct(product);

                if (productResponse.Success)
                {
                    return Ok(productResponse.Product);
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

                var productResponse = await _productService.UpdateProduct(product, id);

                if (productResponse.Success)
                {
                    return Ok(productResponse.Product);
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
            var productResponse = await _productService.RemoveProduct(id);

            if (productResponse.Success)
            {
                return Ok(productResponse.Product);
            }
            else
            {
                return BadRequest(productResponse.Message);
            }
        } 

    }
}