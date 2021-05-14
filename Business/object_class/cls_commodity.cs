using Business.base_class;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.object_class
{
    public class cls_commodity:base_class.business_base_class
    {
        long idCompany;
        string internalCode;
        long idLine;
        long idHeading;
        long idUnitOfMeasurement;
        string line;
        string heading;
        string unitOfMeasurement;
        decimal averageWeight;
        bool procesed = false;
        decimal price = 0m;

        private cls_commodity(Data.udp_selectCommodity_Result r) 
        {
            Id = r.id;
            Name = r.name;
            internalCode = r.internal_code;
            line = r.LINE;
            heading = r.HEADING;
            unitOfMeasurement = r.UNIT_OF_MEASUREMENT;
            averageWeight = r.average_weight;
            price = r.PRICE!=null?r.PRICE.Value:0m;
        }

        private cls_commodity(long par_id_company, string par_name, string par_internalCode, string par_line, string par_heading, string par_unitofmeassurement, string par_averageweight) 
        {
            idCompany = par_id_company;
            Name = par_name;
            internalCode = par_internalCode;
            line = par_line;
            heading = par_heading;
            unitOfMeasurement = par_unitofmeassurement;
            decimal t_averageweight = 0;
            if (decimal.TryParse(par_averageweight, out t_averageweight)) 
            {
                averageWeight = t_averageweight;
            }
            save();
            
        }

        private void save() 
        {
            Data.MSKEntities MSKE = Data.singleton.cls_static_MksModel.GetEntity();
            MSKE.usp_InsertCommodity(Name, IdCompany, internalCode, Line, Heading, unitOfMeasurement, AverageWeight);
            
        }

        public long IdCompany { get => idCompany; set => idCompany = value; }
        public string InternalCode { get => internalCode; set => internalCode = value; }
        public long IdLine { get => idLine; set => idLine = value; }
        public long IdHeading { get => idHeading; set => idHeading = value; }
        public long IdUnitOfMeasurement { get => idUnitOfMeasurement; set => idUnitOfMeasurement = value; }
        public decimal AverageWeight { get => averageWeight; set => averageWeight = value; }
        public string Line { get => line; set => line = value; }
        public string Heading { get => heading; set => heading = value; }
        public string UnitOfMeasurement { get => unitOfMeasurement; set => unitOfMeasurement = value; }
        public bool Procesed { get => procesed; set => procesed = value; }
        public decimal Price { get => price; set => price = value; }

        public static base_class.filter_paged_response GetCommodities(long idCompany, filter_request filter) 
        {
            base_class.filter_paged_response r = new base_class.filter_paged_response();
            r.Debug.Add("se recibe un filtro con resultados por pagina " + filter.ResultsPerPage.ToString());
            r.Debug.Add("se recibe un filtro con pagina " + filter.Page.ToString());
            MSKEntities e = Data.singleton.cls_static_MksModel.GetEntity();
            List<udp_selectCommodity_Result> list = e.udp_selectCommodity(idCompany).ToList();
            r.Debug.Add("Resultados por paginas requeridos " + filter.ResultsPerPage.ToString());
            if (list != null && list.Count > 0)
            {
                
                
                
                List<cls_commodity> listcommodities = new List<cls_commodity>();
                int count = 0;
                for (int a=0; a<list.Count;a++)
                {
                    
                    if (list[a] != null)
                    {
                        if (filter.FiltersExists==true)
                        {
                            r.Debug.Add("Existen filtros");
                            search_filter sf = filter.Filters[filter.Filters.Count - 1]; 
                                bool added = false;
                                r.Debug.Add("Iterando filtro con id:" + sf.Id);
                                if (sf.Id == 0)
                                {
                                    
                                    if (sf.Key == "Name" && added == false && list[a].name.ToLower().Contains(sf.Value.ToLower()))
                                    {
                                        count++;
                                        listcommodities.Add(new cls_commodity(list[a]));
                                        added = true;
                                    }
                                    if (sf.Key == "InternalCode" && added == false && list[a].internal_code.ToLower().Contains(sf.Value.ToLower()))
                                    {
                                        count++;
                                        listcommodities.Add(new cls_commodity(list[a]));
                                        list[a].id = 0;
                                        added = true;
                                    }
                                }
                                else 
                                {
                                    r.Debug.Add("Id es -1");
                                    count++;
                                    listcommodities.Add(new cls_commodity(list[a]));
                                    added = true;
                                }
                            
                        }
                        else 
                        {
                            count++;
                            listcommodities.Add(new cls_commodity(list[a]));
                        }
                    }
                    else 
                    {
                        break;
                    }
                }


                int pointer = filter.ResultsPerPage * (filter.Page - 1);
                r.Debug.Add("Puntero: " + pointer.ToString());
                List<cls_commodity> PageFragment = new List<cls_commodity>();
                for (int a = pointer; a < filter.ResultsPerPage+pointer; a++) 
                {
                    try
                    {
                        if (listcommodities[a] != null)
                        {
                            PageFragment.Add(listcommodities[a]);
                        }
                     
                    }
                    catch (Exception exc) 
                    {
                        r.Debug.Add("Fuera de rango");
                        
                        break;
                    }
                }


                
                r.CommodityList = PageFragment;
                r.Debug.Add("Resultado lista: " + listcommodities.Count.ToString());
                decimal division = listcommodities.Count / filter.ResultsPerPage;
                r.Debug.Add("la division de total de paginas da " + division.ToString());
                division += listcommodities.Count % filter.ResultsPerPage == 0 ? 0 : 1;
                r.Debug.Add("finaliza con un total de paginas de " + division.ToString());
                r.MaxPages = Convert.ToInt32(division);
                r.TotalInPage = listcommodities.Count;
                list.Clear();
                return r;
            }
            else 
            {
                return r;
            }
        }

        public static int processPage(string page, int idCompany) 
        {
            string procesed_page = page.Replace("NULL", "");
            string[] ProcesedString = procesed_page.Split(new string[] { "[ENDLINE]" }, StringSplitOptions.None);
            //string filename = System.Web.Hosting.HostingEnvironment.MapPath("/") + "Client-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".neo";
            int count = 0;
            foreach (string LineString in ProcesedString)
            {
                count++;
                if (!(LineString.Trim() == string.Empty) && !(LineString == null))
                {
                   

                        string[] f = LineString.Split(new string[] { "|" }, StringSplitOptions.None);
                        cls_commodity commodity = new cls_commodity(idCompany, f[1], f[0], f[3], f[4], f[5], f[6]);
                        
                       // cls_customer customer = new cls_customer(long.Parse(f[0]), IdCompany, f[1], f[2], f[3], f[4], f[5], f[6], f[7], f[8], f[9], f[10], f[11], f[12], f[13]);
                       //INSERTAR NUEVO CLS_COMMODITY

                   
                }
            }
            return count;
        }


    }
}
