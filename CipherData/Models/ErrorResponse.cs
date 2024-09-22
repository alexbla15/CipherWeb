namespace CipherData.Models
{
    public class ErrorResponse
    {
        /// <summary>
        /// Error message
        /// </summary>
        [HebrewTranslation(typeof(ErrorResponse), nameof(Message))]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Error code
        /// </summary>
        [HebrewTranslation(typeof(ErrorResponse), nameof(Code))]
        public int Code { get; set; }

        public static readonly ErrorResponse Success = new() { Message = Translator.TranslationsDictionary["RequestResult_200"], Code = 200 };
        public static readonly ErrorResponse BadRequest = new() { Message = Translator.TranslationsDictionary["RequestResult_400"], Code = 400 };
        public static readonly ErrorResponse Unauthorized = new() { Message = Translator.TranslationsDictionary["RequestResult_401"], Code = 401 };
        public static readonly ErrorResponse NotFound = new() { Message = Translator.TranslationsDictionary["RequestResult_404"], Code = 404 };
    }
}
