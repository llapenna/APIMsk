using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Business.base_class;
using Business.object_class;

namespace ksmapi.Controllers
{
    [EnableCors(origins:"*",headers:"*", methods:"*")]
    public class LoginController : ApiController
    {

        [HttpPost]
        public IHttpActionResult Insert([FromBody] cls_login c) 
        {
            try
            {
                if (Business.base_class.cls_token.validate(c))
                {
                    long loginid = cls_token.GetLoginId(c.Token.Key).Value;
                    long idcompany = cls_login.GetCompanyIdByIdLogin(loginid);
                    filter_paged_response r = cls_login.insertLogin(c, idcompany);
                    return Ok(r);
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

        [HttpPost]
        public IHttpActionResult GetAll([FromBody] business_base_class c) 
        {
            try
            {

                if (Business.base_class.cls_token.validate(c))
                {
                    try
                    {
                        long loginid = cls_token.GetLoginId(c.Token.Key).Value;
                        long idcompany = cls_login.GetCompanyIdByIdLogin(loginid);

                        filter_paged_response r = cls_login.GetLogins(idcompany);
                        return Ok(r);
                    }
                    catch (Exception e)
                    {
                        log.insertLog(e, 0, 0);
                        filter_paged_response fpr = new filter_paged_response();
                        fpr.Debug.Add(e.Message);
                        fpr.Debug.Add(e.StackTrace);
                        return Ok(e);

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
                filter_paged_response fpr = new filter_paged_response();
                fpr.Debug.Add(e.Message);
                fpr.Debug.Add(e.StackTrace);
                return InternalServerError(e);
            }
        }

        [HttpPost]
        public IHttpActionResult Login([FromBody] cls_login parLogin) 
        {
          try { 
            cls_login login = parLogin.Login();
            
            if (login != null)
            {
                
                return Ok(login);
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

        [HttpPost]
        public IHttpActionResult CheckLogin([FromBody] cls_login parLogin) 
        {
             try { 
                bool result = cls_token.validate(parLogin);
                if (result == true)
                {
                     

                    return Ok(true);
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
        [HttpGet]
        public IHttpActionResult Prueba() 
        {
            return Ok("Hola Losha");
        }

      
        

    }
}
