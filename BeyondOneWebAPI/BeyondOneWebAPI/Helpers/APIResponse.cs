
namespace BeyondOneWebAPI
{
    //This class will act as global in API to return results
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