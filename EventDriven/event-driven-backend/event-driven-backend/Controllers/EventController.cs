using event_driven_backend.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace event_driven_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : Controller
{
    private Context context { get; set; }
    private readonly ILogger<EventController> _logger;

    public EventController(ILogger<EventController> logger, Context context)
    {
        _logger = logger;
        this.context = context;
    }

    [HttpGet]
    [Route("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Event>>> GetAll([FromQuery] int calendarId)
    {
        var calendar = await context.Calendars.FirstOrDefaultAsync(c => c.ID == calendarId);
        if (calendar == null)
        {
            return BadRequest();
        }
        var events = await context.Events
            .Where(e => e.Calendar.ID == calendarId)
            .ToListAsync();

        
        if (events == null)
        {
            return BadRequest();
        }
        return Ok(events);
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create([FromBody] NewEventDTO ne)
    {
        var calendar = await context.Calendars.FirstOrDefaultAsync(c => c.ID == ne.calendarId);
        if (calendar == null)
        {
            return BadRequest();
        }
        var newEvent = new Event { Calendar = calendar, Name = ne.Name, Color = ne.Color, Start = ne.Start, End = ne.End};
        calendar.Events.Add(newEvent);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
        return Ok(newEvent);
    }

    [HttpDelete]
    [Route("delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete([FromQuery] int eventId)
    {
        var eventToDelete = await context.Events.FirstOrDefaultAsync(e => e.ID == eventId);
        if (eventToDelete == null)
        {
            return BadRequest();
        }
        context.Events.Remove(eventToDelete);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
        return Ok($"Event {eventToDelete.Name} has been deleted");
    }

    [HttpPut]
    [Route("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update([FromBody] Event newEvent)
    {
        var oldEvent = await context.Events.FirstOrDefaultAsync(e => e.ID == newEvent.ID);
        if (oldEvent == null)
        {
            return BadRequest();
        }
        oldEvent.Name = newEvent.Name;
        oldEvent.Color = newEvent.Color;
        oldEvent.Start = newEvent.Start;
        oldEvent.End = newEvent.End;
        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
        return Ok(newEvent);
    }


}

