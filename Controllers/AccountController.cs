using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManager.Entities;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly InitiativeContext _context;
        private readonly ILogger<AccountController> _logger;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountController(InitiativeContext context, ILogger<AccountController> logger, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = new User()
            {
                Email = registerUserDto.Email,
                Nationality = registerUserDto.Nationality,
                DateOfBirth = registerUserDto.DateOfBirth,
                RoleId = registerUserDto.RoleID
            };

            var passwordHash = _passwordHasher.HashPassword(newUser, registerUserDto.Password);
            newUser.PassHash = passwordHash;

            _context.Users.Add(newUser);
            _context.SaveChanges();

            _logger.LogInformation($"New user added: {newUser.Email}");

            return Ok();
        }
    }
}
