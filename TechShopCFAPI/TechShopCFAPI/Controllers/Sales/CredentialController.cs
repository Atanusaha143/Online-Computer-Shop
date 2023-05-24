using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TechShopCFAPI.Models;
using TechShopCFAPI.Models.Sales.DataModels;

namespace TechShopCFAPI.Controllers
{
    public class CredentialController : ApiController
    {
        //CredentialRepository credentialRepository = new CredentialRepository();

        [Route("api/credentials")]
        public IHttpActionResult Get(string Email, string Password)
        {
            TechShopDbContext context2 = new TechShopDbContext();
            if (Email == null || Password == null)
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            }
            else
            {
                ProductsDataModel cc = new ProductsDataModel();
                var c = cc.cred(Email, Password);
                    if (context2.Credentials.Where(x => x.Email == Email && x.Password == Password) == null)
                    {
                        return StatusCode(HttpStatusCode.NotFound);
                    }
                    else
                    {
                        return Json(c);
                    }
                
            }
        }
    }
}
