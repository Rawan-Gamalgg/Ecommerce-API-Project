namespace Ecommerce.BLL
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItemReadDto> OrderItems { get; set; }
    }
}

