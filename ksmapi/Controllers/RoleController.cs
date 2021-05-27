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
    public class RoleController : ApiController
    {
        [HttpPost]
        public IHttpActionResult GetAll([FromBody] Business.base_class.business_base_class c) 
        {
            try { 
            if (Business.base_class.cls_token.validate(c)) {
                long loginid = cls_token.GetLoginId(c.Token.Key).Value;
                long idcompany = cls_login.GetCompanyIdByIdLogin(loginid);
                filter_paged_response r = cls_rol.GetRoles(idcompany);
                return Ok(r);
            } else 
            {
                return Unauthorized();
            }
            }
            catch (Exception e)
            {
                log.insertLog(e, 0, 0);
                return InternalServerError(e);
            }
        }


    }
}