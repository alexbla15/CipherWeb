using System;
using System.Collections;
using System.Collections.Generic;
using static CipherData.Models.CreateEvent;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace CipherData.Models
{
    /// <summary>
    /// Update package details contract
    /// </summary>
    public class UpdatePackage
    {
        /// <summary>
        /// New comment on the package
        /// </summary>
        public string? PackageComments { get; set; }

        /// <summary>
        /// Free text comments on update. Ideally contains reason for change
        /// </summary>
        public string? ActionComments { get; set; }

        /// <summary>
        /// Category ID of the package
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Update package details contract
        /// </summary>
        /// <param name="packageComments">New comment on the package</param>
        /// <param name="actionComments">Free text comments on update. Ideally contains reason for change</param>
        /// <param name="categoryId">Category ID of the package</param>
        public UpdatePackage(string? packageComments = null, string? actionComments = null, int? categoryId = null)
        {
            PackageComments = packageComments;
            ActionComments = actionComments;
            CategoryId = categoryId;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            Tuple<bool, string> result = new Tuple<bool, string>(true, string.Empty);

            result = (!string.IsNullOrEmpty(ActionComments)) ? result : Tuple.Create(false, "הערות תנועה"); // action comments is required

            return result;
        }

        /// <summary>
        /// Get an empty update package object scheme.
        /// </summary>
        /// <returns></returns>
        public static UpdatePackage Empty()
        {
            return new UpdatePackage();
        }

        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true, // Pretty print
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Ensure special characters are preserved
                Converters = { new JsonDateTimeConverter() } // Include custom DateTime converter
            };

            return JsonSerializer.Serialize(this, options);
        }
    }
}
