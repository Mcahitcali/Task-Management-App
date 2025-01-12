using FluentValidation;
using backend.Models;

namespace backend.Validators
{
    public class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
    {
        public UpdateTaskRequestValidator()
        {
            RuleFor(x => x.Title)
                .Must(x => x == null || x is string).WithMessage("Title must be a string value")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters")
                .MinimumLength(1).WithMessage("Title must not be empty")
                .When(x => x.Title != null);

            RuleFor(x => x.Description)
                .Must(x => x == null || x is string).WithMessage("Description must be a string value")
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters")
                .When(x => x.Description != null);

            RuleFor(x => x.IsCompleted)
                .Must(x => x is bool).WithMessage("IsCompleted must be a boolean value");

            RuleFor(x => x)
                .Custom((request, context) =>
                {
                    if (request.Title == null && request.Description == null && request.IsCompleted == false)
                    {
                        context.AddFailure("At least one property must be updated");
                    }
                });
        }
    }
} 