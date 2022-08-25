using System;
using System.Collections.Generic;

namespace KC.Models.ViewModels.Browsing
{
    public class DetailsVM
    {
        public Product Product { get; set; }
        public bool IsInCart { get; set; }
        public int QuantityInCart { get; set; }
        public List<Product> ProductsOffers { get; set; }
    }
}
