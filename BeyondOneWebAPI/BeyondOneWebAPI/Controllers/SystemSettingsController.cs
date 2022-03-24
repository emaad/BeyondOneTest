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

        [HttpGet(Name = "GetServerTime")]
        public ActionResult Get()
        {
            var response = new APIResponse()
            {
                Data = DateTime.UtcNow.ToString("o"),
                Message = ApiMessages.SuccessMessage
            };

            return Ok(response);
        }
    }
}
