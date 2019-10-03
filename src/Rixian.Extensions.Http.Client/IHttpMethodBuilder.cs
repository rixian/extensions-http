// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Http.Client
{
    /// <summary>
    /// Builder for adding HttpMethods to an IHttpRequestMessageBuilder.
    /// </summary>
    public interface IHttpMethodBuilder
    {
        /// <summary>
        /// Gets the IHttpRequestMessageBuilder.
        /// </summary>
        IHttpRequestMessageBuilder Builder { get; }
    }
}
