using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Entities;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/initiative")]
    [ApiController]
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
        public ActionResult<List<Initiative>> Get()
        {
            var initiatives = _context.Initiatives.Include(i => i.Epics);
            var initiativesDto = _mapper.Map<List<InitiativeDto>>(initiatives);

            _logger.LogInformation("log test");

            return Ok(initiativesDto);
        }
    }
}
