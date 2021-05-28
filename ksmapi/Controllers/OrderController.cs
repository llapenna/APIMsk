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

        // TODO: Cambiar id_company por id_user
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
                        cls_order_header oh = new cls_order_header(loginid, idcompany, r.IdCustomer, r.Observation, r.Discount, r.Receipt_type);

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
                log.insertLog(e, 0, 0);
                return InternalServerError(e);
            }
        }
        [HttpPost]
        public IHttpActionResult UpdateOrder([FromBody] Business.object_class.cls_temporalUpdatePedido r)
        {
            try
            {
                if (cls_token.validate(r))
                {
                    long loginid = cls_token.GetLoginId(r.Token.Key).Value;
                    long idcompany = cls_login.GetCompanyIdByIdLogin(loginid);
                    long idcustomer = cls_order_header.GetSingleOrder(r.OrderId, idcompany)[0].IdCustomer;

                    if (r.Detail != null && r.Detail.Count > 0)
                    {
                        cls_order_header oh = new cls_order_header(loginid, idcompany, idcustomer, r.Observation, r.Discount,r.Receipt_type);

                        foreach (cls_temporalUpdatePedidoDetail det in r.Detail)
                        {
                            decimal amount;
                            oh.InsertDetail(det.Id, decimal.TryParse(det.Sellcant, out amount) == true ? amount : 0, det.Precio, det.Nounit);
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
                log.insertLog(e, 0, 0);
                return InternalServerError(e);
            }
        }


        [HttpPost]
        public IHttpActionResult GetOrder([FromBody] order_request r)
        {
            try
            {
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
            try
            {

                if (cls_token.validate(c))
                {
                    long loginid = cls_token.GetLoginId(c.Token.Key).Value;

                    // Obtenemos la informacion del usuario que realiza la consulta
                    cls_login user = cls_login.Get(loginid);

                    List<cls_order_header> orderlist = new List<cls_order_header>();

                    // Muestra todos los pedidos de la compañia
                    if (user.Roles[0].Name == "Administrator" || user.Roles[0].Name == "Administrador")
                    {
                        long idcompany = cls_login.GetCompanyIdByIdLogin(loginid);
                        orderlist = cls_order_header.GetAllByCompanyId(idcompany);

                    }
                    // Muestra todos los pedidos del usuario
                    else
                    {
                        orderlist = cls_order_header.GetAllByUserId(user.Id);
                    }
                    return Ok(orderlist);

                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }
    }
}
