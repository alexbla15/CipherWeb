using CipherData.Models;

namespace CipherData.RequestsInterface
{
    public interface ICategoriesRequests
    {
        /// <summary>
        /// Get all available objects
        /// Path: Get /categories/
        /// </summary>
        Tuple<List<Category>, ErrorResponse> GetCategories();

        /// <summary>
        /// Get details about a single category. 
        /// Path: GET /categories/{id}
        /// </summary>
        Tuple<Category, ErrorResponse> GetCategory(string id);

        /// <summary>
        /// Create a new category.
        /// Path: POST /categories
        /// </summary>
        Tuple<Category, ErrorResponse> CreateCategory(CategoryRequest cat);

        /// <summary>
        /// Update category's details. 
        /// Path: PUT /categories/{id}
        /// </summary>
        Tuple<Category, ErrorResponse> UpdateCategory(string id, CategoryRequest cat);
    }
}
