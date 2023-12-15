using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Bulky.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]  
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }

        [Required]
        [Display(Name = "List Price")]
        [Range(1, 1000)]
        public double ListPrice { get; set; }

        [Required]
        [Display(Name ="Price for 1-50")]
        [Range(1,1000)]
        public int Price { get; set; }

        [Required]
        [Display(Name = "Price for 50+")]
        [Range(1, 1000)]
        public int Price50 { get; set; }

        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1, 1000)]
        public int Price100 { get; set; }

        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]

        [ValidateNever]
        public Category Category { get; set; }
        [ValidateNever]
        public string ImageURL { get; set; }

    }
}
