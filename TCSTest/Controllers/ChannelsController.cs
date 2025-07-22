

using Microsoft.AspNetCore.Mvc;
using TCSTest.Data;
using TCSTest.Models;

namespace TCSTest.Controllers;

[ApiController]
[Route("channels")]
public class ChannelsController : ControllerBase
{
    private readonly JsonDataService _service;

    public ChannelsController(JsonDataService service) => _service = service;

    [HttpGet]
    public IActionResult GetChannels() => Ok(_service.GetChannels());

    [HttpPost]
    public IActionResult AddChannel(ChannelManager channel)
    {
        var channels = _service.GetChannels();
        channel.channelId = channels.Any() ? Guid.NewGuid().ToString() : 1.ToString();
        channels.Add(channel);
        _service.SaveChannels(channels);
        return CreatedAtAction(nameof(GetChannels), channel);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateChannel(string id, ChannelManager updated)
    {
        var channels = _service.GetChannels();
        var channel = channels.FirstOrDefault(c => c.channelId == id.ToString());
        if (channel == null) return NotFound();
        channel.name = updated.name;
        channel.category = updated.category;
        channel.region = updated.region;
        _service.SaveChannels(channels);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteChannel(string id)
    {
        var channels = _service.GetChannels();
        var channel = channels.FirstOrDefault(c => c.channelId == id.ToString());
        if (channel == null) return NotFound();
        channels.Remove(channel);
        _service.SaveChannels(channels);
        return NoContent();
    }
}

