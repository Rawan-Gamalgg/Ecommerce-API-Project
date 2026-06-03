using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL
{
    public class Product : IAuditEntity
    {
        /*-------------------------------------------------------*/
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        public required string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(10, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Available quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Count cannot be negative")]
        public int Count { get; set; }//available quantity

        [Required(ErrorMessage = "Expiry date is required")]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }
        public string? ImageURL { get; set; }
        
        /*-------------------------------------------------------*/
        // Navigation property for the relationship with Category(1-M)
        [Required(ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        /*-------------------------------------------------------*/


        /*-------------------------------------------------------*/
        public override string ToString()
        {
            return $"Id={Id}, Name={Name}, Description={Description}, Price={Price}, Count={Count}, CategoryId={CategoryId}";
        }
    }
}
