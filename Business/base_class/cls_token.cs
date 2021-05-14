using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.singleton;

namespace Business.base_class
{
    public class cls_token
    {
        private string token=String.Empty;
        

        public string Key { get => token; set => token = value; }
        
        
        public cls_token(string parToken) 
        {
            token = parToken;
        }
        
        public static bool validate(business_base_class parToken) 
        {
            if (parToken != null && parToken.Token!=null && parToken.Token.Key != null && parToken.Token.Key != String.Empty)
            {
                ObjectResult<bool?> response = cls_static_MksModel.GetEntity().usp_CheckToken(parToken.Token.Key);
                return response.ToArray()[0].Value;
            }
            else 
            {
                return false;
            }
        }

        public static cls_token newToken(int parUserID) 
        {
            ObjectResult<string> result = cls_static_MksModel.GetEntity().usp_InsertToken(parUserID);
            cls_token token = new cls_token(result.ToList()[0]);
            return token;
        }

        public static long? GetLoginId(string Token) 
        {
            ObjectResult<long?> result = cls_static_MksModel.GetEntity().usp_GetLoginIdByToken(Token);
            List<long?> r = result.ToList();
            if (r != null && r.Count > 0) {
                return r[0].HasValue ? r[0] : null;
            } else return null;
        }

    }
}
