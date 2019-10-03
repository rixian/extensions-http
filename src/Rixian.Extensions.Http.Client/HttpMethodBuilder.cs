// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Http.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Mime;
    using System.Text;

    /// <summary>
    /// Builder for adding HttpMethods to an IHttpRequestMessageBuilder.
    /// </summary>
    internal class HttpMethodBuilder : IHttpMethodBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpMethodBuilder"/> class.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        public HttpMethodBuilder(IHttpRequestMessageBuilder builder)
        {
            this.Builder = builder;
        }

        /// <inheritdoc/>
        public IHttpRequestMessageBuilder Builder { get; }
    }
}
