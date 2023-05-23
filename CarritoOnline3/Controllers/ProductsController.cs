using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarritoOnline3.Models;
using CarritoOnline3.Models.DAL;
using CarritoOnline3.Models.Entities;
using Microsoft.AspNetCore.Http;
using CarritoOnline3.Extensions;
using Newtonsoft.Json;
using CarritoOnline3.Models;

namespace CarritoOnline3.Controllers
{
    public class ProductsController : Controller
    {
        private readonly EtnaContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public ProductsController(EtnaContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        #region CRUD
        // GET: Products
        public async Task<IActionResult> Index()
        {
              return _context.Products != null ? 
                          View(await _context.Products.ToListAsync()) :
                          Problem("Entity set 'EtnaContext.Products'  is null.");
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,Name,Description,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,Name,Description,Price")] Product product)
        {
            if (id != product.ProductID)
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
                    if (!ProductExists(product.ProductID))
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'EtnaContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Aditional

        public async Task<IActionResult> NewOrder(int? productId)
        {
            //A product has just been added
            if (productId != null)
            {
                List<int> cartIds;
                if (HttpContext.Session.GetObject<List<int>>("Cart") == null)
                {
                    cartIds = new List<int>();
                }
                else
                {
                    cartIds = HttpContext.Session.GetObject<List<int>>("Cart");
                }

                if (cartIds.Contains(productId.Value) == false)
                {
                    cartIds.Add(productId.Value);
                    // Marcar la cookie como segura
                    HttpContext.Session.SetObject("Cart", cartIds);
                    
                }
            }
            return _context.Products != null ?
                        View(await _context.Products.ToListAsync()) :
                        Problem("Entity set 'EtnaContext.Products'  is null.");
        }

        public async Task<IActionResult> Cart(int? productId)
        {
            List<int> cartIds = HttpContext.Session.GetObject<List<int>>("Cart");
            if (cartIds == null)
            {
                return View();
            }
            else
            {
                if (productId != null)
                {
                    cartIds.Remove(productId.Value);
                    HttpContext.Session.SetObject("Cart", cartIds);
                }

                List<Product> products = await _context.Products.Where(x => cartIds.Contains(x.ProductID)).ToListAsync();
                List<CartProductViewModel> cartProducts = products.Select(x => new CartProductViewModel
                {
                    Product = x,
                    Quantity = 1
                }
                ).ToList();

                
                return View(cartProducts);
            }
        }

        //muestra la compra ya hecha
        public IActionResult Order()
        {
            List<int> carrito = HttpContext.Session.GetObject<List<int>>("Cart");
            List<Product> productos = _context.Products.Where(x => carrito.Contains(x.ProductID)).ToList();
            HttpContext.Session.Remove("Cart");
            return View(productos);
        }

        #endregion

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductID == id)).GetValueOrDefault();
        }
    }
}
