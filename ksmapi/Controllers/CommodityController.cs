using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Business.base_class;
using Business.object_class;

namespace ksmapi.Controllers
{

   
    
    public class CommodityController : ApiController
    {
        [HttpPost]
        public IHttpActionResult getList([FromBody] Business.base_class.filter_request fr) 
        {
            if (Business.base_class.cls_token.validate(fr))
            {
                try
                {
                    long loginid = cls_token.GetLoginId(fr.Token.Key).Value;
                    long idcompany = cls_login.GetCompanyIdByIdLogin(loginid);
                    filter_paged_response cl = cls_commodity.GetCommodities(idcompany, fr);
                    return Ok(cl);
                }
                catch (Exception e) 
                {
                    log.insertLog(e, 0, 0);
                    return InternalServerError(e);
                }

            }
            else 
            {
                return Unauthorized();
            }

        }
      
    }
}
