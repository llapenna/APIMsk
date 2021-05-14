using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.base_class
{
    public class business_base_class
    {
        long id;
        string name;
        cls_token token;


        public long Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public cls_token Token { get => token; set => token = value; }


        
    }
}
