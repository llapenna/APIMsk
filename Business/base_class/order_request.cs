using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.base_class
{
    public class order_detail_request 
    {
        long idCommodity;
        decimal amount;
        decimal price;
        bool noUnit;
        

        public long IdCommodity { get => idCommodity; set => idCommodity = value; }
        public decimal Amount { get => amount; set => amount = value; }
        public decimal Price { get => price; set => price = value; }
        public bool NoUnit { get => noUnit; set => noUnit = value; }
    }
    public class order_request : business_base_class
    {
        long idClient;
        List<order_detail_request> detail;
        long orderId;
        string observation;
        decimal discount;
        long receipt_type;


        public long IdCustomer { get => idClient; set => idClient = value; }
        public List<order_detail_request> Detail { get => detail; set => detail = value; }
        public long OrderId { get => orderId; set => orderId = value; }
        public string Observation { get => observation; set => observation = value; }
        public decimal Discount { get => discount; set => discount = value; }
        public long Receipt_type { get => receipt_type; set => receipt_type = value; }
    }
}
