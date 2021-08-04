using CoreAndProducts.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAndProducts.ViewComponents
{
    public class ProductListByCategory:ViewComponent
    {
        public IViewComponentResult Invoke(int id=0)
        {
            if (id==0)
            {
                ProductRepository productRepository1 = new ProductRepository();
                var productList1 = productRepository1.TList();
                return View(productList1);
            }
            ProductRepository productRepository = new ProductRepository();
            var productList = productRepository.List(x=>x.CategoryID==id);
            return View(productList);
        }
    }
}
