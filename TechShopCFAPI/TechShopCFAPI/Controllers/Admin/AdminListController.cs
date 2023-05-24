using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TechShopCFAPI.Attributes;
using TechShopCFAPI.Models;
using TechShopCFAPI.Repositories.AdminModule;

namespace TechShopCFAPI.Controllers.Admin
{
    [RoutePrefix("api/admins")]
    public class AdminListController : ApiController
    {
        AdminRepository adminRepository = new AdminRepository();
        CredentialRepository credentialRepository = new CredentialRepository();

        public CredentialRepository CredentialRepository { get => credentialRepository; set => credentialRepository = value; }

        //[Route("")]
        [Route(""), BasicAuthentication]
        public IHttpActionResult GetActive()
        {
            return Ok(adminRepository.GetActive());
        }

        [Route("name"),BasicAuthentication]
        public IHttpActionResult GetByName(string name)
        {
            return Ok(adminRepository.GetByName(name));
        }

        [Route("restricted"), BasicAuthentication]
        public IHttpActionResult GetRestricted()
        {
            return Ok(adminRepository.GetRestricted());
        }

        [Route("profile"), BasicAuthentication]
        public IHttpActionResult Get(string email)
        {
            Models.Admin admin = adminRepository.GetByEmail(email);
            if (admin == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            admin.Links.Add(new Link() { Url = HttpContext.Current.Request.Url.AbsoluteUri, Method = "POST", Relation = "Create new Admin" });
            admin.Links.Add(new Link() { Url = HttpContext.Current.Request.Url.AbsoluteUri, Method = "PUT", Relation = "Modify Admin" });
            return Ok(admin);
        }

        [Route("{id}", Name = "GetByAdminId"), BasicAuthentication]
        public IHttpActionResult Get(int id)
        {
            Models.Admin admin = adminRepository.Get(id);
            if (admin == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            admin.Links.Add(new Link() { Url = HttpContext.Current.Request.Url.AbsoluteUri, Method = "POST", Relation = "Create new Admin" });
            admin.Links.Add(new Link() { Url = HttpContext.Current.Request.Url.AbsoluteUri, Method = "PUT", Relation = "Modify Admin" });
            return Ok(admin);
        }

        [Route(""), BasicAuthentication]
        public IHttpActionResult Post(Models.Admin admin)
        {
            admin.ProfilePic = "default.jpg";
            admin.Status = 1;
            admin.JoiningDate = DateTime.Now;
            admin.LastUpdated = DateTime.Now;
            adminRepository.Insert(admin);

            Credential cred = new Credential();
            cred.Email = admin.Email;
            cred.UserName = admin.UserName;
            cred.Password = admin.UserName + "1458";
            cred.Status = 1;
            cred.Type = 1;
            CredentialRepository.Insert(cred);

            return Created("api/admins/" + admin.Id, admin);
        }

        [Route("{id}"), BasicAuthentication]
        public IHttpActionResult PutAdmin([FromBody]Models.Admin admin, [FromUri]int id)
        {
            admin.Id = id;
            admin.LastUpdated = DateTime.Now;
            adminRepository.Update(admin);
            return Ok();
        }

        [Route("BlockAdmin/{id}"), BasicAuthentication]
        public IHttpActionResult PutBlockAdmin(int id)
        {
            var admin = adminRepository.Get(id);
            adminRepository.Restrict(admin.Email);

            CredentialRepository.Restrict(admin.Email);
            return Ok();
        }

        
    }
}
