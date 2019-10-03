// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace System
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Text;
    using System.Web;

    /// <summary>
    /// Reusable extension methods for Uris.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Sets a query string parameter. Replaces all instances of a given query parameter.
        /// </summary>
        /// <param name="uri">The initial Uri.</param>
        /// <param name="name">The name of the query parameter.</param>
        /// <param name="value">The value of the query parameter.</param>
        /// <returns>The updated Uri.</returns>
        public static Uri SetSingleQueryParam(this Uri uri, string name, string value)
        {
            var uriBuilder = new UriBuilder(uri);
            NameValueCollection query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[name] = value;
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }
    }
}
