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

        public long IdCustomer { get => idClient; set => idClient = value; }
        public List<order_detail_request> Detail { get => detail; set => detail = value; }
        public long OrderId { get => orderId; set => orderId = value; }
    }
}
