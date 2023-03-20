using FluentValidation;

namespace Application.Business.Blog.Commands;
public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
{
    public CreateBlogCommandValidator()
    {
        RuleFor(v => v.Header)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.TextContent)
            .MinimumLength(30);
    }
}
