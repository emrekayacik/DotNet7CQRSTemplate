using Application.Common.Interfaces;
using MediatR;

namespace Application.Business.Blog.Commands;
public class CreateBlogCommand : IRequest<Guid>
{
    public string Header { get; set; } = string.Empty;
    public string? TextContent { get; set; } = string.Empty;
    public int? CreatedBy { get; set; } = 0;
}

public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateBlogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Blog
        {
            Header = request.Header,
            TextContent = request.TextContent,
            CreatedBy = request.CreatedBy,
            CreatedTime = DateTime.Now,
            State = Domain.Enums.BlogState.Pending
        };

        _context.Blogs.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
