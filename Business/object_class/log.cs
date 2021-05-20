using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Data.singleton;

namespace Business.object_class
{
    public class log:base_class.business_base_class
    {

        string message;
        DateTime datetime;
        string stack;
        long idcompany;
        long iduser;

        public log(getLog_Result lr) 
        {
            Id = lr.id;
            message = lr.Message;
            datetime = lr.datetime;
            stack = lr.Stack;
            idcompany = lr.id_company;
            iduser = lr.id_user;
            
        }

        public string Message { get => message; set => message = value; }
        public DateTime Datetime { get => datetime; set => datetime = value; }
        public string Stack { get => stack; set => stack = value; }
        public long Idcompany { get => idcompany; set => idcompany = value; }
        public long Iduser { get => iduser; set => iduser = value; }

        public static void insertLog(Exception e, long piduser, long pidcompany) 
        {
            MSKEntities E = cls_static_MksModel.GetEntity();
            E.insert_log(e.Message, e.StackTrace, piduser, pidcompany);
        }

        public static List<log> getAll() 
        {
            MSKEntities E = cls_static_MksModel.GetEntity();
            List<getLog_Result> lr = E.getLog().ToList();
            List<log> l = new List<log>();
            if (lr != null && lr.Count > 0) 
            {
                foreach (getLog_Result r in lr) 
                {
                    l.Add(new log(r));
                }
            }
            return l;

        }
    }
}
