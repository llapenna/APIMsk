using Business.object_class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.base_class
{
    public class filter_paged_response
    {
        int maxPage;
        int totalInPage;
        List<cls_customer> customerList = new List<cls_customer>();
        List<cls_commodity> commodityList = new List<cls_commodity>();
        List<cls_order_header> iorderList = new List<cls_order_header>();
        List<cls_login> loginList = new List<cls_login>();
        List<cls_rol> roleList = new List<cls_rol>();
        List<String> debug = new List<string>();

        public int MaxPages { get => maxPage; set => maxPage = value; }
        public List<cls_customer> CustomerList { get => customerList; set => customerList = value; }
        public List<string> Debug { get => debug; set => debug = value; }
        public int TotalInPage { get => totalInPage; set => totalInPage = value; }
        public List<cls_commodity> CommodityList { get => commodityList; set => commodityList = value; }
        public List<cls_login> LoginList { get => loginList; set => loginList = value; }
        public List<cls_rol> RoleList { get => roleList; set => roleList = value; }
    }
}
