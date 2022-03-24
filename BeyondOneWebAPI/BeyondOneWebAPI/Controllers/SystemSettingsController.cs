using Microsoft.AspNetCore.Mvc;

namespace BeyondOneWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SystemSettingsController : ControllerBase
    {
        private readonly ILogger<SystemSettingsController> _logger;

        public SystemSettingsController(ILogger<SystemSettingsController> logger)
        {
            _logger = logger;
        }

        //returning server time in ISO8601 format
        [HttpGet(Name = "GetServerTime")]
        public ActionResult Get()
        {
            var response = new APIResponse()
            {
                Data = DateTime.UtcNow.ToString("o"),
                Message = ApiMessages.SuccessMessage
            };
            _logger.LogInformation("Returning time in ISO8601 format");
            return Ok(response);
        }
    }
}
