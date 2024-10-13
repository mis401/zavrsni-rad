using event_driven_backend.DTOs;
using Isopoh.Cryptography.Argon2;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace event_driven_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private Context _context;

        public UserController(ILogger<UserController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        public UserController()
        {
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);
            if (user == null)
            {
                return NotFound("Email not found");
            }

            var correct = Argon2.Verify(user.Password, login.Password, null);
            if (!correct)
            {
                return Unauthorized("Wrong password");
            }
            return Ok(user);
        }


        [HttpGet("get/{id}")]
        public async Task<ActionResult> GetUser(int id)
        {
            var user = await _context.Users
                .Where(user => user.ID == id)
                .Include(u => u.UserCommunities)
                .Include(u => u.CreatedCommunities)
                .Select(u => new
                {
                    Name = u.Name,
                    Surname = u.Surname,
                    Email = u.Email,
                    CreatedCommunities = u.CreatedCommunities.Select(c => new
                    {
                        ID = c.ID,
                        Name = c.Name,
                    }),
                    UserCommunities = u.UserCommunities.Select(uc => new
                    {
                        ID = uc.Community.ID,
                        Name = uc.Community.Name,
                    })
                })
                .FirstOrDefaultAsync();
            
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("getbyemail/{email}")]
        public async Task<ActionResult> GetUser(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> CreateUser([FromBody] NewUserDTO newUser)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == newUser.Email);
            if (existingUser != null)
            {
                Console.WriteLine("Postojeci korisnik");
                return StatusCode(409, $"User with email {newUser.Email} already exists");
            }
            string hash = Argon2.Hash(newUser.Password);
            var user = new User { Email = newUser.Email, Name = newUser.Name, Surname = newUser.Surname, Password = hash};
            Console.WriteLine(hash);
            _context.Users.Add(user);
            Console.WriteLine("Kreirao sam korisnika");
            try
            {
                await _context.SaveChangesAsync();
                Console.WriteLine("Sacuvao sam korisnika");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok(user);
        }

        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteUser([FromQuery] int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("User does not exist");
            }
            _context.Users.Remove(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok($"User {user.Name} {user.Surname} has been deleted");
        }

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateUser([FromQuery] int userId, [FromBody] NewUserDTO nu)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return BadRequest("User does not exist");
            }
            user.Email = nu.Email;
            user.Name = nu.Name;
            user.Surname = nu.Surname;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
            return Ok(user);
        }

    }
}
