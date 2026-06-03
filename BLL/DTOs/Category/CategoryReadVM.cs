
namespace Ecommerce.BLL
{
    public class CategoryReadDto
    {
        /*-------------------------------------------------------*/
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? ImageUrl { get; set; }
        public int? ProductsCount { get; set; }
        public List<ProductReadDto>? Products { get; set; }
        public DateTime CreatedAt { get; set; }
        /*-------------------------------------------------------*/


    }
}
