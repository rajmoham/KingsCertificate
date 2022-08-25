using System;
using System.Collections.Generic;

namespace KC.Models.ViewModels.Basket
{
    public class IndexVM
    {
        public List<ShoppingCartItem> CartList { get; set; }
        public List<Product> ProductsOffers { get; set; }
    }
}
