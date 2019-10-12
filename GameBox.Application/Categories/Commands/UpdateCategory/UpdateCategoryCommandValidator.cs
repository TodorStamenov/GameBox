using FluentValidation;
using GameBox.Application.Infrastructure;

namespace GameBox.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(c => c.Name)
                .Length(Constants.Category.NameMinLength, Constants.Category.NameMaxLength)
                .NotEmpty();
        }
    }
}