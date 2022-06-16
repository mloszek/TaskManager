using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace TaskManager.Controllers
{
    [Route("config")]
    [Authorize(Roles = "Admin")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ConfigController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpOptions("reload")]
        public ActionResult Reload()
        {
            try
            {
                ((IConfigurationRoot)_configuration).Reload();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
