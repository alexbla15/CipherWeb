namespace CipherData.Models
{
    public class ErrorResponse
    {
        /// <summary>
        /// Error message
        /// </summary>
        [HebrewTranslation(typeof(ErrorResponse), nameof(Message))]
        public string Message { get; set; }

        /// <summary>
        /// Error code
        /// </summary>
        [HebrewTranslation(typeof(ErrorResponse), nameof(Code))]
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

        public static readonly ErrorResponse Success = new(Translator.TranslationsDictionary["RequestResult_200"], 200);
        public static readonly ErrorResponse BadRequest = new(Translator.TranslationsDictionary["RequestResult_400"], 400);
        public static readonly ErrorResponse Unauthorized = new(Translator.TranslationsDictionary["RequestResult_401"], 401);
        public static readonly ErrorResponse NotFound = new(Translator.TranslationsDictionary["RequestResult_404"], 404);
    }
}
