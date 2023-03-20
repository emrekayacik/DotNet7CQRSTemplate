using Application.Common.Helpers;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Business.Category.Queries;
public class GetCategoriesQuery : IRequest<ApiResponse>
{
    
}


public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, ApiResponse>
{
    private readonly IApplicationDbContext _context;

    public GetCategoriesQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
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

