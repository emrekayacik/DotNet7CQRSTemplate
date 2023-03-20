using Application.Common.Helpers;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Business.Blog.Queries;
public class GetBlogsQuery : IRequest<ApiResponse>
{
}

public class GetBlogsQueryHandler : IRequestHandler<GetBlogsQuery, ApiResponse>
{
    private readonly IApplicationDbContext _context;

    public GetBlogsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
    {
        return new ApiResponse
        {
            Code = ApiResponseType.Success,
            Message = "Success",
            Ok = true,
            Data = _context.Blogs.Where(a => a.DeletedTime == null).ToList()
        };
    }
}