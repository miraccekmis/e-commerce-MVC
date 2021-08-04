using CoreAndProducts.Data;
using CoreAndProducts.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAndProducts.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult VisualizeProductResult()
        {
            return Json(ProductList());
        }
        public List<Class1> ProductList()
        {
            List<Class1> cs = new List<Class1>();
            using (var c = new Context())
            {
                cs = c.Products.Select(x => new Class1
                {
                    productname = x.Name,
                    productstock = x.Stock
                }).ToList();
                return cs;
            }
        }
        public IActionResult Statistics()
        {
            Context c = new Context();
            var TotalProducts = c.Products.Count();
            ViewBag.d1 = TotalProducts;
            var TotalCategory = c.Categories.Count();
            ViewBag.d2 = TotalCategory;
            var StockSum = c.Products.Sum(x => x.Stock);
            ViewBag.d3 = StockSum;
            var MaxStock = c.Products.OrderByDescending(x => x.Stock).Select(y => y.Name).FirstOrDefault();
            ViewBag.d4 = MaxStock;
            var MinStock = c.Products.OrderBy(x => x.Stock).Select(y => y.Name).FirstOrDefault();
            ViewBag.d5 = MinStock;
            var AvgProduct = c.Products.Average(x => x.Price).ToString("0.00");
            ViewBag.d6 = AvgProduct;
            var MaxPrice = c.Products.OrderByDescending(x => x.Price).Select(y => y.Name).FirstOrDefault();
            ViewBag.d7 = MaxPrice;
            return View();
        }


    }
}
