namespace CipherData.Models
{
    public class ErrorResponse
    {
        /// <summary>
        /// Error message
        /// </summary>
        [HebrewTranslation(Translator.Error_Message)]
        public string Message { get; set; }

        /// <summary>
        /// Error code
        /// </summary>
        [HebrewTranslation(Translator.Error_Code)]
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

        public static readonly ErrorResponse Success = new(Translator.RequestResult_200, 200);
        public static readonly ErrorResponse BadRequest = new(Translator.RequestResult_400, 400);
        public static readonly ErrorResponse Unauthorized = new(Translator.RequestResult_401, 401);
        public static readonly ErrorResponse NotFound = new(Translator.RequestResult_404, 404);
    }
}
