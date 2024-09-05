namespace CipherData.Models
{
    public class ErrorResponse
    {
        /// <summary>
        /// Error message
        /// </summary>
        [HebrewTranslation("Error.Message")]
        public string Message { get; set; }

        /// <summary>
        /// Error code
        /// </summary>
        [HebrewTranslation("Error.Code")]
        public int Code { get; set; }

        /// <summary>
        /// Response of API request
        /// </summary>
        /// <param name="msg">Error message</param>
        /// <param name="code">Error code</param>
        public ErrorResponse(string msg, int code)
        {
            Message = msg;
            Code = code;
        }

        public static readonly ErrorResponse Success = new("Result.200", 200);
        public static readonly ErrorResponse BadRequest = new("Result.400", 400);
        public static readonly ErrorResponse Unauthorized = new("Result.401", 401);
        public static readonly ErrorResponse NotFound = new("Result.404", 404);
    }
}
