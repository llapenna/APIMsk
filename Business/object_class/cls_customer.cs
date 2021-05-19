using Business.base_class;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.object_class
{
    public class cls_customer : business_base_class
    {

        private long id_system;
        private long id_company;
        private string address;
        private string zipcode;
        private string city;
        private string iva;
        private string cuit;
        private string phone;
        private string seller;
        private long internal_code_seller;
        private string zone;
        private string route;
        private string custommerType;
        private string activity;
        private string branch;
        private decimal balance;

        public long Id_system { get => id_system; set => id_system = value; }
        public long Id_company { get => id_company; set => id_company = value; }
        public string Address { get => address; set => address = value; }
        public string Zipcode { get => zipcode; set => zipcode = value; }
        public string City { get => city; set => city = value; }
        public string Iva { get => iva; set => iva = value; }
        public string Cuit { get => cuit; set => cuit = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Seller { get => seller; set => seller = value; }
        public string Zone { get => zone; set => zone = value; }
        public string Route { get => route; set => route = value; }
        public string CustommerType { get => custommerType; set => custommerType = value; }
        public string Activity { get => activity; set => activity = value; }
        public string Branch { get => branch; set => branch = value; }
        public decimal Balance { get => balance; set => balance = value; }
        public long Internal_code_seller { get => internal_code_seller; set => internal_code_seller = value; }

        public cls_customer(cls_customer c)
        {

        }
        private cls_customer(Data.usp_selectCustomer_Result r)
        {

            Id = r.id;
            Name = r.name;
            id_system = r.id_system;
            id_company = r.id_company;
            address = r.address;
            zipcode = r.ZIPCODE;
            city = r.CITY;
            iva = r.IVA;
            cuit = r.CUIT;
            phone = r.phone;
            seller = r.SELLER;
            zone = r.ZONE;
            route = r.ROUTE;
            custommerType = r.TYPE;
            activity = r.ACTIVITY;
            branch = r.BRANCH;
            balance = r.balance != null ? r.balance.Value : 0;



        }
        public cls_customer(usp_selectCustomerByUser_Result r)
        {
            Id = r.ID;
            id_system = r.ID_SYSTEM;
            id_company = r.ID_COMPANY;

            Name = r.NAME;
            address = r.ADDRESS;
            cuit = r.CUIT;
            phone = r.PHONE;
            zipcode = r.ZIPCODE;
            city = r.CITY;
            zone = r.ZONE;
            route = r.ROUTE;
            branch = r.BRANCH;

            custommerType = r.TYPE;
            activity = r.ACTIVITY;

            balance = r.BALANCE ?? 0;
            iva = r.IVA;

            seller = r.SELLER;

        }

        private cls_customer(Data.usp_GetCustomerBySystemID_Result r)
        {

            Id = r.id;
            Name = r.name;
            id_company = r.id_company;
            balance = r.balance != null ? r.balance.Value : 0;
        }



        private cls_customer(
            long par_id_system,
            long par_id_company,
            string par_name,
            string par_address,
            string par_zipcode,
            string par_city,
            string par_iva,
            string par_cuit,
            string par_phone,
            string par_seller,
            string par_zone,
            string par_route,
            string par_custommerType,
            string par_activity,
            string par_branch,
            string par_balance,
            string par_cuitCompany,
            long par_seller_internal_code
            )
        {
            id_system = par_id_system;
            id_company = par_id_company;
            Name = par_name;
            address = par_address;
            zipcode = par_zipcode;
            city = par_city;
            iva = par_iva;
            cuit = par_cuit;
            phone = par_phone;
            seller = par_seller;
            zone = par_zone;
            route = par_route;
            custommerType = par_custommerType;
            activity = par_activity;
            branch = par_branch;
            decimal t_balance = 0;
            balance = decimal.TryParse(par_balance, out t_balance) == true ? decimal.Parse(par_balance) : 0;
            internal_code_seller = par_seller_internal_code;
            save();
        }

        public void save()
        {
            if (Name.ToLower().Contains("grupo"))
            {

            }
            MSKEntities e = new Data.MSKEntities();
            e.usp_InsertCustomer(id_system, id_company,
                                (Name == string.Empty || Name == null ? "" : Name),
                                (address == string.Empty || address == null ? "" : address),
                                (zipcode == string.Empty || zipcode == null ? "" : zipcode),
                                (city == string.Empty || city == null ? "" : city),
                                (iva == string.Empty || iva == null ? "" : iva),
                                (cuit == string.Empty || cuit == null ? "" : cuit),
                                (phone == string.Empty || phone == null ? "" : phone),
                                (seller == string.Empty || seller == null ? "" : seller),
                                (zone == string.Empty || zone == null ? "" : zone),
                                (route == string.Empty || route == null ? "" : route),
                                (custommerType == string.Empty || custommerType == null ? "" : custommerType),
                                (activity == string.Empty || activity == null ? "" : activity),
                                (branch == string.Empty || branch == null ? "" : branch),
                                balance,
                                internal_code_seller);
        }

        public static filter_paged_response GetCustomers(long id_company, filter_request filter)
        {
            filter_paged_response resp = new filter_paged_response();
            resp.Debug.Add("se recibe un filtro con resultados por pagina " + filter.ResultsPerPage.ToString());
            resp.Debug.Add("se recibe un filtro con pagina " + filter.Page.ToString());
            MSKEntities e = Data.singleton.cls_static_MksModel.GetEntity();
            List<usp_selectCustomer_Result> lcust = e.usp_selectCustomer(id_company).ToList();
            if (lcust != null && lcust.Count > 0)
            {
                
                
                List<cls_customer> customerlist = new List<cls_customer>();
                int count = 0;
                for (int a = 0; a < lcust.Count; a++)
                {
                    
                    if (lcust[a] != null )
                    {
                        if (filter.FiltersExists == true)
                        {
                            resp.Debug.Add("Existen filtros");
                            search_filter sf = filter.Filters[filter.Filters.Count - 1];

                            bool added = false;
                            if (sf.Id != -1)
                            {
                                if (sf.Key == "Name" && added == false && lcust[a].name.ToLower().Contains(sf.Value.ToLower()))
                                {
                                    count++;
                                    customerlist.Add(new cls_customer(lcust[a]));
                                    added = true;
                                }
                                if (sf.Key.ToLower() == "cuit" && added == false && lcust[a].CUIT.ToLower().Replace("-", "").Contains(sf.Value.ToLower().Replace("-", "")))
                                {
                                    count++;
                                    customerlist.Add(new cls_customer(lcust[a]));
                                    added = true;
                                }
                                try
                                {
                                    if (sf.Key.ToLower() == "id_system" && added == false && lcust[a].id_system == long.Parse(sf.Value))
                                    {
                                        count++;
                                        customerlist.Add(new cls_customer(lcust[a]));
                                        added = true;
                                    }
                                }
                                catch (Exception exc)
                                {
                                    resp.Debug.Add("No se pudo parsear ese codigo interno...");
                                }
                            }
                            else
                            {
                                count++;
                                customerlist.Add(new cls_customer(lcust[a]));
                                added = true;
                            }





                        }
                        else
                        {

                            resp.Debug.Add("NO Existen filtros");
                            count++;
                            customerlist.Add(new cls_customer(lcust[a]));
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                int pointer = filter.ResultsPerPage * (filter.Page - 1);
                List<cls_customer> PageFragment = new List<cls_customer>();
                for (int a = pointer; a < filter.ResultsPerPage + pointer; a++) 
                {
                    try
                    {
                        if (customerlist[a] != null) 
                        {
                            PageFragment.Add(customerlist[a]);
                        }
                    }
                    catch (Exception exc) 
                    {
                        break;
                    }
                }

                resp.CustomerList = PageFragment;
                decimal division = customerlist.Count / filter.ResultsPerPage;
                resp.Debug.Add("la division de total de paginas da " + division.ToString());
                division += lcust.Count % filter.ResultsPerPage == 0 ? 0 : 1;
                resp.Debug.Add("finaliza con un total de paginas de " + division.ToString());
                resp.MaxPages = Convert.ToInt32(division);
                resp.TotalInPage = customerlist.Count;
                lcust.Clear();
                return resp;
            }

            else
            {
                return new filter_paged_response();
            }

        }

        public static filter_paged_response GetCustomersByUser(long id_login, filter_request filter)
        {
            filter_paged_response response = new filter_paged_response();

            MSKEntities msk = Data.singleton.cls_static_MksModel.GetEntity();
            List<usp_selectCustomerByUser_Result> r = msk.usp_selectCustomerByUser(id_login).ToList();

            // Verificamos que haya por lo menos un filtro y que no sea uno nulo
            bool mustFilter = filter.FiltersExists && filter.Filters.Last().Id != -1;

            if (r != null && r.Count > 0)
            {
                List<cls_customer> customers = new List<cls_customer>();

                foreach (usp_selectCustomerByUser_Result c in r)
                {
                    // Verificamos que la informacion no sea nula
                    if (c != null)
                    {
                        if (mustFilter)
                        {
                            // Nos quedamos con el ultimo filtro, por requerimiento
                            search_filter sf = filter.Filters.Last();

                            // Si el valor coincide con el filtro
                            if (sf.Match(c))
                                customers.Add(new cls_customer(c));

                        }
                        // No hay filtros, devolvemos todo
                        else
                            customers.Add(new cls_customer(c));

                    }
                    else break;
                }

                // Nos quedamos con una seccion de los resultados
                response.CustomerList = customers.Count > filter.ResultsPerPage
                    ? customers.GetRange(filter.Page * filter.ResultsPerPage, filter.ResultsPerPage)
                    : customers;

                response.MaxPages = Convert.ToInt32(customers.Count / filter.ResultsPerPage + r.Count % filter.ResultsPerPage == 0 ? 0 : 1);
                response.TotalInPage = customers.Count;
                r.Clear();
            }

            return response;
        }

        public static filter_paged_response GetCustomersByCompany(long id_company, filter_request filter)
        {
            filter_paged_response resp = new filter_paged_response();
            resp.Debug.Add("se recibe un filtro con resultados por pagina " + filter.ResultsPerPage.ToString());
            resp.Debug.Add("se recibe un filtro con pagina " + filter.Page.ToString());
            MSKEntities e = Data.singleton.cls_static_MksModel.GetEntity();
            List<usp_selectCustomer_Result> lcust = e.usp_selectCustomer(id_company).ToList();
            if (lcust != null && lcust.Count > 0)
            {


                List<cls_customer> customerlist = new List<cls_customer>();
                int count = 0;
                for (int a = 0; a < lcust.Count; a++)
                {

                    if (lcust[a] != null)
                    {
                        if (filter.FiltersExists == true)
                        {
                            resp.Debug.Add("Existen filtros");

                            // Solamente utiliza el ultimo filtro que se envia, por requerimiento de MSK
                            search_filter sf = filter.Filters[filter.Filters.Count - 1];

                            bool added = false;
                            if (sf.Id != -1)
                            {
                                if (sf.Key == "Name" && added == false && lcust[a].name.ToLower().Contains(sf.Value.ToLower()))
                                {
                                    count++;
                                    customerlist.Add(new cls_customer(lcust[a]));
                                    added = true;
                                }
                                if (sf.Key.ToLower() == "cuit" && added == false && lcust[a].CUIT.ToLower().Replace("-", "").Contains(sf.Value.ToLower().Replace("-", "")))
                                {
                                    count++;
                                    customerlist.Add(new cls_customer(lcust[a]));
                                    added = true;
                                }
                                try
                                {
                                    if (sf.Key.ToLower() == "id_system" && added == false && lcust[a].id_system == long.Parse(sf.Value))
                                    {
                                        count++;
                                        customerlist.Add(new cls_customer(lcust[a]));
                                        added = true;
                                    }
                                }
                                catch (Exception exc)
                                {
                                    resp.Debug.Add("No se pudo parsear ese codigo interno...");
                                }
                            }
                            else
                            {
                                count++;
                                customerlist.Add(new cls_customer(lcust[a]));
                                added = true;
                            }
                        }
                        else
                        {

                            resp.Debug.Add("NO Existen filtros");
                            count++;
                            customerlist.Add(new cls_customer(lcust[a]));
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                int pointer = filter.ResultsPerPage * (filter.Page - 1);
                List<cls_customer> PageFragment = new List<cls_customer>();
                for (int a = pointer; a < filter.ResultsPerPage + pointer; a++)
                {
                    try
                    {
                        if (customerlist[a] != null)
                        {
                            PageFragment.Add(customerlist[a]);
                        }
                    }
                    catch (Exception exc)
                    {
                        break;
                    }
                }

                resp.CustomerList = PageFragment;
                decimal division = customerlist.Count / filter.ResultsPerPage;
                resp.Debug.Add("la division de total de paginas da " + division.ToString());
                division += lcust.Count % filter.ResultsPerPage == 0 ? 0 : 1;
                resp.Debug.Add("finaliza con un total de paginas de " + division.ToString());
                resp.MaxPages = Convert.ToInt32(division);
                resp.TotalInPage = customerlist.Count;
                lcust.Clear();
                return resp;
            }

            else
            {
                return new filter_paged_response();
            }

        }
        public static cls_customer GetCustomerByInternalID(long par_SystemID)
        {

            MSKEntities e = Data.singleton.cls_static_MksModel.GetEntity();
            List<usp_GetCustomerBySystemID_Result> r = e.usp_GetCustomerBySystemID(par_SystemID).ToList();
            if (r != null && r.Count > 0)
            {
                return new cls_customer(r[0]);
            }
            else
            {
                return null;
            }
        }

        public static int ProcessPage(string Page, int IdCompany)
        {
            string[] ProcesedString = Page.Split(new string[] { "[ENDLINE]" }, StringSplitOptions.None);
            //string filename = System.Web.Hosting.HostingEnvironment.MapPath("/") + "Client-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".neo";
            int count = 0;
            foreach (string LineString in ProcesedString)
            {
                count++;
                if (!(LineString.Trim() == string.Empty) && !(LineString == null))
                {
                    if (!LineString.Contains("Nº"))
                    {

                        string[] f = LineString.Split(new string[] { "|" }, StringSplitOptions.None);
                        long sellerinternalcode = 0;
                        if (f[16].Trim() != string.Empty && f[16].Trim() != null) 
                        {
                            long.TryParse(f[16], out sellerinternalcode);
                        }
                        cls_customer customer = new cls_customer(long.Parse(f[0]), IdCompany, f[1], f[2], f[3], f[4], f[5], f[6], f[7], f[8], f[9], f[10], f[11], f[12], f[13], f[14], f[15], long.Parse(f[16]));

                    }
                }
            }
            return count;
        }
    }
}

