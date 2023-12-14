using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(20)]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [Range(1, 100)]
        [DisplayName("Display Order")]
        public int displayOrder { get; set; } 
    }
}
