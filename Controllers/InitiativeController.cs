using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Authorization;
using TaskManager.Controllers.Filters;
using TaskManager.Entities;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/initiative")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(TimeTrackFilter))]
    public class InitiativeController : ControllerBase
    {
        private readonly InitiativeContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<InitiativeController> _logger;
        private readonly IAuthorizationService _authorizationService;

        public InitiativeController(InitiativeContext context, IMapper mapper, ILogger<InitiativeController> logger, IAuthorizationService authorizationService)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [Authorize(Policy = "HasNationality")]
        [Authorize(Policy = "AtLeast18")]
        [NationalityFilter("Chosen")]
        public ActionResult<List<Initiative>> Get()
        {
            var initiatives = _context.Initiatives.Include(i => i.Epics);
            var initiativesDto = _mapper.Map<List<InitiativeDto>>(initiatives);

            return Ok(initiativesDto);
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Post([FromBody] InitiativeDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var initiative = new Initiative
            {
                Name = model.Name,
                Epics = model.Epics,
                CreatedById = int.Parse(userId)
            };
            _context.Initiatives.Add(initiative);
            _context.SaveChanges();

            var key = initiative.Name.Replace(" ", "-").ToLower();
            return Created("api/initiative/" + key, null);
        }

        [HttpPut("{name}")]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Put(string name, [FromBody] InitiativeDto model)
        {
            var initiative = _context.Initiatives.FirstOrDefault(m => m.Name.Replace(" ", "-").ToLower() == name.ToLower());

            if (initiative == null)
            {
                return NotFound();
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(User, initiative, new ResourceOperationRequirement(OperationType.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            initiative.Name = model.Name;
            initiative.Epics = model.Epics;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{name}")]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Delete(string name)
        {
            var initiative = _context.Initiatives.FirstOrDefault(m => m.Name.Replace(" ", "-").ToLower() == name.ToLower());

            if (initiative == null)
            {
                return NotFound();
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(User, initiative, new ResourceOperationRequirement(OperationType.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            _context.Remove(initiative);
            _context.SaveChanges();

            return NoContent();
        }

    }
}
