using FluentValidation;
using GameBox.Application.Infrastructure;

namespace GameBox.Application.Games.Commands.UpdateGame;

public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
{
    public UpdateGameCommandValidator()
    {
        RuleFor(g => g.Title)
            .Length(Constants.Game.TitleMinLength, Constants.Game.TitleMaxLength)
            .NotEmpty();

        RuleFor(g => g.Price)
            .GreaterThanOrEqualTo(Constants.Game.MinPrice)
            .LessThanOrEqualTo(Constants.Game.MaxPrice);

        RuleFor(g => g.Size)
            .GreaterThanOrEqualTo(Constants.Game.MinSize)
            .LessThanOrEqualTo(Constants.Game.MaxSize);

        RuleFor(g => g.VideoId)
            .Length(Constants.Game.MinVideoIdLength, Constants.Game.MaxVideoIdLength)
            .NotEmpty();

        RuleFor(g => g.Description)
            .MinimumLength(Constants.Game.MinDescriptionLength)
            .NotEmpty();

        RuleFor(g => g.ReleaseDate)
            .NotEmpty();
    }
}
