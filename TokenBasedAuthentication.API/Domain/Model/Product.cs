using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TokenBasedAuthentication.API.Domain.Model
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}

        [StringLength(50)]
        public string Name {get;set;}

        [StringLength(50)]
        public string Category {get;set;}
        public decimal? Price {get;set;}
    }
}