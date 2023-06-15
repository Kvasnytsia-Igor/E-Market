using Application.Common.Models;
using Application.Laptops.Commands.CreateLaptop;
using Application.Laptops.Commands.DeleteLaptop;
using Application.Laptops.Commands.UpdateLaptop;
using Application.Laptops.Queries.GetLaptopById;
using Application.Laptops.Queries.GetLaptopsWithPagination;
using Azure.Core;
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

    private IWebHostEnvironment? _environment;

    private IWebHostEnvironment Environment =>
        _environment ??= HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();

    private ILogger<LaptopsController>? _logger;

    private ILogger<LaptopsController> Logger =>
        _logger ??= HttpContext.RequestServices.GetRequiredService<ILogger<LaptopsController>>();

    private async Task<ActionResult> Go(IRequest<ApiResponse> request)
    {
        try
        {
            ApiResponse response = await Mediator.Send(request);
            if (response.StatusCode == StatusCodes.Status204NoContent)
            {
                return StatusCode(response.StatusCode);
            }
            return StatusCode(response.StatusCode, response.Data);
        }
        catch (Exception ex)
        {
            string requestName = typeof(IRequest<ApiResponse>).Name;
            Logger.LogError(ex, "Unhandled Exception for Request {Name} {@Request}", requestName, request);
            if (Environment.IsDevelopment())
            {
                throw;
            }
            return StatusCode(StatusCodes.Status500InternalServerError,
                new
                {
                    Message = "Ops! Call the service provider"
                });
        }
    }

    [HttpGet("{PageNumber:int}/{PageSize:int}")]
    public async Task<ActionResult> GetLaptopsWithPagination([FromHeader] GetLaptopsWithPaginationQuery query)
        => await Go(query);

    [HttpGet("{Id:guid}")]
    public async Task<ActionResult> GetLaptopById([FromHeader] GetLaptopByIdQuery query)
        => await Go(query);

    [HttpPost]
    public async Task<ActionResult> CreateLaptop([FromBody] CreateLaptopCommand command)
        => await Go(command);

    [HttpPut("{Id:guid}")]
    public async Task<ActionResult> UpdateLaptop(Guid id, [FromBody] UpdateLaptopDTO userDTO)
        => await Go(new UpdateLaptopCommand
        {
            Id = id,
            LaptopDTO = userDTO
        });

    [HttpDelete("{Id:guid}")]
    public async Task<ActionResult> DeleteLaptop([FromHeader] DeleteLaptopCommand command) 
        => await Go(command);
}
