using Application.Common.Models;
using Application.Laptops.Queries.GetLaptopsWithPagination;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LaptopsController : ControllerBase
{
    private ISender? _mediator;

    private ISender Mediator =>
        _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    [HttpGet]
    public async Task<ActionResult<PaginatedList<Laptop>>> GetLaptopsWithPagination(
        [FromQuery] GetLaptopsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }
}
