﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
