using Microsoft.AspNetCore.Mvc;
using BeyondOneWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeyondOneWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TypiCodeController : ControllerBase
    {
        private readonly ILogger<TypiCodeController> _logger;
        protected readonly IConfiguration _configuration;
        public TypiCodeController(ILogger<TypiCodeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        //Returning search result
        [HttpGet(Name = "Search")]
        public async Task<ActionResult> Get()
        {
            try
            {
                var searchString = "minima".ToLower();

                HttpClient http = new HttpClient();
                var dataTypiCode = await http.GetFromJsonAsync<IList<TypiCodeModel>>(_configuration.GetValue<string>("TypiCodeURL"));//pulling json data using URL

                var response = new APIResponse()
                {
                    Data = dataTypiCode.Where(x => x.body.ToLower().Contains(searchString)).ToList(),//Searching the string text provided
                    Message = ApiMessages.SuccessMessage
                };
                _logger.LogInformation("Returning searched data");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new APIResponse()
                {
                    Data = null,
                    Errors = new string[] { ex.Message },
                    Message = ex.Message
                });
            }
        }
    }
}