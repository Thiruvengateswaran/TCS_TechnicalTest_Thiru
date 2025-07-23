using Microsoft.AspNetCore.Mvc;
using TCSTest.Data;
using TCSTest.Models;

namespace TCSTest.Controllers;

[ApiController]
[Route("catalog")]
public class CatalogController : ControllerBase
{
    private readonly JsonDataService _service;



    public CatalogController(JsonDataService service) => _service = service;

    // Read Catalog values
    [HttpGet("catalog")]
    public IActionResult GetCatelogs() => Ok(_service.GetContent());

    //Add new catalog item
    [HttpPost("catalog")]
    public IActionResult AddCatelog(ContentCatalog catelog)
    {
        var catelogs = _service.GetContent();
        catelog.contentId = catelogs.Any() ? Guid.NewGuid().ToString() : 1.ToString();
        catelogs.Add(catelog);
        _service.SaveContent(catelogs);
        return CreatedAtAction(nameof(GetCatelogs), catelog);
    }

    //Update existing catelog item

    [HttpPut("catelogs/{id}")]
    public IActionResult UpdateCatalog(string id, ContentCatalog updated)
    {
        var catelogs = _service.GetContent();
        var catelog = catelogs.FirstOrDefault(m => m.contentId == id.ToString());
        if (catelog == null) return NotFound();
        catelog.title = updated.title;
        catelog.genre = updated.genre;
        catelog.durationMinutes = updated.durationMinutes;
        _service.SaveContent(catelogs);
        return NoContent();
    }

    //Delete catelog item

    [HttpDelete("catelogs/{id}")]
    public IActionResult DeleteMovie(string id)
    {
        var catelogs = _service.GetContent();
        var catelog = catelogs.FirstOrDefault(m => m.contentId == id.ToString());
        if (catelog == null) return NotFound();
        catelogs.Remove(catelog);
        _service.SaveContent(catelogs);
        return NoContent();
    }


}

