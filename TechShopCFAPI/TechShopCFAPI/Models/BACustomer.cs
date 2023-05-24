using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TechShopCFAPI.Models
{
    public class BACustomer
    {
        List<Link> links = new List<Link>();
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }
        [Required, MaxLength(30), MinLength(3)]
        public string FullName { get; set; }
        [Required, MaxLength(20), MinLength(3), Index(IsUnique = true)]
        public string UserName { get; set; }
        [Required, MaxLength(20), MinLength(3)]
        public string Password { get; set; }
        [Required, EmailAddress, MaxLength(50), MinLength(10), Index(IsUnique = true)]
        public string Email { get; set; }
        [MaxLength(100), MinLength(3)]
        public string ProfilePic { get; set; }
        [MinLength(11), MaxLength(11)]
        public string Phone { get; set; }
        [MinLength(3), MaxLength(100)]
        public string Address { get; set; }
        [Required, MaxLength(20), MinLength(3)]
        public string Status { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime LastUpdated { get; set; }
        [NotMapped]
        public List<Link> Links
        {
            get { return links; }
        }
    }
}