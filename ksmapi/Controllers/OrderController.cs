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
    public class OrderController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Insert([FromBody] Business.base_class.order_request r) 
        {
            try
            {
                if (cls_token.validate(r))
                {
                    long loginid = cls_token.GetLoginId(r.Token.Key).Value;
                    long idcompany = cls_login.GetCompanyIdByIdLogin(loginid);
                    if (r.Detail != null && r.Detail.Count > 0)
                    {
                        cls_order_header oh = new cls_order_header(idcompany, r.IdCustomer);

                        foreach (order_detail_request det in r.Detail)
                        {
                            oh.InsertDetail(det.IdCommodity, det.Amount, det.Price, det.NoUnit);
                        }
                        oh.Save();
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("El pedido no contiene detalle");
                    }

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
        [HttpPost]
        public IHttpActionResult UpdateOrder([FromBody] Business.object_class.cls_temporalUpdatePedido r)
        {
            try { 
            if (cls_token.validate(r))
            {
                long loginid = cls_token.GetLoginId(r.Token.Key).Value;
                long idcompany = cls_login.GetCompanyIdByIdLogin(loginid);
                long idcustomer = cls_order_header.GetSingleOrder(r.OrderId, idcompany)[0].IdCustomer;
                
                if (r.Detail != null && r.Detail.Count > 0)
                {
                    cls_order_header oh = new cls_order_header(idcompany, idcustomer);
                    
                    foreach (cls_temporalUpdatePedidoDetail det in r.Detail)
                    {
                        decimal priceparsing;
                        oh.InsertDetail(det.Id, decimal.TryParse(det.Sellcant,out priceparsing)==true?priceparsing:0, det.Precio, det.Nounit);
                    }
                    oh.Save();
                    cls_order_header.deleteOrder(r.OrderId);
                    return Ok();
                }
                else
                {
                    return BadRequest("El pedido no contiene detalle");
                }

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


        [HttpPost] 
        public IHttpActionResult GetOrder([FromBody] order_request r) 
        {
            try { 
            if (cls_token.validate(r))
            {
                long loginid = cls_token.GetLoginId(r.Token.Key).Value;
                long idcompany = cls_login.GetCompanyIdByIdLogin(loginid);
                List<cls_order_header> order = cls_order_header.GetSingleOrder(r.OrderId, idcompany);
                return Ok(order);
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
        [HttpPost]
        public IHttpActionResult RemoveOrder([FromBody] business_base_class c) 
        {
            try
            {
                if (cls_token.validate(c))
                {
                    long loginid = cls_token.GetLoginId(c.Token.Key).Value;
                    long idcompany = cls_login.GetCompanyIdByIdLogin(loginid);
                    cls_order_header.deleteOrder(c.Id);
                    return Ok(c);

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
        [HttpPost]
        public IHttpActionResult GetAll([FromBody] business_base_class c) 
        {
            try { 
            if (cls_token.validate(c))
            {
                long loginid = cls_token.GetLoginId(c.Token.Key).Value;
                long idcompany = cls_login.GetCompanyIdByIdLogin(loginid);
                List<cls_order_header> orderlist = cls_order_header.getAll(int.Parse(idcompany.ToString()));
                return Ok(orderlist);

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
