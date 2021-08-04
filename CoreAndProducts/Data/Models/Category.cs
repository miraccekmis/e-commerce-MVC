using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAndProducts.Data.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "Kategori Adı Boş Geçilemez")]
        [StringLength(20, ErrorMessage = "Lütfen 4-20 Karakter Arasında Giriş Yapınız.", MinimumLength = 4)]
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public bool Status { get; set; }
        public List<Product> Products { get; set; }

    }
}
