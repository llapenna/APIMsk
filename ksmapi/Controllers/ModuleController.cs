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
    public class ModuleController : ApiController
    {
        [HttpPost]
        public IHttpActionResult GetModules([FromBody] business_base_class _auth) 
        {
            try { 
            if (cls_token.validate(_auth))
            {
                long? iduser = cls_token.GetLoginId(_auth.Token.Key);
                if (iduser != null)
                {
                    List<cls_module> modulelist = cls_module.GetModules(iduser.Value);
                    if (modulelist != null)
                    {
                        return Ok(modulelist);
                    }
                    else
                    {
                        return Ok("null");
                    }
                }
                else
                {
                    return Ok("null");
                }
            }
            else 
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