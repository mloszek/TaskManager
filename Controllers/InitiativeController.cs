using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Entities;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/initiative")]
    [ApiController]
    [Authorize]
    public class InitiativeController : ControllerBase
    {
        private readonly InitiativeContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<InitiativeController> _logger;

        public InitiativeController(InitiativeContext context, IMapper mapper, ILogger<InitiativeController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Policy = "HasNationality")]
        public ActionResult<List<Initiative>> Get()
        {
            var initiatives = _context.Initiatives.Include(i => i.Epics);
            var initiativesDto = _mapper.Map<List<InitiativeDto>>(initiatives);

            return Ok(initiativesDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Post([FromBody]InitiativeDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var initiative = new Initiative
            {
                Name = model.Name,
                Epics = model.Epics
            };
            _context.Initiatives.Add(initiative);
            _context.SaveChanges();

            var key = initiative.Name.Replace(" ", "-").ToLower();
            return Created("api/initiative/" + key, null);
        }
    }
}
