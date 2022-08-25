using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using KC.Helpers;
using KC.Models;
using KC.Models.ViewModels.Browsing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KC.Controllers
{
    public class BrowsingController : Controller
    {
        ProductsLoader _productsLoader;
        SpellChecker _spellChecker;

        public BrowsingController(ProductsLoader productsLoader, SpellChecker spellChecker)
        {
            _productsLoader = productsLoader;
            _spellChecker = spellChecker;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(string cat = "", string searchTerm = "", int page = 1, string sort = "default")
        {
            if (!string.IsNullOrWhiteSpace(cat))
            {
                IEnumerable<Product> productsInitial;
                string catName = null ;
                if (_productsLoader.CategoriesDirectory.primary.GetType().GetProperties().Where(c => c.Name == cat).Count() > 0)
                {
                    productsInitial = _productsLoader.Products
                .Where(p => p.PrimaryCategoryId == cat);
                    catName = (string)_productsLoader.CategoriesDirectory.primary.GetType().GetProperty(cat).GetValue(_productsLoader.CategoriesDirectory.primary);
                }
                else if (_productsLoader.CategoriesDirectory.secondary.GetType().GetProperties().Where(c => c.Name == cat).Count() > 0)
                {
                    productsInitial = _productsLoader.Products
                .Where(p => p.SecondaryCategoryId == cat);
                    catName = (string)_productsLoader.CategoriesDirectory.secondary.GetType().GetProperty(cat).GetValue(_productsLoader.CategoriesDirectory.secondary);
                }
                else if (_productsLoader.CategoriesDirectory.tertiary.GetType().GetProperties().Where(c => c.Name == cat).Count() > 0)
                {
                    productsInitial = _productsLoader.Products
                .Where(p => p.TertiaryCategoryId == cat);
                    catName = (string)_productsLoader.CategoriesDirectory.tertiary.GetType().GetProperty(cat).GetValue(_productsLoader.CategoriesDirectory.tertiary);
                }
                else
                {
                    productsInitial = _productsLoader.Products;
                }
                List<Product> products;

                switch (sort)
                {
                    case Constants.SearchOptions.PriceAscending.key:
                        products = productsInitial.OrderBy(p => p.FurtherDetails.ListPrice).ToList();
                        break;
                    case Constants.SearchOptions.PriceDescending.key:
                        products = productsInitial.OrderBy(p => p.FurtherDetails.ListPrice).Reverse().ToList();
                        break;
                    case Constants.SearchOptions.BrandName.key:
                        products = productsInitial.OrderBy(p => p.BrandName).ToList();
                        break;
                    case Constants.SearchOptions.DisplayName.key:
                        products = productsInitial.OrderBy(p => p.FurtherDetails.DisplayName).ToList();
                        break;
                    default:
                        products = productsInitial.ToList();
                        break;
                }

                double divisionOfPages = (double)products.Count / (double)Constants.DisplayOptions.ItemsPerPage;
                int numberOfPages = (int)Math.Ceiling(divisionOfPages);

                foreach (Product product in products)
                {
                    product.QuantityInCart = HttpContext.Session.GetQuantityInCart(product.productId);
                    product.returnUrl = Url.Action(nameof(Index), "Browsing", new { cat = "", searchTerm = "", page = 1, sort = "default" }) + $"#{product.productId}";
                }

                IndexVM viewModel = new IndexVM()
                {
                    Products = products.GetRange((page - 1) * Constants.DisplayOptions.ItemsPerPage, Constants.DisplayOptions.ItemsPerPage > products.Count ? products.Count : Constants.DisplayOptions.ItemsPerPage ),
                    CategoryName = catName,
                    PageNumber = page,
                    TotalPages = numberOfPages,
                    CategoryId = cat,
                    Sort = sort,
                    SearchTerm = searchTerm,
                    SearchSuggestions = new List<string>()
                };
                return View(viewModel);
            }
            else if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                SpellCheckResult spellCheckResult = await _spellChecker.CheckSpelling(searchTerm);

                IEnumerable<Product> productsInitial = _productsLoader.Products
                .Where(p => p.DisplayName.Replace("  ", " ").ToLower().Contains(searchTerm.Replace("  ", " ").ToLower())).ToList();
                List<Product> products;

                switch (sort)
                {
                    case Constants.SearchOptions.PriceAscending.key:
                        products = productsInitial.OrderBy(p => p.FurtherDetails.ListPrice).ToList();
                        break;
                    case Constants.SearchOptions.PriceDescending.key:
                        products = productsInitial.OrderBy(p => p.FurtherDetails.ListPrice).Reverse().ToList();
                        break;
                    case Constants.SearchOptions.BrandName.key:
                        products = productsInitial.OrderBy(p => p.BrandName).ToList();
                        break;
                    case Constants.SearchOptions.DisplayName.key:
                        products = productsInitial.OrderBy(p => p.FurtherDetails.DisplayName).ToList();
                        break;
                    default:
                        products = productsInitial.ToList();
                        break;
                }

                double divisionOfPages = (double)products.Count / (double)Constants.DisplayOptions.ItemsPerPage;
                int numberOfPages = (int)Math.Ceiling(divisionOfPages);

                IndexVM viewModel = new IndexVM()
                {
                    Products = products.GetRange(
                        (page - 1) * Constants.DisplayOptions.ItemsPerPage,
                        (Constants.DisplayOptions.ItemsPerPage > products.Count - (page - 1) * Constants.DisplayOptions.ItemsPerPage ?
                        products.Count - (page - 1) * Constants.DisplayOptions.ItemsPerPage :
                        Constants.DisplayOptions.ItemsPerPage)),

                    //CategoryName = (string)_productsLoader.CategoriesDirectory.primary.GetType().GetProperty(cat).GetValue(_productsLoader.CategoriesDirectory.primary),
                    PageNumber = page,
                    TotalPages = numberOfPages,
                    //CategoryId = cat,
                    Sort = sort,
                    SearchTerm = searchTerm,
                    SearchSuggestions = (spellCheckResult.IsCorrectable ? spellCheckResult.CorrectedVersions : new List<string>())
                };
                return View(viewModel);
            }
            else
            {
                return View(nameof(Index));
            }
        }

        public IActionResult Offers()
        {
            int max = _productsLoader.Products.Count() - 1;

            Random random = new Random();

            OffersVM viewModel = new OffersVM()
            {
                Products = new List<Product>()
            };

            for (int i = 1; i <= Constants.DisplayOptions.ItemsPerPage; i++)
            {
                int randomNo = random.Next(0, max);
                Product product = _productsLoader.Products[randomNo];
                viewModel.Products.Add(product);
            }

            return View(viewModel);
        }

        public JsonResult GetProducts(string term = "NO_PRODUCT_HAS_THIS_NAME")
        {
            var products = _productsLoader.Products
                .Where(p => p.FurtherDetails.FullDisplayName.ToLower().Contains(term))
                .Select(p => new { name = p.FurtherDetails.DisplayName, imageurl = p.FurtherDetails.ImageUrl, id = p.productId })
                .ToList();

            return Json(products);
        }

        [HttpPost]
        public IActionResult Search(string searchTerm = null)
        {
            if (!string.IsNullOrWhiteSpace(searchTerm))
                return RedirectToAction(nameof(Index), new { searchTerm = searchTerm });
            return RedirectToAction(nameof(Index), "Home");
        }

        public IActionResult Details(string id)
        {
            Product product = _productsLoader.Products.Where(p => p.productId == id).ToList().FirstOrDefault();

            if (product == null)
                return RedirectToAction(nameof(Index), "Home");

            DetailsVM viewModel = new DetailsVM()
            {
                Product = _productsLoader.Products.Where(p => p.productId == id).ToList().FirstOrDefault(),
                QuantityInCart = 1,
                IsInCart = false,
                ProductsOffers = new List<Product>()
            };

            Dictionary<string, string> ProdInfoPairs = new Dictionary<string, string>()
            {
                { "Brand", viewModel.Product.BrandName },
                { "Size/Weight", viewModel.Product.FurtherDetails.SizeVolume },
                { "SKU", viewModel.Product.Sku },
            };

            viewModel.Product.ProdInfoPairs = new Dictionary<string, string>();

            foreach (var pair in ProdInfoPairs)
            {
                if (pair.Value != null) viewModel.Product.ProdInfoPairs.Add(pair.Key, pair.Value);
            }

            List<ShoppingCartItem> cartList = new List<ShoppingCartItem>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCartItem>>(Constants.SessionKeys.Cart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCartItem>>(Constants.SessionKeys.Cart).Count() > 0)
            {
                cartList = HttpContext.Session.Get<List<ShoppingCartItem>>(Constants.SessionKeys.Cart);
            }

            foreach (ShoppingCartItem item in cartList)
            {
                if (item.Quantity != 0)
                {
                    if (item.Product.productId == id)
                    {
                        viewModel.QuantityInCart = item.Quantity;
                        viewModel.IsInCart = true;
                    }
                }
            }

            /*
            int max = _productsLoader.Products.Count() - 1;

            Random random = new Random();

            for (int i = 1; i <= 4; i++)
            {
                int randomNo = random.Next(0, max);
                Product productOffer = _productsLoader.Products[randomNo];
                //product.QuantityInCart = HttpContext.Session.GetQuantityInCart(product.productId);
                viewModel.ProductsOffers.Add(productOffer);
            }
             */

            List<Product> productsInCategory = _productsLoader.Products.Where(p => p.SecondaryCategoryId == viewModel.Product.SecondaryCategoryId).ToList();

            int max = productsInCategory.Count - 1;

            Random random = new Random();

            for(int i = 1; i<= 4; i++)
            {
                bool foundOne = false;
                int randomNo = 0;
                while (!foundOne)
                {
                    randomNo = random.Next(0, max);
                    if(!viewModel.ProductsOffers.Contains(productsInCategory[randomNo]) && viewModel.Product.productId != productsInCategory[randomNo].productId)
                    {
                        foundOne = true;
                    }
                }
                viewModel.ProductsOffers.Add(productsInCategory[randomNo]);
            }

            return View(nameof(Details), viewModel);
        }

        [HttpPost]
        public IActionResult Details(DetailsVM viewModel)
        {
            UpdateQuantityOfItem(viewModel.Product.productId, viewModel.QuantityInCart);
            TempData[Constants.NotificationKeys.SuccessKey] = "Quantity updated";

            return RedirectToAction(nameof(Details), new { id = viewModel.Product.productId });
        }

        [HttpPost]
        public void AddToBasket(string productId, int quantity)
        {
            UpdateQuantityOfItem(productId, quantity);
        }

        [HttpPost]
        public void IncrementQuantityPlus(string productId)
        {
            AddQuantityToItem(productId, 1);
        }

        [HttpPost]
        public void IncrementQuantityMinus(string productId)
        {
            AddQuantityToItem(productId, -1);
        }

        public void AddQuantityToItem(string productId, int incrementQuantity)
        {
            List<ShoppingCartItem> cartList = new List<ShoppingCartItem>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCartItem>>(Constants.SessionKeys.Cart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCartItem>>(Constants.SessionKeys.Cart).Count() > 0)
            {
                cartList = HttpContext.Session.Get<List<ShoppingCartItem>>(Constants.SessionKeys.Cart);
            }

            int indexToRemove = -1;

            foreach (ShoppingCartItem item in cartList)
            {
                if (item.Product.productId == productId)
                {
                    if (item.Product.QuantityInCart + incrementQuantity <= 0)
                    {
                        indexToRemove = cartList.IndexOf(item);
                    }
                    else
                    {
                        item.Quantity += incrementQuantity;
                    }
                }
            }

            if(indexToRemove != -1)
            {
                cartList.RemoveAt(indexToRemove);
            }

            HttpContext.Session.Set(Constants.SessionKeys.Cart, cartList);
        }

        public void UpdateQuantityOfItem(string productId, int quantity)
        {
            List<ShoppingCartItem> cartList = new List<ShoppingCartItem>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCartItem>>(Constants.SessionKeys.Cart) != null
                && HttpContext.Session.Get<IEnumerable<ShoppingCartItem>>(Constants.SessionKeys.Cart).Count() > 0)
            {
                cartList = HttpContext.Session.Get<List<ShoppingCartItem>>(Constants.SessionKeys.Cart);
            }

            bool found = false;

            if (quantity != 0)
            {
                foreach (ShoppingCartItem item in cartList)
                {
                    if (item.Product.productId == productId)
                    {
                        found = true;
                        item.Quantity = quantity;
                    }
                }

                if (!found)
                {
                    cartList.Add(new ShoppingCartItem() { Product = _productsLoader.Products.Where(p => p.productId == productId).FirstOrDefault(), Quantity = quantity });
                }
            }
            else
            {
                ShoppingCartItem itemToRemove = cartList.Where(i => i.Product.productId == productId).FirstOrDefault();
                cartList.Remove(itemToRemove);
            }

            HttpContext.Session.Set(Constants.SessionKeys.Cart, cartList);
        }
    }
}
