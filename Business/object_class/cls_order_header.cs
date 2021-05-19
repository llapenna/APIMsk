using Business.base_class;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.object_class
{
    /*
     * {
  "Token": {
    "Key": "blablabla"
  },
  "IdCustomer": 1,
  "Detail": [
    {
      "IdCommodity": 1,
      "Amount": 100,
      "Price": 890
    },
    {
      "IdCommodity": 1,
      "Amount": 100,
      "Price": 890
    }
  ]
}
     */
    public class cls_order_header:base_class.business_base_class
    {
        long idCompany;
        long idCustomer;
        long idUser;
        DateTime date;
        bool transmited;
        List<cls_order_detail> detail;
        string customerName;
        string customerCUIT;
        string observation;
        decimal discount;

        public long IdCompany { get => idCompany; set => idCompany = value; }
        public DateTime Date { get => date; set => date = value; }
        public bool Transmited { get => transmited; set => transmited = value; }
        public List<cls_order_detail> Detail { get => detail; set => detail = value; }
        public long IdCustomer { get => idCustomer; set => idCustomer = value; }
        public string CustomerName { get => customerName; set => customerName = value; }
        public string CustomerCUIT { get => customerCUIT; set => customerCUIT = value; }
        public long IdUser { get => idUser; set => idUser = value; }
        public string Observation { get => observation; set => observation = value; }
        public decimal Discount { get => discount; set => discount = value; }

        /// <summary>
        /// For new orders
        /// </summary>
        /// <param name="par_id_company"></param>
        public cls_order_header(long par_id_user, long par_id_company, long par_idCustomer, string par_observations, decimal par_discount) 
        {
            idCustomer = par_idCustomer;
            idCompany = par_id_company;
            idUser = par_id_user;
            date = DateTime.Now;
            transmited = false;
            detail = new List<cls_order_detail>();
            Id = 0;
            observation = par_observations;
            discount = par_discount;
        }

        public cls_order_header(usp_GetAllOrders_Result r, int par_idcompany) 
        {
            date = r.DATETIME;
            customerName = r.CUSTOMER;
            Id = r.ID;
            detail = cls_order_detail.GetByOrderId(Id,par_idcompany);
            customerCUIT = r.CUSTOMERCUIT;
            idCustomer = r.CUSTOMERINTERNALID!=null?r.CUSTOMERINTERNALID.Value:0;
            observation = r.OBSERVATIONS;
            discount = r.DISCOUNT!=null?r.DISCOUNT.Value:0;
            
        }

        public cls_order_header(usp_GetSingleOrder_Result r, long par_company) 
        {
            date = r.DATETIME;
            customerName = r.CUSTOMER;
            Id = r.ID;
            detail = cls_order_detail.GetByOrderId(Id, par_company);
            customerCUIT = r.CUSTOMERCUIT;
            idCustomer = r.CUSTOMERID.Value;
            discount = r.DISCOUNT!=null?r.DISCOUNT.Value:0;
            observation = r.OBSERVATIONS;
            
        }

        public cls_order_header(usp_GetAllOrdersByUser_Result r)
        {
            Id = r.ID;
            date = r.DATETIME;
            detail = cls_order_detail.Get(r.ID);
            customerName = r.CUSTOMER;
            customerCUIT = r.CUSTOMERCUIT;
            
        }

        public cls_order_header(usp_GetAllOrdersByCompany_Result r)
        {
            Id = r.ID;
            date = r.DATETIME;
            detail = cls_order_detail.Get(r.ID);
            customerName = r.CUSTOMER;
            customerCUIT = r.CUSTOMERCUIT;
            
        }

        public static List<cls_order_header> GetSingleOrder (long par_idOrder, long par_id_company) 
        {
            MSKEntities msk = Data.singleton.cls_static_MksModel.GetEntity();
            List<usp_GetSingleOrder_Result> list = msk.usp_GetSingleOrder(par_id_company, par_idOrder).ToList();
            List<cls_order_header> myList = new List<cls_order_header>();
            if (list != null && list.Count > 0) 
            {
                cls_order_header order = new cls_order_header(list[0], par_id_company);
                myList.Add(order);

            }
            return myList;

        }

        public static bool deleteOrder(long idorder) 
        {
            MSKEntities msk = Data.singleton.cls_static_MksModel.GetEntity();
            msk.usp_deleteOrder(idorder);
            return true;
        }
        

        public void InsertDetail(long par_idComodity, decimal par_amount, decimal par_price, bool par_noUnit) 
        {

            MSKEntities msk = Data.singleton.cls_static_MksModel.GetEntity();
            List<usp_GetUnitByIdComodity_Result> list = msk.usp_GetUnitByIdComodity(par_idComodity).ToList();
            long idunit = (((list!=null)&&(list.Count>0))&&list[0].IDUNIT!=null)?list[0].IDUNIT.Value:0;
            Detail.Add(new cls_order_detail(idunit, idCompany, Id, par_idComodity, par_amount, par_price, par_noUnit));

        }

        public void Save() 
        {
            MSKEntities msk = Data.singleton.cls_static_MksModel.GetEntity();
            Id = msk.ups_InsertOrderHeader(IdUser, IdCustomer,IdCompany, observation, discount).ToList()[0].Value;
            foreach (cls_order_detail d in detail) 
            {
                d.Unit = d.Unit;
                d.IdHeader = Id;
                d.save();
            }
        }

        public static string GetOrderPlainText(int idcompany)
        {
            
            string response="";
            try
            {
                List<cls_order_header> OrderList =GetAllByCompanyId(idcompany);
                response = "[HEADERS][ENDLINE]";

                foreach (cls_order_header oh in OrderList)
                {
                    response += oh.Id + "|" + oh.date.ToString("yyyyMMdd") + "|" + oh.CustomerCUIT + "|" + oh.IdCustomer.ToString() + "|" + oh.CustomerName + "[ENDLINE]";

                }

                response += "[DETAIL][ENDLINE]";
                foreach (cls_order_header oh in OrderList)
                {
                    if (oh.detail != null && oh.detail.Count > 0)
                    {
                        foreach (cls_order_detail od in oh.Detail)
                        {
                            response += oh.Id.ToString() + "|" + od.InternalCode + "|" + (od.NoUnit == true ? "1" : "0") + "|" + od.Amount.ToString() + "|" + od.Price + "[ENDLINE]";
                        }
                    }
                }
                response += "[ENDFILE][ENDLINE]";
            }
            catch (Exception e) 
            {
                response = "";
                response += e.Message + "\n" + e.StackTrace;
            }
            return response;

        }

        public static void TransmitOrders(int idCompany) 
        {
            MSKEntities msk = Data.singleton.cls_static_MksModel.GetEntity();
            msk.TransmitOrder(idCompany);
        }

        public static List<cls_order_header> getAllOrders() 
        {
            MSKEntities msk = Data.singleton.cls_static_MksModel.GetEntity();
            List<usp_GetAllOrders_Result> orderlist = msk.usp_GetAllOrders().ToList();
            List<cls_order_header> list = new List<cls_order_header>();
            if (orderlist != null && orderlist.Count > 0)
            {               
                foreach (usp_GetAllOrders_Result or in orderlist)
                {
                    /*list.Add(new cls_order_header(or, loginid));*/
                    //Arroja vacio es de superadmin... corregir... fadlta constructor con la clase GetAllOrderResult
                }
                return list;
            }
            else 
            {
                return list;
            }


        }

        public static List<cls_order_header> GetAllByUserId(long userid)
        {
            MSKEntities msk = Data.singleton.cls_static_MksModel.GetEntity();
            List<usp_GetAllOrdersByUser_Result> r = msk.usp_GetAllOrdersByUser(userid).ToList();

            List<cls_order_header> orders = new List<cls_order_header>();

            if (r != null && r.Count > 0)
            {
                foreach (usp_GetAllOrdersByUser_Result order in r)
                    orders.Add(new cls_order_header(order));
            }

            return orders;
        }

        public static List<cls_order_header> GetAllByCompanyId(long companyid)
        {
            MSKEntities msk = Data.singleton.cls_static_MksModel.GetEntity();
            List<usp_GetAllOrdersByCompany_Result> r = msk.usp_GetAllOrdersByCompany(companyid).ToList();

            List<cls_order_header> orders = new List<cls_order_header>();

            if (r != null && r.Count > 0)
            {
                foreach (usp_GetAllOrdersByCompany_Result order in r)
                    orders.Add(new cls_order_header(order));
            }

            return orders;
        }
        

    }
}
