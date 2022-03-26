using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManager.Entities;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly InitiativeContext _context;
        private readonly ILogger<AccountController> _logger;

        public AccountController(InitiativeContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
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

            _context.Users.Add(newUser);
            _context.SaveChanges();

            _logger.LogInformation($"New user added: {newUser.Email}");

            return Ok();
        }
    }
}
