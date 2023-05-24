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
    [RoutePrefix("api/salesexecutives")]
    public class SalesExecutiveListController : ApiController
    {
        SalesExecutiveRepository saleExeRepo = new SalesExecutiveRepository();
        CredentialRepository credentialRepository = new CredentialRepository();

        [Route(""), BasicAuthentication]
        public IHttpActionResult GetActive()
        {
            return Ok(saleExeRepo.GetActive());
        }

        [Route("name"), BasicAuthentication]
        public IHttpActionResult GetByName(string name)
        {
            return Ok(saleExeRepo.GetByName(name));
        }

        [Route("restricted"), BasicAuthentication]
        public IHttpActionResult GetRestricted()
        {
            return Ok(saleExeRepo.GetRestricted());
        }

        [Route("{id}"), BasicAuthentication]
        public IHttpActionResult Get(int id)
        {
            SalesExecutive salesEXe = saleExeRepo.Get(id);
            if (salesEXe == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            salesEXe.Links.Add(new Link() { Url = HttpContext.Current.Request.Url.AbsoluteUri, Method = "POST", Relation = "Create new Sales Executive" });
            salesEXe.Links.Add(new Link() { Url = HttpContext.Current.Request.Url.AbsoluteUri, Method = "PUT", Relation = "Modify Sales Executive" });
            return Ok(salesEXe);
        }

        [Route(""), BasicAuthentication]
        public IHttpActionResult Post(SalesExecutive salesExecutive)
        {
            salesExecutive.ProfilePic = "default.jpg";
            salesExecutive.Status = 1;
            salesExecutive.JoiningDate = DateTime.Now;
            salesExecutive.LastUpdated = DateTime.Now;
            saleExeRepo.Insert(salesExecutive);

            Credential cred = new Credential();
            cred.Email = salesExecutive.Email;
            cred.UserName = salesExecutive.UserName;
            cred.Password = salesExecutive.Password;
            cred.Status = 1;
            cred.Type = 2;
            credentialRepository.Insert(cred);

            return Created("api/salesexecutives/" + salesExecutive.Id, salesExecutive);
        }

        [Route("{id}"), BasicAuthentication]
        public IHttpActionResult Put([FromBody]SalesExecutive salesExe, [FromUri]int id)
        {
            salesExe.Id = id;
            salesExe.LastUpdated = DateTime.Now;
            saleExeRepo.Update(salesExe);

            return Ok();
        }

        [Route("Block/{id}"), BasicAuthentication]
        public IHttpActionResult PutBlockSalesExecutive([FromUri] int id)
        {
            var by = saleExeRepo.Get(id);
            saleExeRepo.Restrict(by.Email);

            credentialRepository.Restrict(by.Email);
            return Ok();
        }
    }
}
