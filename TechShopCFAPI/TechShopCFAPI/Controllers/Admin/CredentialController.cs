using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechShopCFAPI.Attributes;
using TechShopCFAPI.Models;
using TechShopCFAPI.Repositories.AdminModule;

namespace TechShopCFAPI.Controllers.Admin
{
    public class CredentialController : ApiController
    {
        CredentialRepository credentialRepository = new CredentialRepository();
        [Route("api/credentials")]
        public IHttpActionResult Get(string email, string password)
        {
            if (email == null || password == null)
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            }
            else
            {
                var c = credentialRepository.Validation(email, password);
                if (c == null)
                {
                    return StatusCode(HttpStatusCode.NotFound);
                }
                else
                {
                    return Ok(c);
                }
            }
        }

        [Route("api/credentials/{id}"), BasicAuthentication]
        public IHttpActionResult Get(int id)
        {
            Credential cred = credentialRepository.Get(id);
            if (cred == null)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return Ok(cred);
        }

        [Route("api/credentials/{id}"), BasicAuthentication]
        public IHttpActionResult PutChangePass([FromBody]Credential cred, [FromUri]int id)
        {
            cred.Id = id;
            credentialRepository.Update(cred);
            return Ok();
        }
    }
}
