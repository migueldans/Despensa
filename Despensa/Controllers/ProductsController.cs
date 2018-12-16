using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Despensa.Data;
using Despensa.Models;
using System.Net.Http;
using Despensa.Service;

namespace Despensa.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            List<Product> products = new List<Product>();
            SearchProduct rootObject = null;
            int i = 1;

            do
            {
                string URL = "https://world.openfoodfacts.org/cgi/search.pl?search_terms=coke&search_simple=1&json=1&page=" + i;
                HttpResponseMessage response = await client.GetAsync(URL);
                if (response.IsSuccessStatusCode)
                {
                    rootObject = await response.Content.ReadAsAsync<SearchProduct>();
                    foreach (Product product in rootObject.Products)
                    {
                        if (product.Product_Name.ToUpper().Contains("COKE"))
                        {
                            Product p = new Product
                            {
                                Product_Name = product.Product_Name,
                                Image_front_url = product.Image_front_url,
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

            return View(products);
            //return View(await _context.Product.ToListAsync());
        }




        //GET: Products/Details/5
        public async Task<IActionResult> Details(string name)
        {
            if (name == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Product_Name == name);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name_Product,Image_front_url,NovaGroup")] Product product)
        {
            PServices service = new PServices(_context);
            product.despensa = service.DespensaUser(User.Identity.Name);

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<ActionResult> InsertSearch(string productName)
        {
            PServices services = new PServices();
            List<Product> products;
            if (productName == null)
            {
                products = new List<Product>();
            }
            else
            {
                products = await services.InsertSearch(productName);

            }
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy([Bind("Name_Product,Image_front_url,NovaGroup")] Product product)
        {
            PServices service = new PServices(_context);
            product.despensa = service.DespensaUser(User.Identity.Name);

            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(InsertSearch));
            }
            return View(product);
        }
        
        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Product_Name,Image_front_url")] Product product)
        {
            if (id != product.Product_Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Product_Name))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Product_Name == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id)
        {
            return _context.Product.Any(e => e.Product_Name == id);
        }
    }
}
