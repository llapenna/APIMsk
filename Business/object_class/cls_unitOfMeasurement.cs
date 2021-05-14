using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.object_class
{
    public class cls_unitOfMeasurement:base_class.business_base_class
    {
        bool showDecimal;

        public bool ShowDecimal { get => showDecimal; set => showDecimal = value; }
    }
}
