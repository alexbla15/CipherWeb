using CipherData.Models;

namespace CipherData.Requests
{
    public class PackagesRequests
    {
        /// <summary>
        /// Get all available objects
        /// Path: Get /packages/
        /// </summary>
        public static Tuple<List<Package>, ErrorResponse> GetPackages()
        {
            return GenericRequests.Request(TestedData.Packages, canBadRequest: false);
        }

        /// <summary>
        /// Get details about a single package given package ID.
        /// Path: GET /packages/{id}
        public static Tuple<Package,ErrorResponse> GetPackage(string pack_id)
        {
            return GenericRequests.Request(Package.Random(pack_id), canBadRequest:false, canBeNotFound:true);
        }

        /// <summary>
        /// Update comments or caterogy of package
        /// Path: PUT /packages/{id}
        /// </summary>
        public static Tuple<Package, ErrorResponse> UpdatePackage(string pack_id, UpdatePackage pack)
        {
            return GenericRequests.Request(Package.Random(pack_id), canBadRequest: false, canBeNotFound: true);
        }
    }
}
