using CipherData.Models;

namespace CipherData.RequestsInterface
{
    public interface ICategoriesRequests
    {
        /// <summary>
        /// Get all available objects
        /// Path: Get /categories/
        /// </summary>
        Tuple<List<ICategory>, ErrorResponse> GetCategories();

        /// <summary>
        /// Get details about a single category. 
        /// Path: GET /categories/{id}
        /// </summary>
        Tuple<ICategory, ErrorResponse> GetCategory(string id);

        /// <summary>
        /// Create a new category.
        /// Path: POST /categories
        /// </summary>
        Tuple<ICategory, ErrorResponse> CreateCategory(ICategoryRequest cat);

        /// <summary>
        /// Update category's details. 
        /// Path: PUT /categories/{id}
        /// </summary>
        Tuple<ICategory, ErrorResponse> UpdateCategory(string id, ICategoryRequest cat);
    }
}
