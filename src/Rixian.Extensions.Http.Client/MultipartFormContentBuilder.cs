// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Http.Client
{
    using System.Net.Http;

    /// <summary>
    /// Builder for creating and manipulating MultipartFormContent.
    /// </summary>
    internal class MultipartFormContentBuilder : IMultipartFormContentBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultipartFormContentBuilder"/> class.
        /// </summary>
        /// <param name="httpRequestMessageBuilder">The IHttpRequestMessageBuilder.</param>
        public MultipartFormContentBuilder(IHttpRequestMessageBuilder httpRequestMessageBuilder)
        {
            this.Content = new MultipartFormDataContent();
            this.RequestBuilder = httpRequestMessageBuilder;
        }

        /// <inheritdoc/>
        public MultipartFormDataContent Content { get; }

        /// <inheritdoc/>
        public IHttpRequestMessageBuilder RequestBuilder { get; }
    }
}
