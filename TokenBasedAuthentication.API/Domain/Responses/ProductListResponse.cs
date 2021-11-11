using System.Collections.Generic;
using TokenBasedAuthentication.API.Domain.Model;

namespace TokenBasedAuthentication.API.Domain.Responses
{
    public class ProductListResponse: BaseResponse
    {
        public IEnumerable<Product> ProductList {get;set;}

        private ProductListResponse(bool success, string message, IEnumerable<Product> productList) : base(success, message)
        {
            ProductList=productList;
        }

        public ProductListResponse(IEnumerable<Product> productList): this(true,string.Empty,productList){}

        public ProductListResponse(string message): this(false,message,null){}
        
    }
}