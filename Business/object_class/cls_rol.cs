using Business.base_class;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.object_class
{
    public class cls_rol:base_class.business_base_class
    {
        public cls_rol() { }

        public cls_rol(usp_GetRoles_Result r) 
        {
            Name = r.NAME;
            Id = r.ID;
        }

        public cls_rol(usp_GetRolesByUserId_Result r) 
        {
            Id = r.IDROLE.Value;
            Name = r.NAME;

        }

        public cls_rol(usp_GetRolesByLoginId_Result r)
        {
            Id = r.IDROLE;
            Name = r.NAME;
        }

        public static filter_paged_response GetRoles(long idcompany) 
        {
            MSKEntities e = Data.singleton.cls_static_MksModel.GetEntity();
            List<usp_GetRoles_Result> roles = e.usp_GetRoles(idcompany).ToList();
            filter_paged_response r = new filter_paged_response();
            if (roles != null && roles.Count > 0) 
            {
                List<cls_rol> rolelist = new List<cls_rol>();
                foreach (usp_GetRoles_Result a in roles) 
                {
                    rolelist.Add(new cls_rol(a));
                }
                r.RoleList = rolelist;
            }
            return r;
        }

        public static List<cls_rol> GetRolesByUserId(long userid, long companyid) 
        {
            MSKEntities ent = Data.singleton.cls_static_MksModel.GetEntity();
            List<usp_GetRolesByUserId_Result> r = ent.usp_GetRolesByUserId(userid, companyid).ToList();
            if (r != null && r.Count > 0)
            {
                List<cls_rol> roleslist = new List<cls_rol>();
                foreach (usp_GetRolesByUserId_Result role in r)
                {
                    roleslist.Add(new cls_rol(role));
                }
                return roleslist;
            }
            else 
            {
                return null;
            }
        }
    }
}
