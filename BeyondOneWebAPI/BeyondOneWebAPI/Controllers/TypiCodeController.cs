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
        /// <summary>
        /// This method will get all record from the 3rd party, search the text in description field and return results
        /// </summary>
        /// <returns></returns>
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
                _logger.LogInformation("Returning searched data");//Log message
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());//Log error details
                //return error details
                return BadRequest(new APIResponse()
                {
                    Errors = new string[] { ex.Message },
                    Message = ex.Message
                });
            }
        }
    }
}