using Application.Business.Blog.Queries.GetBlogs;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Business.Category.Queries.GetCategoriesQuery;
public class GetCategoriesQuery : IRequest<ApiResponse>
{
    private readonly IApplicationDbContext _context;

    public GetCategoriesQuery(IApplicationDbContext context)
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
            Data = _context.Categories.Where(a => a.DeletedTime == null).ToList()
        };
    }
}
