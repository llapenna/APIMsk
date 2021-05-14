using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.singleton
{
    public static class cls_static_MksModel
    {
        public static MSKEntities entity;
        public static MSKEntities GetEntity() 
        {
            if (entity == null) 
            {
                return new MSKEntities();
            } else
            {
                System.Data.ConnectionState Estado = entity.Database.Connection.State;
                switch (Estado) 
                {
                    case System.Data.ConnectionState.Open:return entity;
                    case System.Data.ConnectionState.Closed: { entity.Dispose(); return new MSKEntities(); };
                    case System.Data.ConnectionState.Broken: { entity.Dispose(); return new MSKEntities(); };
                    default: return new MSKEntities();
                }
            }
            
        }
    }
}
