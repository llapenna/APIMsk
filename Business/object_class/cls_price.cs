using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.object_class
{
    public class cls_price:base_class.business_base_class
    {
        public static int processPage(string Page, int idCompany) 
        {
            int count = 0;
            try
            {
                Data.MSKEntities E = Data.singleton.cls_static_MksModel.GetEntity();
                string[] lineas = Page.Split(new string[] { "[ENDLINE]" }, StringSplitOptions.None);
                foreach (string l in lineas)
                {
                    string[] campos = l.Split(new string[] { "|" }, StringSplitOptions.None);
                    E.InsertPrice(long.Parse(campos[1]), decimal.Parse(campos[2]), idCompany);
                    count++;
                }
            }
            catch (Exception e) 
            {

            }
            return count;
        }
    }
}
