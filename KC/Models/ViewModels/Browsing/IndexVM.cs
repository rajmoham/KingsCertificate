using System;
using System.Collections.Generic;

namespace KC.Models.ViewModels.Browsing
{
    public class IndexVM
    {
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
        public string Sort { get; set; }
        public List<Product> Products { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public string SearchTerm { get; set; }
        public List<string> SearchSuggestions { get; set; }
    }
}
