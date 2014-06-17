using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using FelixWebAPI.Models;
using System.Net.Http.Headers;

namespace FelixWebAPI.Controllers
{
    [RoutePrefix("api")]
    public class UsersController : ApiController
    {
        DataClassesDataContext dc = new DataClassesDataContext();

        // GET api/users
        [Route("users")]
        public IEnumerable<USER> GetAllUsers()
        {
            var users = from result in dc.USERs
                        select result;

            return users;
        }

        // GET api/users/user0001
        [Route("getUser/{usrid?}")]
        public IEnumerable<USER> GetUsers(string usrid)
        {
            var user = from result in dc.USERs
                       where usrid == result.USER_ID
                       select result;
            return user;
        }

        // POST api/addUser
        [Route("addUser")]
        [HttpPost]
        public HttpResponseMessage AddUser(USER user)
        {
            try
            {
                dc.USERs.InsertOnSubmit(user);
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return ResponseMsg();
        }

        private HttpResponseMessage ResponseMsg()
        {
            //Acknowledge to payment gateway no matter success or failed
            var response = new HttpResponseMessage();
            response.Content = new StringContent("OK");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

        // PUT api/updateUser/user0001
        [HttpPost]
        [Route("updateUser/{usrid?}")]
        public HttpResponseMessage updateUser(string usrid, [FromBody] USER user)
        {
            var query = from result in dc.USERs
                        where usrid == result.USER_ID
                        select result;

            foreach (USER usr in query)
            {
                usr.FIRST_NAME = user.FIRST_NAME;
                usr.LAST_NAME = user.LAST_NAME;
                usr.GENDER = user.GENDER;
                usr.EMAIL = user.EMAIL;
                usr.PHONE = user.PHONE;
                usr.DOB = user.DOB;
                usr.CITY = user.CITY;
                usr.STATE = user.STATE;
                usr.ZIPCODE = user.ZIPCODE;
                usr.ADDRESS = user.ADDRESS;
                usr.PROFILEPIC = user.PROFILEPIC;
            }

            // Submit the changes to the database. 
            try
            {
                dc.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Provide for exceptions.
            }

            return ResponseMsg();
        }

        // DELETE api/users/5
        [Route("deleteUser/{usrid?}")]
        [HttpPost]
        [AcceptVerbs("GET", "POST")]
        public HttpResponseMessage Delete(string usrid)
        {
            var user = (from result in dc.USERs
                        where usrid == result.USER_ID
                       select result).First();

            dc.USERs.DeleteOnSubmit(user);

            try
            {
                dc.SubmitChanges();
            }
            catch (Exception ex)
            {
                
            }

            return ResponseMsg();
        }
    }
}
