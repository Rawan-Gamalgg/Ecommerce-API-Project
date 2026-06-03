// Order.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.DAL
{
  
    public class Order : IAuditEntity
    {
        /*-------------------------------------------------------*/
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }     

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        /*-------------------------------------------------------*/
        // Navigation property for the relationship with User (M-1)
        [Required]
        public required string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        /*-------------------------------------------------------*/

        // Navigation property for the relationship with OrderItems (1-M)
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        /*-------------------------------------------------------*/
        public override string ToString()
        {
            return $"Id={Id}, UserId={UserId}, TotalAmount={TotalAmount}, Status={Status}, CreatedAt={CreatedAt}";
        }
        /*-------------------------------------------------------*/

    }
}