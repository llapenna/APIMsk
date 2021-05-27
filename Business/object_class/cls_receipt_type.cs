using Business.base_class;
using Data;
using Data.singleton;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace Business.object_class
{
    public class cls_receipt_type:business_base_class
    {

        public cls_receipt_type(usp_GetReceiptType_Result row) 
        {
            Id = row.id;
            Name = row.name;
        }

        public cls_receipt_type(usp_GetReciptTypeByid_Result row)
        {
            Id = row.id;
            Name = row.name;
        }

        public static List<cls_receipt_type> GetAll()
        {
            List<cls_receipt_type> rt = new List<cls_receipt_type>();

            MSKEntities E = cls_static_MksModel.GetEntity();
            List<usp_GetReceiptType_Result> rl =  E.usp_GetReceiptType().ToList();
            if (rl != null && rl.Count > 0) 
            {
                foreach (usp_GetReceiptType_Result r in rl) 
                {
                    rt.Add(new cls_receipt_type(r));
                }
            }

            return rt;
        }

        public static cls_receipt_type GetById(long id) 
        {
            List<cls_receipt_type> rt = new List<cls_receipt_type>();

            MSKEntities E = cls_static_MksModel.GetEntity();
            List<usp_GetReciptTypeByid_Result> r = E.usp_GetReciptTypeByid(id).ToList();
            if (r != null && r.Count > 0)
            {
                return new cls_receipt_type(r[0]);

            }
            else 
            {
                return null;
            }
        }
    }
}