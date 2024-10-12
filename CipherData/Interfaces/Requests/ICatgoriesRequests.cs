namespace CipherData.Interfaces
{
    public interface ICategoriesRequests
    {
        /// <summary>
        /// Get all available objects
        /// Path: Get /categories/
        /// </summary>
        Task<Tuple<List<ICategory>, ErrorResponse>> GetCategories();

        /// <summary>
        /// Get details about a single category. 
        /// Path: GET /categories/{id}
        /// </summary>
        Task<Tuple<ICategory, ErrorResponse>> GetCategory(string id);

        /// <summary>
        /// Create a new category.
        /// Path: POST /categories
        /// </summary>
        Task<Tuple<ICategory, ErrorResponse>> CreateCategory(ICategoryRequest cat);

        /// <summary>
        /// Update category's details. 
        /// Path: PUT /categories/{id}
        /// </summary>
        Task<Tuple<ICategory, ErrorResponse>> UpdateCategory(string id, ICategoryRequest cat);
    }
}
