using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.DAL
{
    public class Category: IAuditEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be 2-100 characters")]
        public required string Name { get; set; }
        public string? ImageURL { get; set; }

        /*--------------------------------------------------*/
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();
        
        /*--------------------------------------------------*/
        public override string ToString()
        {
            return $"Id={Id}, Name={Name}, Createion Date={CreatedAt}";
        }
    }
}
