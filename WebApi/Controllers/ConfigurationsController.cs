using Microsoft.AspNetCore.Mvc;
using Business.Abstracts;
using Entities.Concretes;

[ApiController]
[Route("api/[controller]")]
public class ConfigurationsController : ControllerBase
{
    private readonly IConfigurationService _configService;

    public ConfigurationsController(IConfigurationService configService)
    {
        _configService = configService;
    }

    // GET: api/configurations
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var configs = await _configService.GetAllAsync();
        return Ok(configs);
    }

    // GET: api/configurations/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var config = await _configService.GetByIdAsync(id);
        if (config == null)
            return NotFound();

        return Ok(config);
    }

    // POST: api/configurations
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Entities.Concretes.Configuration config)
    {
        if (config == null)
            return BadRequest();

        await _configService.AddAsync(config);
        return CreatedAtAction(nameof(GetById), new { id = config.Id }, config);
    }

    // PUT: api/configurations/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Entities.Concretes.Configuration config)
    {
        if (config == null || config.Id != id)
            return BadRequest();

        var existing = await _configService.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        await _configService.UpdateAsync(config);
        return Ok(config);
    }

    // DELETE: api/configurations/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _configService.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        await _configService.DeleteAsync(id);
        return NoContent();
    }

    // GET: api/configurations/value?key=SiteName
    [HttpGet("value")]
    public IActionResult GetValue([FromQuery] string key)
    {
        try
        {
            var value = _configService.GetValue<string>(key);
            return Ok(value);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
