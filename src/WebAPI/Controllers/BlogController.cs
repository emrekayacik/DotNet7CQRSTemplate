using Application.Business.Blog.Commands;
using Application.Business.Blog.Queries;
using Application.Common.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Base;
using WebAPI.Models;

namespace WebAPI.Controllers;
[Route("[controller]")]
[ApiController]
public class BlogController : ApiControllerBase
{

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> Get() => await Mediator.Send(new GetBlogsQuery());

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse>> GetBlogsById(Guid id) => await Mediator.Send(new GetBlogsByIdQuery() { Id = id});

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateBlogCommand command) => await Mediator.Send(command);

    [HttpPut]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateBlogModel model) => await Mediator.Send(new UpdateBlogCommand() { Id = model.Id,Header = model.Header,State = model.State, TextContent = model.TextContent,UpdatedBy= model.UpdatedBy});

    [HttpDelete("{id}")]
    public async Task<ActionResult<Guid>> Delete(Guid id,int deletedBy) => await Mediator.Send(new DeleteBlogCommand() { Id = id,DeletedBy= deletedBy});
}
