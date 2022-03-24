namespace BeyondOneWebAPI.Models
{
    /// <summary>
    /// This class will be used to convert json string into object parameters.
    /// </summary>
    public class TypiCodeModel
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }
}