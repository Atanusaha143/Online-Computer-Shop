
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TechShopCFAPI.Models
{
    public class BuyingAgent
    {
        List<Link> links = new List<Link>();

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50), MinLength(3)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(50), MinLength(3), Index(IsUnique = true)]
        public string UserName { get; set; }

        [MaxLength(50), MinLength(3)]
        public string ProfilePic { get; set; }

        [Required]
        [MaxLength(20), MinLength(3)]
        public string Password { get; set; }

        [Required]
        [MaxLength(50), MinLength(3), Index(IsUnique = true)]
        public string Email { get; set; }

        [Required]
        [MaxLength(11), MinLength(11)]
        public string Phone { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        [MaxLength(150), MinLength(5)]
        public string Address { get; set; }

        [Required]
        public int Status { get; set; }

        public System.DateTime JoiningDate { get; set; }

        public Nullable<DateTime> LastUpdated { get; set; }

        [NotMapped]
        public List<Link> Links
        {
            get { return links; }
        }
    }
}