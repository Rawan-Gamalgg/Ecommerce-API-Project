using Ecommerce.BLL;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageManager _imageManager;
        private readonly IValidator<ImageUploadDto> _validator;
        private readonly ICategoryManager _categoryManager;
        private readonly IProductManager _productManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        /*-----------------------------------------------------------------------------------*/

        public ImagesController(
            IImageManager imageManager,
            IWebHostEnvironment webHostEnvironment,
            IValidator<ImageUploadDto> validator,
            ICategoryManager categoryManager,
            IProductManager productManager)
        {
            _imageManager = imageManager;
            _categoryManager = categoryManager;
            _productManager = productManager;
            _validator = validator;
            _webHostEnvironment = webHostEnvironment;
        }
        /*-----------------------------------------------------------------------------------*/
        //post : api/image/upload
        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult<GeneralResult<ImageUploadResultDto>>> UploadAsync([FromForm] ImageUploadDto imageUploadDto)//sending images as form data
        {
            var validationResult = await _validator.ValidateAsync(imageUploadDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(GeneralResult<ImageUploadResultDto>.Fail(errors));
            }

            var schema = Request.Scheme;
            var hostName = Request.Host.Value;
            var basePath = _webHostEnvironment.ContentRootPath;
            var result = await _imageManager.UploadAsync(
                imageUploadDto,
                basePath,
                schema,
                hostName,
                folderName: "Images");
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        /*-----------------------------------------------------------------------------------*/
        //POST: api/image/product/{id}
        [HttpPost]
        [Route("product/{id:int}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<GeneralResult<ImageUploadResultDto>>> UploadProductImage(
            [FromRoute] int id,
            [FromForm] ImageUploadDto imageUploadDto)
        {
            var validationResult = await _validator.ValidateAsync(imageUploadDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(GeneralResult<ImageUploadResultDto>.Fail(errors));
            }

            var schema = Request.Scheme;
            var hostName = Request.Host.Value;
            var basePath = _webHostEnvironment.ContentRootPath;
            var uploadResponse = await _imageManager.UploadAsync(
                imageUploadDto,
                basePath,
                schema,
                hostName,
                folderName: "Products");

            if (!uploadResponse.Success)
                return BadRequest(uploadResponse);

            // 3. save url to product
            var updateResponse = await _productManager.UpdateImageAsync(id, uploadResponse.Data!.ImageUrl);
            if (!updateResponse.Success)
                return NotFound(updateResponse);

            return Ok(uploadResponse);
        }
        /*------------------------------------------------------------------*/
        // POST: api/image/category/{id}
        [HttpPost("category/{id:int}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<GeneralResult<ImageUploadResultDto>>> UploadCategoryImage(
            [FromRoute] int id,
            [FromForm] ImageUploadDto imageUploadDto)
        {
            var validationResult = await _validator.ValidateAsync(imageUploadDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(GeneralResult<ImageUploadResultDto>.Fail(errors));
            }

            var schema = Request.Scheme;
            var hostName = Request.Host.Value;
            var basePath = _webHostEnvironment.ContentRootPath;
            var uploadResponse = await _imageManager.UploadAsync(
                imageUploadDto,
                basePath,
                schema,
                hostName,
                folderName: "Categories");

            if (!uploadResponse.Success)
                return BadRequest(uploadResponse);

            var updateResponse = await _categoryManager.UpdateImageAsync(id, uploadResponse.Data!.ImageUrl);
            if (!updateResponse.Success)
                return NotFound(updateResponse);

            return Ok(uploadResponse);
        }

    }
}
