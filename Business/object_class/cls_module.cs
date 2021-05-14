using Business.base_class;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.object_class
{
    public class cls_module: business_base_class
    {

        private long permissionId;
        private string icon;
        private string section;
        private long sectionid;


        public long PermissionId { get => permissionId; set => permissionId = value; }
        public string Icon { get => icon; set => icon = value; }
        public string Section { get => section; set => section = value; }
        public long SectionId { get => sectionid; set => sectionid = value; }

        private cls_module(GetUserModules_Result parObject) 
        {
            Name = parObject.name;
            Id = parObject.id;
            permissionId = parObject.id_permission;
            icon = parObject.icon;
            MSKEntities E = Data.singleton.cls_static_MksModel.GetEntity();
            List<usp_GetSectionByIdModule_Result> rl = E.usp_GetSectionByIdModule(parObject.id).ToList();
            if (rl != null && rl.Count > 0)
            {
                Section = rl[0].name;
                SectionId = rl[0].id;
            }
            else 
            {
                Section = "";
                SectionId = 0;
            }
        }


        public static List<cls_module> GetModules(long parIdUser) 
        {
            MSKEntities E = Data.singleton.cls_static_MksModel.GetEntity();
            List<GetUserModules_Result> R = E.GetUserModules(parIdUser).ToList();
            if (R != null && R.Count > 0)
            {
                List<cls_module> t_list = new List<cls_module>();
                foreach (GetUserModules_Result row in R) 
                {
                    t_list.Add(new cls_module(row));
                }
                return t_list;
            }
            else 
            {
                return null;
            }

        }
    }
}
