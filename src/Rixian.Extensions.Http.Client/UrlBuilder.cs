// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Http.Client
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Entry point for creating an IUrlBuilder.
    /// </summary>
    public static class UrlBuilder
    {
        /// <summary>
        /// Creates a new IUrlBuilder.
        /// </summary>
        /// <returns>A new IUrlBuilder.</returns>
        public static IUrlBuilder Create()
        {
            return new InternalUrlBuilder();
        }

        /// <summary>
        /// Creates a new IUrlBuilder.
        /// </summary>
        /// <param name="url">The initial url.</param>
        /// <returns>A new IUrlBuilder.</returns>
#pragma warning disable CA1054 // Uri parameters should not be strings
        public static IUrlBuilder Create(string url)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            return new InternalUrlBuilder(url);
        }

        private class InternalUrlBuilder : IUrlBuilder
        {
            public InternalUrlBuilder()
            {
                this.Path = "/";
            }

            public InternalUrlBuilder(string path)
            {
                this.Path = path;
            }

            /// <inheritdoc/>
            public ICollection<KeyValuePair<string, string>> QueryParams { get; } = new List<KeyValuePair<string, string>>();

            /// <inheritdoc/>
            public string Path { get; set; }

            /// <inheritdoc/>
            public string Fragment { get; set; }

            /// <inheritdoc/>
            public Uri Uri
            {
                get
                {
                    var url = $"{this.Path}{this.ToQueryString()}";
                    if (!string.IsNullOrWhiteSpace(this.Fragment))
                    {
                        url = $"{url}#{this.Fragment}";
                    }

                    return new Uri(url, UriKind.RelativeOrAbsolute);
                }
            }
        }
    }
}
