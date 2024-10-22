namespace CipherData.Interfaces
{
    public interface IResourceRequests<TResourceInterface, TCreateRequest, TUpdateRequest>
        where TResourceInterface : IResource
        where TCreateRequest : ICipherClass
        where TUpdateRequest : ICipherClass
    {
        /// <summary>
        /// Get all available objects.
        /// Path: GET /{entity}/
        /// </summary>
        Task<Tuple<List<TResourceInterface>, ErrorResponse>> GetAll();

        /// <summary>
        /// Get details about a single object by ID.
        /// Path: GET /{entity}/{id}
        /// </summary>
        Task<Tuple<TResourceInterface, ErrorResponse>> GetById(string? id);

        /// <summary>
        /// Create a new object.
        /// Path: POST /{entity}
        /// </summary>
        Task<Tuple<TResourceInterface, ErrorResponse>> Create(TCreateRequest request);

        /// <summary>
        /// Update object's details.
        /// Path: PUT /{entity}/{id}
        /// </summary>
        Task<Tuple<TResourceInterface, ErrorResponse>> Update(string? id, TUpdateRequest request);
    }
    
    public interface IResourceRequests<TResourceInterface, TRequest> : 
        IResourceRequests<TResourceInterface, TRequest, TRequest>
        where TResourceInterface : IResource
        where TRequest : ICipherClass
    {
    }
}
