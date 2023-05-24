using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Web;

namespace TechShopCFAPI.Models
{
    public class Credential
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [NotMapped]
        public string ConfirmPassword { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int Status { get; set; }
    }
}