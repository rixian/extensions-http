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
            private readonly UriBuilder uriBuilder;

            public InternalUrlBuilder()
            {
                this.uriBuilder = new UriBuilder();
            }

            public InternalUrlBuilder(string url)
            {
                this.uriBuilder = new UriBuilder(url);
            }

            /// <inheritdoc/>
            public ICollection<KeyValuePair<string, string>> QueryParams { get; } = new List<KeyValuePair<string, string>>();

            /// <inheritdoc/>
            public string Scheme { get => this.uriBuilder.Scheme; set => this.uriBuilder.Scheme = value; }

            /// <inheritdoc/>
            public int Port { get => this.uriBuilder.Port; set => this.uriBuilder.Port = value; }

            /// <inheritdoc/>
            public string Path { get => this.uriBuilder.Path; set => this.uriBuilder.Path = value; }

            /// <inheritdoc/>
            public string Fragment { get => this.uriBuilder.Fragment; set => this.uriBuilder.Fragment = value; }

            /// <inheritdoc/>
            public string Host { get => this.uriBuilder.Host; set => this.uriBuilder.Host = value; }

            /// <inheritdoc/>
            public Uri Uri
            {
                get
                {
                    this.uriBuilder.Query = this.ToQueryString();
                    return this.uriBuilder.Uri;
                }
            }
        }
    }
}
