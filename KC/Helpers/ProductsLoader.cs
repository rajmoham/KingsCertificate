using System;
using System.Collections.Generic;
using System.Text.Json;
using KC.Models;
using Microsoft.AspNetCore.Hosting;

namespace KC.Helpers
{
    public class ProductsLoader
    {
        IWebHostEnvironment _webHostEnvironment;
        public CategoriesDirectory CategoriesDirectory { get; private set; }
        public List<Product> Products { get; private set; }

        public ProductsLoader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            LoadCategories();
            LoadProducts();
        }

        private void LoadCategories()
        {
            string categoriesJson = System.IO.File.ReadAllText(_webHostEnvironment.WebRootPath + "/json/categories.json");

            CategoriesDirectory categories = JsonSerializer.Deserialize<CategoriesDirectory>(categoriesJson);
            CategoriesDirectory = categories;
        }

        private void LoadProducts()
        {
            string productsJson = System.IO.File.ReadAllText(_webHostEnvironment.WebRootPath + "/json/productsDirectory2.json");

            List<Product> products = JsonSerializer.Deserialize<List<Product>>(productsJson);
            Products = products;

            /*Console.WriteLine("\"secondaryByPrimary\":{");

            foreach(var Category in CategoriesDirectory.primary.GetType().GetProperties())
            {
                string propName = Category.Name;
                Console.WriteLine("\""+propName + "\":[");
                List<string> affiliated2ndCatIds = new List<string>();

                foreach(Product product in products)
                {
                    if(product.PrimaryCategoryId == propName)
                    {
                        if (!affiliated2ndCatIds.Contains(product.SecondaryCategoryId))
                        {
                            affiliated2ndCatIds.Add(product.SecondaryCategoryId);
                        }
                    }
                }

                foreach (string catId in affiliated2ndCatIds)
                    Console.WriteLine("\"" + catId + "\",");
                Console.WriteLine("],");
            }

            Console.WriteLine("},\"tertiaryBySecondary\":{");

            foreach (var Category in CategoriesDirectory.secondary.GetType().GetProperties())
            {
                string propName = Category.Name;
                Console.WriteLine("\"" + propName + "\":[");
                List<string> affiliated3rdCatIds = new List<string>();

                foreach (Product product in products)
                {
                    if (product.SecondaryCategoryId == propName)
                    {
                        if (!affiliated3rdCatIds.Contains(product.TertiaryCategoryId))
                        {
                            affiliated3rdCatIds.Add(product.TertiaryCategoryId);
                        }
                    }
                }

                foreach (string catId in affiliated3rdCatIds)
                    Console.WriteLine("\""+catId + "\",");
                Console.WriteLine("],");
            }
            Console.WriteLine("}");*/
        }
    }
}
