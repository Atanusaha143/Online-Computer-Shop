using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TechShopCFAPI.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        [StringLength(150)]
        public string ProfilePic { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(12)]
        public string Phone { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        [StringLength(150)]
        public string Address { get; set; }

        public System.DateTime JoiningDate { get; set; }

        public System.DateTime LastUpdated { get; set; }

        [NotMapped]
        List<Link> links = new List<Link>();
        [NotMapped]
        public List<Link> Links
        {
            get { return links; }
        }
    }
}