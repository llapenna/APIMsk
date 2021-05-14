using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.object_class
{
    public class cls_company:base_class.business_base_class
    {
        public static long? GetIdCompanyByCUIT(string cuit) 
        {
            Data.MSKEntities E = Data.singleton.cls_static_MksModel.GetEntity();
            List<long?> IdCompany = E.SelectIdCompanyByCUIT(cuit).ToList();
            return IdCompany[0];
        }
    }
}
