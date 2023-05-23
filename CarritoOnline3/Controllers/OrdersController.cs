using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarritoOnline3.Models.DAL;
using CarritoOnline3.Models.Entities;
using CarritoOnline3.Models;
using CarritoOnline3.Extensions;

namespace CarritoOnline3.Controllers
{
    public class OrdersController : Controller
    {
        private readonly EtnaContext _context;

        public OrdersController(EtnaContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            IEnumerable<CarritoOnline3.Models.Entities.Order> orders = _context.Orders;
            IEnumerable<CarritoOnline3.Models.Entities.OrderDetail> orderDetails = _context.OrderDetails;
            
            

            var viewModel = new OrderViewModel
            {
               orders = orders,
               orderDetails = orderDetails
              
            };

            return _context.Products != null ? 
                          View(viewModel) :
                          Problem("Entity set 'EtnaContext.Products'  is null.");
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }


        // POST: Orders/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        /*public async Task<IActionResult> CreateOrder()
        {
            List<int> ids = HttpContext.Session.GetObject<List<int>>("Cart");
            List<Product> details = new List<Product>();

            foreach (var item in ids)
            {
                Product product = (Product)_context.Products.Where(x => x.ProductID == item);
                details.Add(product);
            }

            Order order = new Order();

            OrderDetail detail = new OrderDetail();

            foreach (var item in details)
            {

                detail.Product = item.Product;
                detail.Quantity = item.Quantity;

                if(detail != null)
                    order.OrderDetail.Add(detail);
               
            }
            var totalPrice = order.OrderDetail.Sum(x => x.Product.Price * x.Quantity);
            order.TotalPrice = totalPrice;

            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        }*/


        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductID == id)).GetValueOrDefault();
        }
    }
}
