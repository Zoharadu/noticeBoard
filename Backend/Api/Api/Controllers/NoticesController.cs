using Api.Mappers;
using Api.Requests;
using Api.Responses;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoticesController : ControllerBase
{
    private readonly NoticeService _service;

    public NoticesController(NoticeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<NoticeResponse>>> GetAll()
    {
        var notices = await _service.GetAllAsync();
        return Ok(notices.Select(n => n.ToResponse()));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<NoticeResponse>> GetById(Guid id)
    {
        var notice = await _service.GetByIdAsync(id);
        if (notice is null) return NotFound();

        return Ok(notice.ToResponse());
    }

    [HttpPost]
    public async Task<ActionResult<NoticeResponse>> Create(CreateNoticeRequest request)
    {
        var location = new Location(
            request.Location.Latitude,
            request.Location.Longitude,
            request.Location.Address);

        var notice = await _service.CreateAsync(
            request.Title,
            request.Content,
            location);

        return CreatedAtAction(
            nameof(GetById),
            new { id = notice.Id },
            notice.ToResponse());
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateNoticeRequest request)
    {
        var location = new Location(
            request.Location.Latitude,
            request.Location.Longitude,
            request.Location.Address);

        var updated = await _service.UpdateAsync(
            id,
            request.Title,
            request.Content,
            location);

        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}
