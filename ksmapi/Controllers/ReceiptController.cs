using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.object_class;
using Business.base_class;

namespace ksmapi.Controllers
{
    public class ReceiptController : ApiController
    {
        // GET api/<controller>
        [HttpPost]
        public IHttpActionResult GetAll([FromBody] business_base_class b) 
        {

            try
            {
                if (cls_token.validate(b))
                {
                    long loginid = cls_token.GetLoginId(b.Token.Key).Value;
                    long idcompany = cls_login.GetCompanyIdByIdLogin(loginid);
                    return Ok(cls_receipt_type.GetAll());

                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                log.insertLog(e,0, 0);
                return InternalServerError(e);
            }


          
        }
        
    }
}