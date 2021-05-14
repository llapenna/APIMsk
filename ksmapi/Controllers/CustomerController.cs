using Business.base_class;
using Business.object_class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ksmapi.Controllers
{
    public class CustomerController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Insert([FromBody] cls_customer c) 
        {
            if (cls_token.validate(c))
            {
                long loginid = cls_token.GetLoginId(c.Token.Key).Value;
                long idcompany = cls_login.GetCompanyIdByIdLogin(loginid);
                cls_customer customer = c;
                c.Id_company = idcompany;
                customer.save();
                return Ok();
            }
            else 
            {
                return Unauthorized();
            }
        }
        // GET api/<controller>
        [HttpPost]
        public IHttpActionResult GetList(Business.base_class.filter_request c) 
        {
            try
            {
                if (cls_token.validate(c))
                {
                    long loginid = cls_token.GetLoginId(c.Token.Key).Value;
                    long idcompany = cls_login.GetCompanyIdByIdLogin(loginid);
                    filter_paged_response cl = cls_customer.GetCustomers(idcompany, c);
                    return Ok(cl);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e) 
            {
                return InternalServerError(e);
            }
        }

        public IHttpActionResult GetByInternalId(business_base_class c) 
        {
            try
            {
                if (cls_token.validate(c))
                {
                    long loginid = cls_token.GetLoginId(c.Token.Key).Value;
                    long idcompany = cls_login.GetCompanyIdByIdLogin(loginid);
                    cls_customer cust = cls_customer.GetCustomerByInternalID(c.Id);
                    return Ok(cust);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}