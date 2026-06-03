// CartItem.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL
{
    public class CartItem 
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        /*-------------------------------------------------------*/
        // Navigation property for the relationship with Product (M-1)
        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        // Navigation property for the relationship with User (M-1)
        [Required]
        public required string UserId { get; set; }   // string because Identity uses GUID
        public virtual ApplicationUser User { get; set; }
        /*-------------------------------------------------------*/

        public override string ToString()
        {
            return $"Id={Id}, UserId={UserId}, ProductId={ProductId}, Quantity={Quantity}";
        }
    }
}