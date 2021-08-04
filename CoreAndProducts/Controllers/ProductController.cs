using CoreAndProducts.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreAndProducts.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using System.IO;

namespace CoreAndProducts.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository productRepository = new ProductRepository();
        Context c = new Context();
        public IActionResult Index(int page = 1)
        {

            return View(productRepository.TList("Category").ToPagedList(page, 3));
        }
        public IActionResult ProductAdd()
        {
            List<SelectListItem> values = (from x in c.Categories.ToList() select new SelectListItem { Text = x.CategoryName, Value = x.CategoryID.ToString() }).ToList();
            ViewBag.v1 = values;
            return View();
        }

        [HttpPost]
        public IActionResult ProductAdd(AddProduct p)
        {
            Product f = new Product();
            if (p.ImageURL != null)
            {

                var extension = Path.GetExtension(p.ImageURL.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/resimler/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                p.ImageURL.CopyTo(stream);
                f.ImageURL = "/resimler/" + newImageName;
            }
            f.Name = p.Name;
            f.Price = p.Price;
            f.Stock = p.Stock;
            f.CategoryID = p.CategoryID;
            f.Description = p.Description;
            productRepository.TAdd(f);
            return RedirectToAction("Index");
        }
        public IActionResult ProductDelete(int id)
        {
            productRepository.TDelete(new Product { ProductID = id });
            return RedirectToAction("Index");
        }
        public IActionResult ProductGet(int id)
        {
            List<SelectListItem> values = (from y in c.Categories.ToList() select new SelectListItem { Text = y.CategoryName, Value = y.CategoryID.ToString() }).ToList();
            ViewBag.v1 = values;
            var x = productRepository.TGet(id);
            Product p = new Product()
            {
                ProductID = x.ProductID,
                CategoryID = x.CategoryID,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock,
                Description = x.Description,
                ImageURL = x.ImageURL
            };
            return View(p);
        }
        [HttpPost]
        public IActionResult ProductUpdate(Product p)
        {
            var x = productRepository.TGet(p.ProductID);
            x.Name = p.Name;
            x.Stock = p.Stock;
            x.Price = p.Price;
            x.ImageURL = p.ImageURL;
            x.Description = p.Description;
            x.CategoryID = x.CategoryID;
            productRepository.TUpdate(x);
            return RedirectToAction("Index");
        }
    }
}
