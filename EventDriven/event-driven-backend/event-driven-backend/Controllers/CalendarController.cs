using Microsoft.AspNetCore.Mvc;

namespace event_driven_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarController : ControllerBase
    {

        private Context context { get; set; }

        private readonly ILogger<CalendarController> _logger;

        public CalendarController(ILogger<CalendarController> logger, Context context)
        {
            _logger = logger;
            this.context = context;
        }

        [HttpGet]
        [Route("get-all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<Calendar>>> GetAll([FromQuery] int communityID)
        {
            var calendars = await context.Communities
                .Where(c => c.ID == communityID)
                .Select(c => c.Calendar)
                .FirstOrDefaultAsync();

            if (calendars == null)
            {
                return BadRequest();
            }

            return Ok(calendars);
        }   
    }
}