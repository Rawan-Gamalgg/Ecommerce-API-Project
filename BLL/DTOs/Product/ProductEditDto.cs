using Ecommerce.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.BLL
{
    public class ProductEditDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        //[Remote(action: "IsTitleUnique", controller: "Product",
        //       AdditionalFields = nameof(Id),
        //       ErrorMessage = "A product with this name already exists.")]
        //[StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Count cannot be negative")]
        public int Count { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(10, double.MaxValue, ErrorMessage = "Price must be > 10")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public string Description { get; set; }

        //[Required(ErrorMessage = "Expiry date is required")]
        //[DataType(DataType.Date)]
        //[CustomValidation(typeof(customDateValidator), nameof(customDateValidator.ValidateNotFuture))]
        //public DateTime? ExpiryDate { get; set; }
        ////public string? ImageURL { get; set; }
        //public IFormFile? Image { get; set; }


        //public List<SelectListItem>? Categories { get; set; }
    }
}