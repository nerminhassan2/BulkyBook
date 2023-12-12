using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkyWebRazor.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(20)]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [Range(1, 100, ErrorMessage ="Display order must be between 1-100")]
        [DisplayName("Display Order")]
        public int displayOrder { get; set; }
    }
}
