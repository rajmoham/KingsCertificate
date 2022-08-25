using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KC.Models.ViewModels;
using System.Text.Json;
using KC.Models;
using Microsoft.AspNetCore.Hosting;
using KC.Helpers;
using KC.Models.ViewModels.Home;

namespace KC.Controllers
{
    public class HomeController : Controller
    {
        IWebHostEnvironment _webHostEnvironment;
        ProductsLoader _productsLoader;

        public HomeController(IWebHostEnvironment webHostEnvironment, ProductsLoader productsLoader)
        {
            _webHostEnvironment = webHostEnvironment;
            _productsLoader = productsLoader;
        }

        // GET - Index
        public IActionResult Index()
        {
            int max = _productsLoader.Products.Count() - 1;

            Random random = new Random();

            IndexVM viewModel = new IndexVM()
            {
                Products = new List<Product>()
            };

            for(int i = 1; i <= 12; i++)
            {
                int randomNo = random.Next(0, max);
                Product product = _productsLoader.Products[randomNo];
                //product.QuantityInCart = HttpContext.Session.GetQuantityInCart(product.productId);
                viewModel.Products.Add(product);
            }

            return View(viewModel);
        }

        public IActionResult TermsAndConditions()
        {
            return View();
        }

        public IActionResult FAQs()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Survey()
        {
            return View(new SurveyVM() { Problems = Constants.WebsiteProblems });
        }

        public IActionResult ContactUs()
        {
            ContactUsVM viewModel = new ContactUsVM()
            {
                EmailAddress = string.Empty,
                Message = string.Empty,
                Name = string.Empty,
                OrderNumber = string.Empty,
                ShowConfirmation = false
            };
            return View(viewModel);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(ContactUsVM viewModel)
        {
            viewModel.ShowConfirmation = true;
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
