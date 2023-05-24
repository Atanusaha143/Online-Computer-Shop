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
    [RoutePrefix("api/buyingagents")]
    public class BuyingAgentListController : ApiController
    {
        BuyingAgentRepository byRepo = new BuyingAgentRepository();
        CredentialRepository credentialRepository = new CredentialRepository();

        [Route(""), BasicAuthentication]
        public IHttpActionResult GetActive()
        {
            return Ok(byRepo.GetActive());
        }

        [Route("name"), BasicAuthentication]
        public IHttpActionResult GetByName(string name)
        {
            return Ok(byRepo.GetByName(name));
        }

        [Route("restricted"), BasicAuthentication]
        public IHttpActionResult GetRestricted()
        {
            return Ok(byRepo.GetRestricted());
        }

        [Route("{id}"), BasicAuthentication]
        public IHttpActionResult Get(int id)
        {
            BuyingAgent by = byRepo.Get(id);
            if (by == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            by.Links.Add(new Link() { Url = HttpContext.Current.Request.Url.AbsoluteUri, Method = "POST", Relation = "Create new Buying Agent" });
            by.Links.Add(new Link() { Url = HttpContext.Current.Request.Url.AbsoluteUri, Method = "PUT", Relation = "Modify Buying Agent" });
            return Ok(by);
        }


        [Route(""), BasicAuthentication]
        public IHttpActionResult Post(BuyingAgent by)
        {
            by.ProfilePic = "default.jpg";
            by.Status = 1;
            by.JoiningDate = DateTime.Now;
            by.LastUpdated = DateTime.Now;
            byRepo.Insert(by);

            Credential cred = new Credential();
            cred.Email = by.Email;
            cred.UserName = by.UserName;
            cred.Password = by.Password;
            cred.Status = 1;
            cred.Type = 3;
            credentialRepository.Insert(cred);

            return Created("api/buyingagents/" + by.Id, by);
        }

        [Route("{id}"), BasicAuthentication]
        public IHttpActionResult PutBuyingAgent([FromBody]BuyingAgent by, [FromUri]int id)
        {
            by.Id = id;
            by.LastUpdated = DateTime.Now;
            byRepo.Update(by);
            return Ok();
        }

        [Route("Block/{id}"), BasicAuthentication]
        public IHttpActionResult PutBlockBuyingAgent(int id)
        {
            var by = byRepo.Get(id);
            byRepo.Restrict(by.Email);

            credentialRepository.Restrict(by.Email);
            return Ok();
        }
    }


}