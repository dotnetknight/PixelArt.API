using FluentValidation;
using PixelArt.Models.Requests;

namespace PixelArtAPI.Validators
{
    public class ConvertRequestValidator : AbstractValidator<ConvertRequest>
    {
        public ConvertRequestValidator()
        {
            RuleFor(c => c.ImageFile)
                .NotEmpty()
                .WithMessage("Image file is required");

            RuleFor(c => c.HexCodes)
                .NotNull()
                .WithMessage("Hex codes required");
        }
    }
}