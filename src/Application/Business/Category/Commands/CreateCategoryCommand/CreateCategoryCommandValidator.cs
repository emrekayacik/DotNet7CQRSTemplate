using FluentValidation;

namespace Application.Business.Category.Commands.CreateCategoryCommand;
public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(25)
            .MinimumLength(5)
            .NotEmpty();
    }
}
