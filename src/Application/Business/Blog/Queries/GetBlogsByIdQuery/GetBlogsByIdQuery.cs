using Application.Common.Helpers;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Business.Blog.Queries;

public class GetBlogsByIdQuery : IRequest<ApiResponse>
{
    public Guid Id { get; set; } = Guid.Empty;
}

public class GetBlogsByIdQueryHandler : IRequestHandler<GetBlogsByIdQuery, ApiResponse>
{
    private readonly IApplicationDbContext _context;

    public GetBlogsByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse> Handle(GetBlogsByIdQuery request, CancellationToken cancellationToken)
    {
        return new ApiResponse
        {
            Code = ApiResponseType.Success,
            Message = "Success",
            Ok = true,
            Data = _context.Blogs.FirstOrDefault(predicate: a => a.DeletedTime == null && a.Id == request.Id)
        };
    }
}
