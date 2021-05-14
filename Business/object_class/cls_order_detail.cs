using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.singleton;

namespace Business.object_class
{
    public class cls_order_detail:base_class.business_base_class
    {
        long idHeader;
        long idComodity;
        long idCompany;
        long idUnit;
        decimal amount;
        decimal price;
        string commodityName;
        bool noUnit = false;
        string internalCode;
        string unit;
        decimal avgWeight;
        cls_unitOfMeasurement measurement_unit;

        public long IdComodity { get => idComodity; set => idComodity = value; }
        public decimal Amount { get => amount; set => amount = value; }
        public decimal Price { get => price; set => price = value; }
        public long IdHeader { get => idHeader; set => idHeader = value; }
        public long IdCompany { get => idCompany; set => idCompany = value; }
        public long IdUnit { get => idUnit; set => idUnit = value; }
        public string CommodityName { get => commodityName; set => commodityName = value; }
        public bool NoUnit { get => noUnit; set => noUnit = value; }
        public string InternalCode { get => internalCode; set => internalCode = value; }
        public cls_unitOfMeasurement Measurement_unit { get => measurement_unit; set => measurement_unit = value; }
        public string Unit { get => unit; set => unit = value; }
        public decimal AvgWeight { get => avgWeight; set => avgWeight = value; }

        public cls_order_detail(long par_idUnit, long par_idCompany,long par_idHeader, long par_idComodity, decimal par_amount, decimal par_price, bool par_nounit) 
        {
            idHeader = par_idHeader;
            IdComodity = par_idComodity;
            amount = par_amount;
            price = par_price;
            idCompany = par_idCompany;
            idUnit = par_idUnit;
            noUnit = par_nounit;
        }


        

        public cls_order_detail(usp_GetOrderDetail_Result r) 
        {
            idComodity = r.IDCOMMODITY;
            amount = r.AMOUNT;
            commodityName = r.NAME;
            price = r.PRICE;
            noUnit = r.NOUNIT == null ? false : r.NOUNIT.Value;
            internalCode = r.INTERNALCODE;
            unit = r.UNIT;
            avgWeight = r.AVERAGEWEIGHT ?? 0 ;

        }

        public static List<cls_order_detail> GetByOrderId(long orderid, long id_company) 
        {
            MSKEntities E = cls_static_MksModel.GetEntity();
            List<usp_GetOrderDetail_Result> list = E.usp_GetOrderDetail(id_company,orderid).ToList();
            if (list != null && list.Count > 0)
            {
                List<cls_order_detail> det = new List<cls_order_detail>();
                foreach (usp_GetOrderDetail_Result d in list)
                {
                    det.Add(new cls_order_detail(d));
                }
                return det;
            }
            else 
            {
                return null;
            }
        }


        public void save() 
        {
            MSKEntities msk = Data.singleton.cls_static_MksModel.GetEntity();
            msk.ups_InsertOrderDetail(idCompany, IdHeader,IdComodity, Amount, idUnit ,price,noUnit);
        }

    }
}
