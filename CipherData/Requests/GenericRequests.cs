using CipherData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Requests
{
    public class GenericRequests
    {
        /// <summary>
        /// Create a new category.
        /// Path: POST /categories
        /// </summary>
        /// <param name="cat"></param>
        /// <returns></returns>
        public (T? Result, ErrorResponse Error) Request<T>(T successResult, bool canBeNotFound = true)
        {
            Random rand = new();
            int result = rand.Next(1, 4);

            return result switch
            {
                1 => (successResult, ErrorResponse.Success),
                2 => (default(T?), ErrorResponse.BadRequest),
                3 when canBeNotFound => (default(T?), ErrorResponse.NotFound),
                _ => (default(T?), ErrorResponse.Unauthorized)
            };
        }
    }
}
