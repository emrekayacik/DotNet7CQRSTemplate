using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Business.Blog.Commands;

public class UpdateBlogCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Header { get; set; } = string.Empty;
    public string? TextContent { get; set; } = string.Empty;
    public int? UpdatedBy { get; set; } = 0;
    public BlogState State { get; set; } = 0;
}

public class UpdateBlogCommandandler : IRequestHandler<UpdateBlogCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public UpdateBlogCommandandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
    {
        var entity = _context.Blogs.FirstOrDefault(a => a.Id == request.Id && a.DeletedTime == null);

        if (entity == null)
            return Guid.Empty;

        entity.Header = request.Header;
        entity.TextContent= request.TextContent;
        entity.UpdatedBy = request.UpdatedBy;
        entity.UpdatedTime = DateTime.Now;
        entity.State = request.State;

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
