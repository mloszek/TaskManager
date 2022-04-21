using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Entities;
using TaskManager.Identity;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly InitiativeContext _context;
        private readonly ILogger<AccountController> _logger;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        private const string BadLoginMessage = "Invalid username / password.";

        public AccountController(InitiativeContext context,
            ILogger<AccountController> logger,
            IPasswordHasher<User> passwordHasher,
            IJwtProvider jwtProvider)
        {
            _context = context;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _jwtProvider = jwtProvider;
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
                RoleId = 1
            };

            var passwordHash = _passwordHasher.HashPassword(newUser, registerUserDto.Password);
            newUser.PassHash = passwordHash;

            _context.Users.Add(newUser);
            _context.SaveChanges();

            _logger.LogInformation($"New user added: {newUser.Email}");

            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] UserLoginDto userLoginDto)
        {
            var user = _context.Users
                .Include(user => user.Role)
                .FirstOrDefault(user => user.Email == userLoginDto.Email);

            if (user == null)
            {
                return BadRequest(BadLoginMessage);
            }

            var passwordResult = _passwordHasher.VerifyHashedPassword(user, user.PassHash, userLoginDto.Password);

            if (passwordResult == PasswordVerificationResult.Failed)
            {
                return BadRequest(BadLoginMessage);
            }

             var token = _jwtProvider.GenerateJwtToken(user);
            return Ok(token);
        }
    }
}
