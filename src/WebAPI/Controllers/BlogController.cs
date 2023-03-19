using Application.Business.Blog.Commands.CreateBlogCommand;
using Application.Business.Blog.Queries.GetBlogs;
using Application.Common.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> Get() => await Mediator.Send(new GetBlogsQuery());

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateBlogCommand command) => await Mediator.Send(command);
}
