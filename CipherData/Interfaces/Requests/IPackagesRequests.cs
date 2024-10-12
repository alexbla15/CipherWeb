namespace CipherData.Interfaces
{
    public interface IPackagesRequests
    {
        /// <summary>
        /// Get all available objects
        /// Path: Get /packages/
        /// </summary>
        Task<Tuple<List<IPackage>, ErrorResponse>> GetPackages();

        /// <summary>
        /// Get details about a single package given package ID.
        /// Path: GET /packages/{id}
        Task<Tuple<IPackage, ErrorResponse>> GetPackage(string pack_id);

        /// <summary>
        /// Update comments or caterogy of package
        /// Path: PUT /packages/{id}
        /// </summary>
        Task<Tuple<IPackage, ErrorResponse>> UpdatePackage(string pack_id, IUpdatePackage pack);
    }
}
