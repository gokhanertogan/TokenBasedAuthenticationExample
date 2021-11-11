using System;
using System.Threading.Tasks;
using TokenBasedAuthentication.API.Domain.Model;
using TokenBasedAuthentication.API.Domain.Repositories;
using TokenBasedAuthentication.API.Domain.Responses;
using TokenBasedAuthentication.API.Domain.Services;
using TokenBasedAuthentication.API.Domain.UnitOfWork;

namespace TokenBasedAuthentication.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public ProductService(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> AddProduct(Product product)
        {
            try
            {
                await _productRepository.AddProductAsync(product);

                await _unitOfWork.CompleteAsync();

                return new ProductResponse(product);
            }

            catch(Exception ex)
            {
                return new ProductResponse($"Error an occurred while adding a product : {ex.Message}");
            }
        }

        public async Task<ProductResponse> FindByIdAsync(int productId)
        {
            try
            {
                var product =await _productRepository.FindByIdAsync(productId);
                if(product==null)
                    return new ProductResponse($"Could not find product");

                return new ProductResponse(product);
            }

            catch(Exception ex)
            {
                return new ProductResponse($"Error an occurred while finding the product : {ex.Message}");
            }
        }

        public async Task<ProductListResponse> ListAsync()
        {
            try
            {
                var productList =await _productRepository.ListAsync();

                return new ProductListResponse(productList);
            }

            catch(Exception ex)
            {
                return new ProductListResponse($"Error an occurred while getting the product list : {ex.Message}");
            }
        }

        public async Task<ProductResponse> RemoveProduct(int productId)
        {
            try
            {
                var product =await _productRepository.FindByIdAsync(productId);
                if(product==null)
                    return new ProductResponse($"Could not find product");

                await _productRepository.RemoveProductAsync(productId);    
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(product);
            }

            catch(Exception ex)
            {
                return new ProductResponse($"Error an occurred while deleting the product : {ex.Message}");
            }
        }

        public async Task<ProductResponse> UpdateProduct(Product product, int productId)
        {
            try
            {
                var firstProduct =await _productRepository.FindByIdAsync(productId);
                if(firstProduct==null)
                    return new ProductResponse($"Could not find product");

                firstProduct.Name=product.Name;
                firstProduct.Category=product.Category;
                firstProduct.Price=product.Price;
                    
                _productRepository.UpdateProductASync(firstProduct);    
                await _unitOfWork.CompleteAsync();

                return new ProductResponse(firstProduct);
            }

            catch(Exception ex)
            {
                return new ProductResponse($"Error an occurred while updating the product : {ex.Message}");
            }
        }
    }
}