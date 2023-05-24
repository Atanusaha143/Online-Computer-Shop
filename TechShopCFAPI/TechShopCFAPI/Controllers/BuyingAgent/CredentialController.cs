using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechShopCFAPI.Repository;

namespace TechShopCFAPI.Controllers.BuyingAgent
{
    public class CredentialController : ApiController
    {
        CredentialRepository credentialRepository = new CredentialRepository();
        [Route("api/credentials")]
        public IHttpActionResult Get(string Email, string Password)
        {
            if(Email == null || Password == null)
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            }
            else
            {
                var c = credentialRepository.Validation(Email, Password);
                if(c==null)
                {
                    return StatusCode(HttpStatusCode.NotFound);
                }
                else
                {
                    return Ok(c);
                }
            }
        }
    }
}
