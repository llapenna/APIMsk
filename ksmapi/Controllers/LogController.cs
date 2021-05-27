using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace ksmapi.Controllers
{
    public class LogController : ApiController
    {
        // GET: Log
        [HttpGet]
        public  IHttpActionResult GetLogs() 
        {
            List<Business.object_class.log> L = Business.object_class.log.getAll();
            return Ok(L);
        }
    }
}