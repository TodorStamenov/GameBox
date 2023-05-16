using FluentValidation;
using GameBox.Application.Infrastructure;

namespace GameBox.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.Name)
            .Length(Constants.Category.NameMinLength, Constants.Category.NameMaxLength)
            .NotEmpty();
    }
}
