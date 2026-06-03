using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.BLL
{
    public class ImageUploadDtoValidator : AbstractValidator<ImageUploadDto>
    {
        private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png" };
        private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5MB

        public ImageUploadDtoValidator()
        {


            RuleFor(x => x.File)
                .NotNull().WithMessage("Please select a file to upload");

            When(x => x.File != null, () =>
            {
                RuleFor(x => x.File.Length)
                    .GreaterThan(0).WithMessage("File is empty")
                    .LessThanOrEqualTo(MaxFileSizeBytes)
                    .WithMessage("File size cannot exceed 5MB");

                RuleFor(x => x.File.FileName)
                    .Must(HaveAllowedExtension)
                    .WithMessage("Only .jpg, .jpeg and .png files are allowed");
            });
        }
        /*------------------------------------------------------------------*/
        private bool HaveAllowedExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return _allowedExtensions.Contains(extension);
        }
        /*------------------------------------------------------------------*/
    }
}

