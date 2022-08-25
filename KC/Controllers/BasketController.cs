using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KC.Helpers;
using KC.Models;
using KC.Models.ViewModels.Basket;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KC.Controllers
{
    public class BasketController : Controller
    {
        private readonly ProductsLoader _productsLoader;

        public BasketController(ProductsLoader productsLoader)
        {
            _productsLoader = productsLoader;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<ShoppingCartItem> cartList = new List<ShoppingCartItem>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCartItem>>(Constants.SessionKeys.Cart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCartItem>>(Constants.SessionKeys.Cart).Count() > 0)
            {
                cartList = HttpContext.Session.Get<List<ShoppingCartItem>>(Constants.SessionKeys.Cart);
            }

            IndexVM viewModel = new IndexVM()
            {
                CartList = cartList,
                ProductsOffers = new List<Product>()
            };
            /*int max = _productsLoader.Products.Count() - 1;

            Random random = new Random();

            

            for (int i = 1; i <= cartList.Count + 2; i++)
            {
                int randomNo = random.Next(0, max);
                Product product = _productsLoader.Products[randomNo];
                //product.QuantityInCart = HttpContext.Session.GetQuantityInCart(product.productId);
                viewModel.ProductsOffers.Add(product);
            }*/

            foreach (ShoppingCartItem item in cartList)
            {
                // generate ONE recommended item
                List<Product> productsInCategory = _productsLoader.Products.Where(p => p.SecondaryCategoryId == item.Product.SecondaryCategoryId).ToList();

                int max = productsInCategory.Count - 1;

                Random random = new Random();

                bool foundOne = false;
                int randomNo = 0;

                while (!foundOne)
                {
                    randomNo = random.Next(0, max);
                    if (!viewModel.ProductsOffers.Contains(productsInCategory[randomNo]) &&
                        !viewModel.CartList.Select(item => item.Product.productId).Contains(productsInCategory[randomNo].productId))
                    {
                        foundOne = true;
                    }
                }
                viewModel.ProductsOffers.Add(productsInCategory[randomNo]);
            }

            if(cartList.Count == 0)
            {
                int max = _productsLoader.Products.Count() - 1;

                Random random = new Random();

                for (int i = 1; i <= 2; i++)
                {
                    int randomNo = random.Next(0, max);
                    Product product = _productsLoader.Products[randomNo];
                    //product.QuantityInCart = HttpContext.Session.GetQuantityInCart(product.productId);
                    viewModel.ProductsOffers.Add(product);
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            List<ShoppingCartItem> cartList = new List<ShoppingCartItem>();
            HttpContext.Session.Set(Constants.SessionKeys.Cart, cartList);

            TempData[Constants.NotificationKeys.SuccessKey] = "Cart cleared successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
