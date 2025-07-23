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

    // Get all the schedule items
    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetSchedule());

    //Get the current Telecasting/Airing items
    [HttpGet("current")]
    public IActionResult GetCurrentlyAiring()
    {
        var now = DateTime.Now;
        var airing = _service.GetSchedule().Where(s => DateTime.Parse(s.airTime) <= now && DateTime.Parse(s.endTime) >= now);
        return Ok(airing);
    }
    // Get the current Airing item based on the channel
    [HttpGet("channel/{channelId}")]
    public IActionResult GetByChannel(string channelId)
    {
        var items = _service.GetSchedule().Where(s => s.channelId == channelId);
        return Ok(items);
    }

    // Add new schedule
    [HttpPost]
    public IActionResult AddSchedule(ScheduleSystem item)
    {
        var items = _service.GetSchedule();
        items.Add(item);
        _service.SaveSchedule(items);
        return CreatedAtAction(nameof(GetAll), item);
    }
}


