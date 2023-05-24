using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechShopCFAPI.Models;
using TechShopCFAPI.Repository;
using System.Threading;
using System.Web;
using TechShopCFAPI.Attributes;

namespace TechShopCFAPI.Controllers.BuyingAgent
{
    [RoutePrefix("api/buying_agent")]
    public class BuyingAgentController : ApiController
    {
        BuyingAgentRepository buyingAgentRepository = new BuyingAgentRepository();

        [Route("{id}", Name = "GetBuyingAgentById"), BasicAuthentication]
        public IHttpActionResult Get([FromUri] int id)
        {
            var buyingAgnet = buyingAgentRepository.Get(id);

            if (buyingAgnet == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                var mainUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                var insertUrl = mainUrl.Remove(mainUrl.Length - 2, 2);
                buyingAgnet.Links.Add(new Link() { Url = mainUrl, Method = "GET", Relation = "Get an existing Buying Agent" });
                buyingAgnet.Links.Add(new Link() { Url = insertUrl, Method = "POST", Relation = "Create a new Buying Agent" });
                buyingAgnet.Links.Add(new Link() { Url = mainUrl, Method = "PUT", Relation = "Update an existing Buying Agent" });
                buyingAgnet.Links.Add(new Link() { Url = mainUrl, Method = "DELETE", Relation = "Delete an existing Buying Agent" });
                buyingAgnet.Links.Add(new Link() { Url = insertUrl+ "/update_password/"+buyingAgnet.Id, Method = "PUT", Relation = "Update an existing Buying Agent password" });
                return Ok(buyingAgnet);
            }
        }

        [Route(""), BasicAuthentication]
        public IHttpActionResult Get()
        {
            var buyingAgnets = buyingAgentRepository.GetAll().ToList();

            if(buyingAgnets.Count == 0)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                var mainUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                foreach(var buyingAgent in buyingAgnets)
                {
                    buyingAgent.Links.Add(new Link() { Url = mainUrl + "/"+buyingAgent.Id, Method = "GET", Relation = "Get an existing Buying Agent" });
                    buyingAgent.Links.Add(new Link() { Url = mainUrl, Method = "POST", Relation = "Create a new Buying Agent" });
                    buyingAgent.Links.Add(new Link() { Url = mainUrl + "/" + buyingAgent.Id, Method = "PUT", Relation = "Update an existing Buying Agent" });
                    buyingAgent.Links.Add(new Link() { Url = mainUrl + "/" + buyingAgent.Id, Method = "DELETE", Relation = "Delete an existing Buying Agent" });
                    buyingAgent.Links.Add(new Link() { Url = mainUrl + "/update_password/" + buyingAgent.Id, Method = "PUT", Relation = "Update an existing Buying Agent password" });
                }
                return Ok(buyingAgnets);
            }
        }

        [Route(""), BasicAuthentication]
        public IHttpActionResult Post(Models.BuyingAgent buyingAgent)
        {
            if(ModelState.IsValid)
            {
                buyingAgentRepository.Insert(buyingAgent);
                string url = Url.Link("GetBuyingAgentById", new { id = buyingAgent.Id });
                return Created(url, buyingAgent);
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
            
        }

        [Route("{id}"), BasicAuthentication]
        public IHttpActionResult Put([FromBody] Models.BuyingAgent buyingAgent, [FromUri] int id)
        {
            var updatedBuyingAgent = buyingAgentRepository.Get(id);
            if(updatedBuyingAgent == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                if(ModelState.IsValid)
                {
                    updatedBuyingAgent.FullName = buyingAgent.FullName;
                    updatedBuyingAgent.UserName = buyingAgent.UserName;
                    updatedBuyingAgent.Email = buyingAgent.Email;
                    updatedBuyingAgent.Phone = buyingAgent.Phone;
                    updatedBuyingAgent.Address = buyingAgent.Address;
                    updatedBuyingAgent.LastUpdated = DateTime.Now;
                    buyingAgentRepository.Update(updatedBuyingAgent);
                    return Ok(updatedBuyingAgent);
                }
                else
                {
                    return StatusCode(HttpStatusCode.NotModified);
                }
            }
        }

        [Route("{id}"), BasicAuthentication]
        public IHttpActionResult Delete([FromUri] int id)
        {
            var buyingAgnet = buyingAgentRepository.Get(id);

            if (buyingAgnet == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                buyingAgentRepository.Delete(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        [Route("update_password/{id}"), HttpPut, BasicAuthentication]
        public IHttpActionResult UpdatePassword([FromBody] Models.BuyingAgent buyingAgent, [FromUri] int id)
        {
            CredentialRepository credentialRepository = new CredentialRepository();
            var updatedBuyingAgent = buyingAgentRepository.Get(id);
            var updatedCredentials = credentialRepository.GetByEmail(updatedBuyingAgent.Email);

            if (updatedBuyingAgent == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    updatedBuyingAgent.Password = buyingAgent.Password;
                    updatedBuyingAgent.LastUpdated = DateTime.Now;
                    buyingAgentRepository.Update(updatedBuyingAgent);
                    updatedCredentials.Password = buyingAgent.Password;
                    credentialRepository.Update(updatedCredentials);
                    return Ok(updatedBuyingAgent);
                }
                else
                {
                    return StatusCode(HttpStatusCode.NotModified);
                }
            }
        }

        [Route(""), HttpGet, BasicAuthentication]
        public IHttpActionResult BuyingAgentByEmail(string Email)
        {
            var details = buyingAgentRepository.GetBuyingAgentByEmail(Email);
            return Ok(details);
        }
    }
}
