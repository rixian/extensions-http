// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Http.Client
{
    using System.Net.Http;

    /// <summary>
    /// Builder for creating and manipulating MultipartFormDataContent.
    /// </summary>
    public interface IMultipartFormContentBuilder
    {
        /// <summary>
        /// Gets the current MultipartFormDataContent.
        /// </summary>
        MultipartFormDataContent Content { get; }

        /// <summary>
        /// Gets the current request builder.
        /// </summary>
        IHttpRequestMessageBuilder RequestBuilder { get; }
    }
}
