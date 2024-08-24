using FluentValidation;

namespace Sm.Crm.Application.Features.Customers.Commands.UploadProfileImage;

public class UploadProfileImageValidator : AbstractValidator<UploadProfileImageCommand>
{
    public UploadProfileImageValidator()
    {
        RuleFor(x => x.CustomerId).NotNull();
        //RuleFor(x => x.ProfileImageUrl).NotEmpty().NotNull();
    }
}