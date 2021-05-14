using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.base_class
{

    public class search_filter 
    {
        string key;
        string value;
        long id;

        public string Key { get => key; set => key = value; }
        public string Value { get => value; set => this.value = value; }
        public long Id { get => id; set => id = value; }
    }

    public class filter_request:business_base_class
    {
        private int page;
        private int resultsPerPage;
        List<search_filter> filters = new List<search_filter>();
        private List<search_filter> getFilter() 
        {
            if (filters == null)
            {
                filters = new List<search_filter>();
            }
            return filters;
        }

        private bool filtersExists() { return Filters.Count > 0 ? true : false; }
        

        public int Page { get => page; set => page = value; }
        public int ResultsPerPage { get => resultsPerPage; set => resultsPerPage = value; }
        public List<search_filter> Filters { get => getFilter(); set => filters = value; }
        public bool FiltersExists { get => filtersExists(); }

    }
}
