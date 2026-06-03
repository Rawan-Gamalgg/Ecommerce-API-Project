namespace Ecommerce.BLL
{
    public class ProductReadDto
    {
        /*-------------------------------------------------------------------*/
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime CreatedAt { get; set; }
        /*-------------------------------------------------------------------*/

    }
}
