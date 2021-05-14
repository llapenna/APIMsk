using Business.object_class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ksmapi.Controllers
{
    public class SyncController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage GetOrders() 
        {
            HttpResponseMessage rm = new HttpResponseMessage(HttpStatusCode.OK);
            Task<byte[]> s = Request.Content.ReadAsByteArrayAsync();
            s.Wait();
            byte[] result = s.Result;
            string finalResult = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(result);
            
            string companystring = finalResult.Split(new string[] { "[ENDLINE]" },StringSplitOptions.None)[0];
            long idcompany = long.Parse(cls_company.GetIdCompanyByCUIT(companystring).Value.ToString());
            rm.Content = new StringContent(cls_order_header.GetOrderPlainText(int.Parse(idcompany.ToString())), System.Text.Encoding.UTF8, "text/plain");
            cls_order_header.TransmitOrders(int.Parse(idcompany.ToString()));
            return rm;
        }

     /*   [HttpPost]
        public HttpResponseMessage GetOrdersDetail()
        {
            
        }*/

        [HttpPost]
        public IHttpActionResult ImportClient()
        {
            try { 
            Task<byte[]> s = Request.Content.ReadAsByteArrayAsync();
            s.Wait();
            byte[] result = s.Result;
            string finalResult = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(result);
            string companystring = finalResult.Split(new string[] { "[ENDLINE]"},StringSplitOptions.None)[0].Split(new string[] {"|"},StringSplitOptions.None)[15];
            long idcompany = long.Parse(cls_company.GetIdCompanyByCUIT(companystring).Value.ToString());
            int recorCount = Business.object_class.cls_customer.ProcessPage(finalResult, int.Parse(idcompany.ToString()));

            return Ok("El comando se completo con exito: " + recorCount + " registros procesados");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        public IHttpActionResult ImportCommodity()
        {
            try { 
            Task<string> s = Request.Content.ReadAsStringAsync();
            s.Wait();
            string result = s.Result;
            string companystring = result.Split(new string[] { "[ENDLINE]" }, StringSplitOptions.None)[0].Split(new string[] { "|" }, StringSplitOptions.None)[9];
            long idcompany = long.Parse(cls_company.GetIdCompanyByCUIT(companystring).Value.ToString());
            cls_commodity.processPage(result,int.Parse(idcompany.ToString()));
            /*File.WriteAllText(System.Web.Hosting.HostingEnvironment.MapPath("/") + "Commodity-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".neo", result,System.Text.Encoding.UTF8);*/
            return Ok("El comando se completo con exito");
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPost]
        public IHttpActionResult ImportPrice()
        {
            try { 
            Task<string> s = Request.Content.ReadAsStringAsync();
            s.Wait();
            string result = s.Result;
            string companystring = result.Split(new string[] { "[ENDLINE]" }, StringSplitOptions.None)[0].Split(new string[] { "|" }, StringSplitOptions.None)[4];
            long idcompany = long.Parse(cls_company.GetIdCompanyByCUIT(companystring).Value.ToString());


            cls_price.processPage(result, int.Parse(idcompany.ToString()));
            File.WriteAllText(System.Web.Hosting.HostingEnvironment.MapPath("/") + "Price-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".neo", result, System.Text.Encoding.UTF8);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
            return Ok("El comando se completo con exito");
        }
    }
}