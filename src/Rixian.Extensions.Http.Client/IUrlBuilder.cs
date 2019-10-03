// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Http.Client
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Builder for creating and manipulating a Url.
    /// </summary>
    public interface IUrlBuilder
    {
        /// <summary>
        /// Gets or sets the scheme part of the url.
        /// </summary>
        string Scheme { get; set; }

        /// <summary>
        /// Gets or sets the port part of the url.
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// Gets or sets the path part of the url.
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// Gets or sets the fragment part of the url.
        /// </summary>
        string Fragment { get; set; }

        /// <summary>
        /// Gets or sets the host part of the url.
        /// </summary>
        string Host { get; set; }

        /// <summary>
        /// Gets the query parameters of the url.
        /// </summary>
        ICollection<KeyValuePair<string, string>> QueryParams { get; }

        /// <summary>
        /// Gets the currently represented Uri.
        /// </summary>
        Uri Uri { get; }
    }
}
