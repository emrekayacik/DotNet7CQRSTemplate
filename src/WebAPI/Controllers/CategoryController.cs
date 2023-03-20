using Application.Business.Blog.Commands;
using Application.Business.Category.Commands;
using Application.Business.Category.Queries;
using Application.Common.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    [HttpGet]
    public async Task<ActionResult<ApiResponse>> Get() => await Mediator.Send(new GetCategoriesQuery());

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateCategoryCommand command) => await Mediator.Send(command);

    [HttpPut]
    public async Task<ActionResult<Guid>> Update([FromBody] UpdateCategoryModel model) => await Mediator.Send(new UpdateCategoryCommand() { Id = model.Id, Name= model.Name, UpdatedBy = model.UpdatedBy });

    [HttpDelete("{id}")]
    public async Task<ActionResult<Guid>> Delete(Guid id, int deletedBy) => await Mediator.Send(new DeleteCategoryCommand() { Id = id, DeletedBy = deletedBy });
}
