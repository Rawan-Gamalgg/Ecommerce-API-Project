// Lab4/ViewModels/Product/ProductCreateVM.cs
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;

namespace Ecommerce.BLL
{
    public class ProductCreateDto
    {
        #region Get From User
        //public int Id { get; set; }

        //[Required(ErrorMessage = "Name is required")]
        //[Remote(action: "IsTitleUnique", controller: "Product",
        //       AdditionalFields = nameof(Id),
        //       ErrorMessage = "A product with this name already exists.")]
        //[StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; }

        //[Range(0, int.MaxValue, ErrorMessage = "Count cannot be negative")]
        public int Count { get; set; }

        //[Required(ErrorMessage = "Price is required")]
        //[Range(10, double.MaxValue, ErrorMessage = "Price must be greater than 10")]
        public decimal Price { get; set; }

        //[StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        //[Required(ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }

        //[Required(ErrorMessage = "Expiry date is required")]
        //[DataType(DataType.Date)]
        //[CustomValidation(typeof(customDateValidator), nameof(customDateValidator.ValidateNotFuture))]
        public DateTime? ExpiryDate { get; set; }

        public string? ImageUrl { get; set; }


        #endregion


    }
}