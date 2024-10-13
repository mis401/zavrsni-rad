using event_driven_backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace event_driven_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class CommunityController : ControllerBase
{
        
    private Context context { get; set; }
        
    private readonly ILogger<CommunityController> _logger;

    public CommunityController(ILogger<CommunityController> logger, Context context)
    {
        _logger = logger;
        this.context = context;
    }

    [HttpGet]
    [Route("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<Community>>> GetAll([FromQuery] int userId)
    {
        Console.WriteLine("entering getall");
        var communities = await context.UserCommunities
            .Where(uc => uc.User.ID == userId)
            .Select(uc => new
            {
                Name = uc.Community.Name,
                ID = uc.Community.ID,
                Creator = uc.Community.Creator,
                CreatedAt = uc.Community.CreatedAt,
                Code = uc.Community.Code,
                Calendar = uc.Community.Calendar
            })
            .ToListAsync();

     
        return Ok(communities);
      
        

    }

    [HttpGet]
    [Route("get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Community>> GetCommunity([FromQuery] int id)
    {
        var community = await context.Communities
            .Select(c => new
            {
                ID = c.ID,
                Name = c.Name,
                Creator = c.Creator,
                CreatedAt = c.CreatedAt,
                Users = c.UserCommunities.Select(uc => uc.User),
                Calendar = c.Calendar
            })
            .FirstOrDefaultAsync(c => c.ID == id);
        if (community == null)
        {
            return BadRequest();
        }
        return Ok(community);
    }

    [HttpPut]
    [Route("join")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> JoinCommunity([FromQuery] int userId, [FromQuery] string communityCode)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.ID == userId);
        var community = await context.Communities.FirstOrDefaultAsync(c => c.Code == communityCode);
        if (user == null)
        {
            return BadRequest("User does not exist");
        }
        if (community == null)
        {
            return BadRequest("Community does not exist");
        }
        var userCommunity = new UserCommunity
        {
            User = user,
            Community = community
        };
        community.UserCommunities.Add(userCommunity);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
        return Ok($"User {user.Name} {user.Surname} joined community {community.Name}");
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("leave")]
    public async Task<ActionResult> LeaveCommunity([FromQuery] int userId, [FromQuery] int communityId)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.ID == userId);
        var community = await context.Communities.FirstOrDefaultAsync(c => c.ID == communityId);
        if (user == null)
        {
            return BadRequest("User does not exist");
        }
        if (community == null)
        {
            return BadRequest("Community does not exist");
        }
        var userCommunity = await context.UserCommunities
            .FirstOrDefaultAsync(uc => uc.User.ID == userId && uc.Community.ID == communityId);
        if (userCommunity == null)
        {
            return BadRequest("User is not a member of this community");
        }
        context.UserCommunities.Remove(userCommunity);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
        return Ok($"User {user.Name} {user.Surname} left community {community.Name}");
    }

    [HttpPost]
    [Route("create")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateCommunity([FromBody] NewCommunityDTO nc)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.ID == nc.userId);
        if (user == null)
        {
            return BadRequest("User does not exist");
        }
        var community = new Community
        {
            Calendar = new Calendar(),
            Name = nc.Name,
            Creator = user,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            Code = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(nc.Name + user.Name))).Substring(0, 6)
        };
        user.UserCommunities.Add(new UserCommunity
        {
            User = user,
            Community = community
        });

        

        try
        {
            await context.SaveChangesAsync();  
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
        return Ok(community);
    }

    [HttpDelete]
    [Route("delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteCommunity([FromQuery] int communityId)
    {
        var community = await context.Communities.FirstOrDefaultAsync(c => c.ID == communityId);
        if (community == null)
        {
            return BadRequest("Community does not exist");
        }
        context.Communities.Remove(community);
        try
        {
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
        return Ok($"Community {community.Name} successfully deleted");
    }
}

