using CipherData.Models;

namespace CipherData.RequestsInterface
{
    public interface IPackagesRequests
    {
        /// <summary>
        /// Get all available objects
        /// Path: Get /packages/
        /// </summary>
        Tuple<List<Package>, ErrorResponse> GetPackages();

        /// <summary>
        /// Get details about a single package given package ID.
        /// Path: GET /packages/{id}
        Tuple<Package, ErrorResponse> GetPackage(string pack_id);

        /// <summary>
        /// Update comments or caterogy of package
        /// Path: PUT /packages/{id}
        /// </summary>
        Tuple<Package, ErrorResponse> UpdatePackage(string pack_id, UpdatePackage pack);
    }
}
