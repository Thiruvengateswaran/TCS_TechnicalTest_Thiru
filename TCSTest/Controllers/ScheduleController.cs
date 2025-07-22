using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
using TCSTest.Data;
using TCSTest.Models;

namespace TCSTest.Controllers;

[ApiController]
[Route("schedule")]
public class ScheduleController : ControllerBase
{
    private readonly JsonDataService _service;

    public ScheduleController(JsonDataService service) => _service = service;

    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetSchedule());

    [HttpGet("current")]
    public IActionResult GetCurrentlyAiring()
    {
        var now = DateTime.Now;
        var airing = _service.GetSchedule().Where(s => DateTime.Parse(s.airTime) <= now && DateTime.Parse(s.endTime) >= now);
        return Ok(airing);
    }

    [HttpGet("channel/{channelId}")]
    public IActionResult GetByChannel(string channelId)
    {
        var items = _service.GetSchedule().Where(s => s.channelId == channelId);
        return Ok(items);
    }

    

    [HttpPost]
    public IActionResult AddSchedule(ScheduleSystem item)
    {
        var items = _service.GetSchedule();
        items.Add(item);
        _service.SaveSchedule(items);
        return CreatedAtAction(nameof(GetAll), item);
    }
}


