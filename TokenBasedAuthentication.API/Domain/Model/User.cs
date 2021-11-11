using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TokenBasedAuthentication.API.Domain.Model
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }     

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(8)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string SurName { get; set; }

        [StringLength(500)]
        public string RefreshToken { get; set; }

        public DateTime? RefreshTokenEndDate { get; set; }
    }
}