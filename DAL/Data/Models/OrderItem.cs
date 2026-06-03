// OrderItem.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL
{
    public class OrderItem 
    {
        /*-------------------------------------------------------*/
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }        // snapshot of price at time of purchase

        /*-------------------------------------------------------*/
        // Navigation property for the relationship with Order (M-1)
        [Required]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        /*-------------------------------------------------------*/

        // Navigation property for the relationship with Product (M-1)
        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        /*-------------------------------------------------------*/

        public override string ToString()
        {
            return $"Id={Id}, OrderId={OrderId}, ProductId={ProductId}, Quantity={Quantity}, UnitPrice={UnitPrice}";
        }
        /*-------------------------------------------------------*/

    }
}