using System.ComponentModel.DataAnnotations;

namespace TokenBasedAuthentication.API.Resources
{
    public class ProductResource
    {
        [Required]
        public string Name {get;set;}

        [Required]
        public string Category {get;set;}

        [Required]
        public decimal? Price{get;set;}
    }
}