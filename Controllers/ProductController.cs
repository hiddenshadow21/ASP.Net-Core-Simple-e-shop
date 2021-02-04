using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MVCApp.Models.Entities;
using MVCApp.Service;
using System.Threading.Tasks;

namespace MVCApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        // GET: ProductController
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        // GET: ProductController/Details/5
        [AllowAnonymous]
        public async Task<ActionResult> Details(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            return View(product);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = ObjectId.GenerateNewId().ToString();
                await _productService.CreateAsync(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = id;
                await _productService.UpdateAsync(id, product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Product product = await _productService.GetByIdAsync(id);
            await _productService.RemoveAsync(product);
            return RedirectToAction("Index");
        }
    }
}
