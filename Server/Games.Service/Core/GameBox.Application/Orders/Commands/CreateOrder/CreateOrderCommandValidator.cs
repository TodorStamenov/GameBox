using FluentValidation;

namespace GameBox.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(o => o.Username).NotEmpty();
        RuleFor(o => o.GameIds).NotEmpty();
    }
}
