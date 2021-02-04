using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MVCApp.Helpers;
using MVCApp.Models;
using MVCApp.Models.Entities;
using MVCApp.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCApp.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly TicketService ticketService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ProductService productService;

        public CheckoutController(TicketService ticketService, UserManager<ApplicationUser> userManager, ProductService productService)
        {
            this.ticketService = ticketService;
            this.userManager = userManager;
            this.productService = productService;
        }
        public IActionResult Index()
        {
            var list = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            return View(list);
        }

        public async Task<IActionResult> Buy()
        {
            var list = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            var ticket = new Ticket()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Buyer = await userManager.GetUserAsync(User),
                Items = list
            };
            if (await ticketService.CreateAsync(ticket))
            {
                foreach (var item in list)
                {
                    var product = item.Product;
                    product.Quantity -= item.Quantity;
                    await productService.UpdateAsync(item.Product.Id, product);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", null);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
    }
}
