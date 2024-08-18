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
        public static Tuple<Category?,ErrorResponse> CreateCategory(CategoryRequest cat)
        {
            return GenericRequests.Request(Category.Random());
        }

        /// <summary>
        /// Get details about a single category. 
        /// Path: GET /categories/{id}
        /// </summary>
        public static Tuple<Category?, ErrorResponse> GetCategory(string id)
        {
            return GenericRequests.Request(Category.Random(id), canBadRequest:false, canBeNotFound: true);
        }

        /// <summary>
        /// Update category's details. 
        /// Path: PUT /categories/{id}
        /// </summary>
        public static Tuple<Category?, ErrorResponse> UpdateCategory(string id, CategoryRequest cat)
        {
            return GenericRequests.Request(Category.Random(id), canBeNotFound: true);
        }
    }
}
