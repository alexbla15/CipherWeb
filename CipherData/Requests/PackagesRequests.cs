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
        /// </summary>
        /// <param name="pack_id"></param>
        /// <returns></returns>
        public static Tuple<Package?,ErrorResponse> GetPackage(string pack_id)
        {
            // an example for each of the three options
            Random rand = new();

            int result = rand.Next(1, 3);
            if (result == 1)
            {
                return new Tuple<Package?, ErrorResponse>(TestedData.RandomPackage(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<Package?, ErrorResponse>(null, ErrorResponse.Unauthorized);
            }
            else
            {
                return new Tuple<Package?, ErrorResponse>(null, ErrorResponse.NotFound);
            }
        }

        /// <summary>
        /// Update comments or caterogy of package
        /// Path: PUT /packages/{id}
        /// </summary>
        /// <param name="pack_id"></param>
        /// <returns></returns>
        public static Tuple<Package?, ErrorResponse> UpdatePackage(string pack_id, UpdatePackage pack)
        {

            // an example for each of the three options

            Random rand = new();
            int result = rand.Next(1, 3);

            if (result == 1)
            {
                return new Tuple<Package?, ErrorResponse>(TestedData.RandomPackage(), ErrorResponse.Success);
            }
            else if (result == 2)
            {
                return new Tuple<Package?, ErrorResponse>(null, ErrorResponse.BadRequest);
            }
            else
            {
                return new Tuple<Package?, ErrorResponse>(null, ErrorResponse.NotFound);
            }
        }
    }
}
