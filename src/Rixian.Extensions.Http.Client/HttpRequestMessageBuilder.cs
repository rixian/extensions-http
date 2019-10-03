// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Http.Client
{
    using System;
    using System.Net.Http;

    /// <summary>
    /// Entry point for creating an IHttpRequestMessageBuilder.
    /// </summary>
    public static class HttpRequestMessageBuilder
    {
        /// <summary>
        /// Creates a new IHttpRequestMessageBuilder.
        /// </summary>
        /// <returns>The IHttpRequestMessageBuilder.</returns>
        public static IHttpRequestMessageBuilder Create()
        {
            return new InternalHttpRequestMessageBuilder();
        }

        /// <summary>
        /// Creates a new IHttpRequestMessageBuilder.
        /// </summary>
        /// <param name="uri">The initial uri.</param>
        /// <returns>The IHttpRequestMessageBuilder.</returns>
        public static IHttpRequestMessageBuilder Create(Uri uri)
        {
            return new InternalHttpRequestMessageBuilder(uri);
        }

        /// <summary>
        /// Creates a new IHttpRequestMessageBuilder.
        /// </summary>
        /// <param name="request">The initial HttpRequestMessage.</param>
        /// <returns>The IHttpRequestMessageBuilder.</returns>
        public static IHttpRequestMessageBuilder Create(HttpRequestMessage request)
        {
            return new InternalHttpRequestMessageBuilder(request);
        }

        private class InternalHttpRequestMessageBuilder : IHttpRequestMessageBuilder
        {
            internal InternalHttpRequestMessageBuilder()
            {
                this.Request = new HttpRequestMessage();
            }

            internal InternalHttpRequestMessageBuilder(Uri requestUri)
            {
                this.Request = new HttpRequestMessage
                {
                    RequestUri = requestUri,
                };
            }

            internal InternalHttpRequestMessageBuilder(HttpRequestMessage request)
            {
                this.Request = request;
            }

            public HttpRequestMessage Request { get; }
        }
    }
}
