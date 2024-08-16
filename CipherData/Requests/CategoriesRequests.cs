using CipherData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Requests
{
    public class CategoriesRequests
    {
        /// <summary>
        /// Create a new category.
        /// Path: POST /categories
        /// </summary>
        /// <param name="cat"></param>
        /// <returns></returns>
        public Tuple<Category?,ErrorResponse> CreateCategory(CategoryRequest cat)
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 3);
            if (result == 1) 
            {
                return new Tuple<Category?, ErrorResponse>(new Category(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<Category?, ErrorResponse>(null, ErrorResponse.BadRequest);
            }
            else
            {
                return new Tuple<Category?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
        }

        /// <summary>
        /// Get details about a single category. 
        /// Path: GET /categories/{id}
        /// </summary>
        /// <returns></returns>
        public Tuple<Category?, ErrorResponse> GetCategory(string id)
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 3);
            if (result == 1)
            {
                return new Tuple<Category?, ErrorResponse>(new Category(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<Category?, ErrorResponse>(null, ErrorResponse.NotFound);
            }
            else
            {
                return new Tuple<Category?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
        }

        /// <summary>
        /// Update category's details. 
        /// Path: PUT /categories/{id}
        /// </summary>
        /// <returns></returns>
        public Tuple<Category?, ErrorResponse> UpdateCategory(string id, CategoryRequest cat)
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 3);
            if (result == 1)
            {
                return new Tuple<Category?, ErrorResponse>(new Category(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<Category?, ErrorResponse>(null, ErrorResponse.NotFound);
            }
            else if (result == 3)
            {
                return new Tuple<Category?, ErrorResponse>(null, ErrorResponse.BadRequest);
            }
            else
            {
                return new Tuple<Category?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
        }
    }
}
