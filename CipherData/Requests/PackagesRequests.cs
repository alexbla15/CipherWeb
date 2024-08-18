using CipherData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherData.Requests
{
    public class PackagesRequests
    {
        /// <summary>
        /// Get details about a single package given package ID.
        /// Path: GET /packages/{id}
        public static Tuple<Package?,ErrorResponse> GetPackage(string pack_id)
        {
            return GenericRequests.Request(TestedData.RandomPackage(), canBadRequest:false, canBeNotFound:true);
        }

        /// <summary>
        /// Update comments or caterogy of package
        /// Path: PUT /packages/{id}
        /// </summary>
        public static Tuple<Package?, ErrorResponse> UpdatePackage(string pack_id, UpdatePackage pack)
        {
            return GenericRequests.Request(TestedData.RandomPackage(), canBadRequest: false, canBeNotFound: true);
        }
    }
}
