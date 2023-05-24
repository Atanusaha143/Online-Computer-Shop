using MimeKit;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Http;
using TechShopCFAPI.Authentications;
using TechShopCFAPI.Models;
using TechShopCFAPI.Repository;
using MailKit.Net.Smtp;
using System.IO;

namespace TechShopCFAPI.Controllers.Customer
{
    [RoutePrefix("Api/Customers")]
    public class CustomerController : ApiController
    {
        CustomerRepository customerRepository = new CustomerRepository();
        ShippingRepository shippingRepository = new ShippingRepository();
        [Route("")]
        public IHttpActionResult Get()
        {
            var activeCustomerList = customerRepository.GetAll().Where(x=>x.Status=="Active").ToList();
            if (activeCustomerList!=null && activeCustomerList.Count!=0)
            {
                foreach (var customer in activeCustomerList)
                {
                    var url = HttpContext.Current.Request.Url.AbsoluteUri;
                    customer.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "POST", Relation = "Create a new customer." });
                    customer.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get an existing specific customer." });
                    customer.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "GET", Relation = "Get all the registered customers." });
                    customer.Links.Add(new Link() { Url = url, Method = "PUT", Relation = "Edit an existing specific customer." });
                    customer.Links.Add(new Link() { Url = url, Method = "DELETE", Relation = "Deletes an existing specific customer." });
                    customer.Links.Add(new Link() { Url = url + "/ResetPassword", Method = "PUT", Relation = "Resets password of an existing customer." });
                    customer.Links.Add(new Link() { Url = url + "/shipping", Method = "GET", Relation = "Gets shipping data of an existing customer." });
                    customer.Links.Add(new Link() { Url = url + "/UploadProfilePicture", Method = "POST", Relation = "Upload profile picture of an existing customer." });
                    customer.Links.Add(new Link() { Url = url + "/RemoveProfilePicture", Method = "PUT", Relation = "Removes profile picture of an existing customer." });
                }
                return Ok(activeCustomerList);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
        [Route("{id}", Name ="GetById"), LoginAuthentication]
        public IHttpActionResult Get(int id)
        {
            var customer = customerRepository.Get(id);
            if (customer!=null)
            {
                var url = HttpContext.Current.Request.Url.AbsoluteUri;
                customer.Links.Add(new Link() { Url = url.Substring(0,url.Length-3), Method = "POST", Relation = "Create a new customer." });
                customer.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get an existing specific customer." });
                customer.Links.Add(new Link() { Url = url.Substring(0, url.Length-3), Method = "GET", Relation = "Get all the registered customers." });
                customer.Links.Add(new Link() { Url = url, Method = "PUT", Relation = "Edit an existing specific customer." });
                customer.Links.Add(new Link() { Url = url, Method = "DELETE", Relation = "Deletes an existing specific customer." });
                customer.Links.Add(new Link() { Url = url+ "/ResetPassword", Method = "PUT", Relation = "Resets password of an existing customer." });
                customer.Links.Add(new Link() { Url = url+ "/shipping", Method = "GET", Relation = "Gets shipping data of an existing customer." });
                customer.Links.Add(new Link() { Url = url+ "/UploadProfilePicture", Method = "POST", Relation = "Upload profile picture of an existing customer." });
                customer.Links.Add(new Link() { Url = url+ "/RemoveProfilePicture", Method = "PUT", Relation = "Removes profile picture of an existing customer." });
                return Ok(customer);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }
        [Route("")]
        public IHttpActionResult Post(Models.Customer customer)
        {
            if (ModelState.IsValid)
            {
                customerRepository.Insert(customer);

                Models.ShippingData shippingData = new ShippingData();
                shippingData.CustomerId = customer.CustomerId;
                shippingData.CardNumber = null;
                shippingData.CardType = null;
                shippingData.ExpirationMonth = null;
                shippingData.ExpirationYear = null;
                shippingData.ShippingAddress = null;
                shippingData.ShippingMethod = null;
                shippingRepository.Insert(shippingData);

                string URL = Url.Link("GetById", new { id = customer.CustomerId });
                var url = HttpContext.Current.Request.Url.AbsoluteUri;
                customer.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "POST", Relation = "Create a new customer." });
                customer.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get an existing specific customer." });
                customer.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "GET", Relation = "Get all the registered customers." });
                customer.Links.Add(new Link() { Url = url, Method = "PUT", Relation = "Edit an existing specific customer." });
                customer.Links.Add(new Link() { Url = url, Method = "DELETE", Relation = "Deletes an existing specific customer." });
                customer.Links.Add(new Link() { Url = url + "/ResetPassword", Method = "PUT", Relation = "Resets password of an existing customer." });
                customer.Links.Add(new Link() { Url = url + "/shipping", Method = "GET", Relation = "Gets shipping data of an existing customer." });
                customer.Links.Add(new Link() { Url = url + "/UploadProfilePicture", Method = "POST", Relation = "Upload profile picture of an existing customer." });
                customer.Links.Add(new Link() { Url = url + "/RemoveProfilePicture", Method = "PUT", Relation = "Removes profile picture of an existing customer." });
                return Created(URL, customer);
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }
        [Route("{id}"), HttpPut]
        public IHttpActionResult Edit([FromBody]Models.Customer customer, [FromUri]int id)
        {
            customer.CustomerId = id;
            if (ModelState.IsValid)
            {
                var url = HttpContext.Current.Request.Url.AbsoluteUri;
                customer.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "POST", Relation = "Create a new customer." });
                customer.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get an existing specific customer." });
                customer.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "GET", Relation = "Get all the registered customers." });
                customer.Links.Add(new Link() { Url = url, Method = "PUT", Relation = "Edit an existing specific customer." });
                customer.Links.Add(new Link() { Url = url, Method = "DELETE", Relation = "Deletes an existing specific customer." });
                customer.Links.Add(new Link() { Url = url + "/ResetPassword", Method = "PUT", Relation = "Resets password of an existing customer." });
                customer.Links.Add(new Link() { Url = url + "/shipping", Method = "GET", Relation = "Gets shipping data of an existing customer." });
                customer.Links.Add(new Link() { Url = url + "/UploadProfilePicture", Method = "POST", Relation = "Upload profile picture of an existing customer." });
                customer.Links.Add(new Link() { Url = url + "/RemoveProfilePicture", Method = "PUT", Relation = "Removes profile picture of an existing customer." });
                customerRepository.Update(customer);
                return Ok(customer);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotModified);
            }
        }
        [Route("{id}"), HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            customerRepository.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
        [Route("GetCustomerLoginValidation"), HttpGet, LoginAuthentication]
        public IHttpActionResult GetCustomerLoginValidation()
        {
            var customer = customerRepository.GetCustomerByEmailOrUserName(Thread.CurrentPrincipal.Identity.Name);

            var url = HttpContext.Current.Request.Url.AbsoluteUri;
            customer.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "POST", Relation = "Create a new customer." });
            customer.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get an existing specific customer." });
            customer.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "GET", Relation = "Get all the registered customers." });
            customer.Links.Add(new Link() { Url = url, Method = "PUT", Relation = "Edit an existing specific customer." });
            customer.Links.Add(new Link() { Url = url, Method = "DELETE", Relation = "Deletes an existing specific customer." });
            customer.Links.Add(new Link() { Url = url + "/ResetPassword", Method = "PUT", Relation = "Resets password of an existing customer." });
            customer.Links.Add(new Link() { Url = url + "/shipping", Method = "GET", Relation = "Gets shipping data of an existing customer." });
            customer.Links.Add(new Link() { Url = url + "/UploadProfilePicture", Method = "POST", Relation = "Upload profile picture of an existing customer." });
            customer.Links.Add(new Link() { Url = url + "/RemoveProfilePicture", Method = "PUT", Relation = "Removes profile picture of an existing customer." });

            if (customer!=null)
            {
                return Ok(customer);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }
        [Route("GetUserNameValidation/{user}", Name = "ValidateByUserName")]
        public IHttpActionResult GetUserNameValidation(string user)
        {
            var customer = customerRepository.GetCustomerByEmailOrUserName(user);

            var url = HttpContext.Current.Request.Url.AbsoluteUri;
            customer.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "POST", Relation = "Create a new customer." });
            customer.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get an existing specific customer." });
            customer.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "GET", Relation = "Get all the registered customers." });
            customer.Links.Add(new Link() { Url = url, Method = "PUT", Relation = "Edit an existing specific customer." });
            customer.Links.Add(new Link() { Url = url, Method = "DELETE", Relation = "Deletes an existing specific customer." });
            customer.Links.Add(new Link() { Url = url + "/ResetPassword", Method = "PUT", Relation = "Resets password of an existing customer." });
            customer.Links.Add(new Link() { Url = url + "/shipping", Method = "GET", Relation = "Gets shipping data of an existing customer." });
            customer.Links.Add(new Link() { Url = url + "/UploadProfilePicture", Method = "POST", Relation = "Upload profile picture of an existing customer." });
            customer.Links.Add(new Link() { Url = url + "/RemoveProfilePicture", Method = "PUT", Relation = "Removes profile picture of an existing customer." });

            if (customer!=null)
            {
                return StatusCode(HttpStatusCode.Conflict);
            }
            else
            {
                return StatusCode(HttpStatusCode.OK);
            }
        }
        [Route("{customerId}/shipping")]
        public IHttpActionResult GetShippingDataByCustomerId(int customerId)
        {
            var shippingData = shippingRepository.GetShippingDataByCustomerId(customerId);
            var url = HttpContext.Current.Request.Url.AbsoluteUri;

            shippingData.Links.Add(new Link() { Url = url + "/" + shippingData.CustomerId+"/shipping", Method = "GET", Relation = "Get shipping data by customer ID." });

            if (shippingData!=null)
            {
                return Ok(shippingData);
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }
        [Route("CheckValidEmail"), HttpPost]
        public IHttpActionResult CheckValidEmail(JObject jsonResult)
        {
            string email = null;

            foreach (JProperty property in jsonResult.Properties())
            {
                email = (string)property.Value;
            }
            var customers = customerRepository.GetAll().Where(x => x.Email == email).FirstOrDefault();
            if (customers!=null)
            {
                Random randomNumber = new Random();
                var verificationCode = randomNumber.Next(1000, 9999).ToString();
                var userID = customers.CustomerId;

                //Send email
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Verification Code", "shohag.cse45@gmail.com"));
                message.To.Add(new MailboxAddress("Customer", email));
                message.Subject = "Password Reset";
                message.Body = new TextPart("plain")
                {
                    Text = "Verification code is : " + verificationCode
                };
                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("shohag.cse45@gmail.com", "cinecarnival");
                    client.Send(message);
                    client.Disconnect(true);
                }
                return Ok(verificationCode+":"+userID);
            }
            else
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
        }
        [Route("{id}/ResetPassword"), HttpPut]
        public IHttpActionResult ResetPassword([FromUri]int id, JObject passwordObj)
        {
            string password = null;
            foreach (JProperty property in passwordObj.Properties())
            {
                password = (string)property.Value;
            }

            var customer = customerRepository.Get(id);
            customer.Password = password;

            var url = HttpContext.Current.Request.Url.AbsoluteUri;
            customer.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "POST", Relation = "Create a new customer." });
            customer.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get an existing specific customer." });
            customer.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "GET", Relation = "Get all the registered customers." });
            customer.Links.Add(new Link() { Url = url, Method = "PUT", Relation = "Edit an existing specific customer." });
            customer.Links.Add(new Link() { Url = url, Method = "DELETE", Relation = "Deletes an existing specific customer." });
            customer.Links.Add(new Link() { Url = url + "/ResetPassword", Method = "PUT", Relation = "Resets password of an existing customer." });
            customer.Links.Add(new Link() { Url = url + "/shipping", Method = "GET", Relation = "Gets shipping data of an existing customer." });
            customer.Links.Add(new Link() { Url = url + "/UploadProfilePicture", Method = "POST", Relation = "Upload profile picture of an existing customer." });
            customer.Links.Add(new Link() { Url = url + "/RemoveProfilePicture", Method = "PUT", Relation = "Removes profile picture of an existing customer." });
            customerRepository.Update(customer);
            return Ok(customer);
        }

        [Route("{id}/UploadProfilePicture"), HttpPost]
        public IHttpActionResult UploadProfilePicture(int id)
        {
            var customer = customerRepository.Get(id);
            if (customer!=null)
            {
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    // Get the uploaded image from the Files collection
                    var image = HttpContext.Current.Request.Files["UploadedImage"];

                    string ImageFileName = Path.GetFileName(image.FileName);
                    string ImageExtension = Path.GetExtension(image.FileName);
                    if (ImageExtension.Equals(".jpg") || ImageExtension == ".jpg" || ImageExtension.Equals(".JPG") || ImageExtension == ".JPG" || ImageExtension.Equals(".PNG") || ImageExtension == ".PNG" || ImageExtension.Equals(".png") || ImageExtension == ".png")
                    {
                        string FolderPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Contents/Customer/ProfilePicture/"), ImageFileName.Replace(ImageFileName, customer.Email.ToString() + ImageExtension));
                        image.SaveAs(FolderPath);
                        customer.ProfilePic = ImageFileName.Replace(ImageFileName, customer.Email.ToString() + ImageExtension);

                        var url = HttpContext.Current.Request.Url.AbsoluteUri;
                        customer.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "POST", Relation = "Create a new customer." });
                        customer.Links.Add(new Link() { Url = url, Method = "GET", Relation = "Get an existing specific customer." });
                        customer.Links.Add(new Link() { Url = url.Substring(0, url.Length - 3), Method = "GET", Relation = "Get all the registered customers." });
                        customer.Links.Add(new Link() { Url = url, Method = "PUT", Relation = "Edit an existing specific customer." });
                        customer.Links.Add(new Link() { Url = url, Method = "DELETE", Relation = "Deletes an existing specific customer." });
                        customer.Links.Add(new Link() { Url = url + "/ResetPassword", Method = "PUT", Relation = "Resets password of an existing customer." });
                        customer.Links.Add(new Link() { Url = url + "/shipping", Method = "GET", Relation = "Gets shipping data of an existing customer." });
                        customer.Links.Add(new Link() { Url = url + "/UploadProfilePicture", Method = "POST", Relation = "Upload profile picture of an existing customer." });
                        customer.Links.Add(new Link() { Url = url + "/RemoveProfilePicture", Method = "PUT", Relation = "Removes profile picture of an existing customer." });
                        customerRepository.Update(customer);
                        return Ok(customer) ;
                    }
                    else
                    {
                        return StatusCode(HttpStatusCode.NotModified);
                    }
                }
                else
                {
                    return StatusCode(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
        }

        [Route("{id}/RemoveProfilePicture"), HttpPut]
        public IHttpActionResult RemoveProfilePicture(int id)
        {
            var customer = customerRepository.Get(id);
            customer.ProfilePic = null;

            var url = HttpContext.Current.Request.Url.AbsoluteUri;

            customer.Links.Add(new Link() { Url = url, Method = "POST", Relation = "Create a new customer." });
            customer.Links.Add(new Link() { Url = url + "/" + customer.CustomerId, Method = "GET", Relation = "Get an existing specific customer." });
            customer.Links.Add(new Link() { Url = url + "/" + customer.CustomerId, Method = "PUT", Relation = "Edit an existing specific customer." });
            customer.Links.Add(new Link() { Url = url + "/" + customer.CustomerId, Method = "DELETE", Relation = "Deletes an existing specific customer." });
            customer.Links.Add(new Link() { Url = url + "/" + customer.CustomerId + "/ResetPassword", Method = "PUT", Relation = "Resets password of an existing customer." });
            customer.Links.Add(new Link() { Url = url + "/" + customer.CustomerId + "/shipping", Method = "GET", Relation = "Gets shipping data of an existing customer." });
            customer.Links.Add(new Link() { Url = url + "/" + customer.CustomerId + "/UploadProfilePicture", Method = "POST", Relation = "Upload profile picture of an existing customer." });
            customer.Links.Add(new Link() { Url = url + "/" + customer.CustomerId + "/RemoveProfilePicture", Method = "PUT", Relation = "Removes profile picture of an existing customer." });
            customerRepository.Update(customer);
            return Ok(customer);
        }
    }
}
