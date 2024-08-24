using FluentValidation;

namespace Sm.Crm.Application.Features.Notifications.Commands.CreateNotification;

public class CreateNotificationValidator : AbstractValidator<CreateNotificationCommand>
{
    public CreateNotificationValidator()
    
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Description).NotNull().WithMessage("Please specify a Description");
          
        }
}