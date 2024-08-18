﻿using System;
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

        public static readonly ErrorResponse Success = new("OK", 200);
        public static readonly ErrorResponse BadRequest = new("Bad request", 400);
        public static readonly ErrorResponse Unauthorized = new("Unauthorized", 401);
        public static readonly ErrorResponse NotFound = new("Not found", 404);
    }
}
