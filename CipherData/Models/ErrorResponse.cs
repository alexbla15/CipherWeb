using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Models
{
    public class ErrorResponse
    {
        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Error code
        /// </summary>
        public int Code { get; set; }

        public ErrorResponse(string msg, int code)
        {
            Message = msg;
            Code = code;
        }

        public static ErrorResponse Success = new("OK", 200);
        public static ErrorResponse BadRequest = new("Bad request", 400);
        public static ErrorResponse Unauthorized = new("Unauthorized", 401);
        public static ErrorResponse NotFound = new("Not found", 404);
    }
}
