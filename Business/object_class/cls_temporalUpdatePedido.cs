using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.object_class
{
    public class cls_temporalUpdatePedidoDetail
    {
        long id;
        string codigo;
        string descripcion;
        decimal precio;
        string unit;
        bool nounit;
        string sellcant;

        public long Id { get => id; set => id = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public string Unit { get => unit; set => unit = value; }
        public bool Nounit { get => nounit; set => nounit = value; }
        public string Sellcant { get => sellcant; set => sellcant = value; }
        
    }
    public class cls_temporalUpdatePedido:base_class.business_base_class
    {
        List<cls_temporalUpdatePedidoDetail> detail;
        long orderId;
        string observation;
        decimal discount;
        long receipt_type;

        public List<cls_temporalUpdatePedidoDetail> Detail { get => detail; set => detail = value; }
        public long OrderId { get => orderId; set => orderId = value; }
        public string Observation { get => observation; set => observation = value; }
        public decimal Discount { get => discount; set => discount = value; }
        public long Receipt_type { get => receipt_type; set => receipt_type = value; }
    }
}
