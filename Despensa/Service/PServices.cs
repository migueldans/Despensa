using Despensa.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Despensa.Service
{
    public class PServices
    {

        private readonly Data.ApplicationDbContext _context;
        
        public PServices(Data.ApplicationDbContext context)
        {
            _context = context;
        }
        public PServices()
        {
        
        }

        public async Task<List<Product>> InsertSearch(string productName)
        {
            HttpClient client = new HttpClient();
            List<Product> products = new List<Product>();
            SearchProduct rootObject = null;



            int i = 1;
            do
            {
                string URL = "https://world.openfoodfacts.org/cgi/search.pl?search_terms=" + productName + "&search_simple=1&json=1&page=" + i;
                HttpResponseMessage response = await client.GetAsync(URL);
                if (response.IsSuccessStatusCode)
                {
                    rootObject = await response.Content.ReadAsAsync<SearchProduct>();
                    foreach (Product product in rootObject.Products)
                    {
                        if (product.Product_Name.ToUpper().Contains(productName.ToUpper()))
                        {
                            Product p = new Product
                            {
                                Name_Product = product.Product_Name,
                                Image_front_url = product.Image_front_url,
                                NovaGroup = product.NovaGroup
                            };
                            products.Add(p);
                        }
                    }
                    if (rootObject.Page_size + rootObject.Skip >= rootObject.Count)
                    {
                        break;
                    }
                    i++;
                }
            } while (rootObject.Page_size + rootObject.Skip < rootObject.Count);

            return products;
        }

    public despensa DespensaUser(string email)
    {
            despensa d1 = new despensa();
        foreach(despensa desp in _context.despensa.ToList())
            {
                if (desp.Name == email)
                {
                    d1 = desp;
                }
            }

            return d1;
    }
    }
}
