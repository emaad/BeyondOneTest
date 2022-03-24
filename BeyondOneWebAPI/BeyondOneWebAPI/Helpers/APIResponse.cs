using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeyondOneWebAPI
{
    public class APIResponse
    {
        /// <summary>
        /// API response object.
        /// </summary>
        public dynamic Data { get; set; }
        /// <summary>
        /// Verbose repose of API call status.
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Details of errors such as validation errors.
        /// </summary>
        public string[] Errors { get; set; }
    }
}