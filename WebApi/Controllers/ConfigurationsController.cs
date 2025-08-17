using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ConfigurationsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet("{key}")]
        public IActionResult GetConfigValue(string key)
        {
            try
            {
                var value = _configuration.GetValue<string>(key);
                return Ok(value);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidCastException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
