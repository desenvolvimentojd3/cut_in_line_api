using CutInLine.Models.Class;
using CutInLine.Models.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CutInLine.Controllers;

[ApiController]
[Route("events")]
public class EventsController : ControllerBase
{
    private readonly IEvents _events;

    public EventsController(IEvents events)
    {
        _events = events;
    }

    [HttpPut]
    [Authorize]
    public async Task<dynamic> SignUp([FromHeader] string token, [FromBody] Events events) => await _events.Save(events, token);

    [HttpPost]
    [Route("get")]
    public async Task<dynamic> GetProducst([FromHeader] string token, [FromBody] SearchHelper search) => await _events.GetEvents(search, token);

    [HttpDelete]
    [Route("{id}")]
    public async Task<dynamic> GetProducst([FromHeader] string token, int id) => await _events.Delete(id, token);

    [HttpGet]
    [Route("{id}")]
    public async Task<dynamic> GetById([FromHeader] string token, int id) => await _events.GetById(id, token);
}
