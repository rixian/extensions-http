// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Microsoft.Extensions.DependencyInjection
{
    using System;

    /// <summary>
    /// Basic exception for configuration errors.
    /// </summary>
    [Serializable]
    public class HttpClientConfigurationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientConfigurationException"/> class.
        /// </summary>
        public HttpClientConfigurationException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientConfigurationException"/> class.
        /// </summary>
        /// <param name="message">The eception message.</param>
        public HttpClientConfigurationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientConfigurationException"/> class.
        /// </summary>
        /// <param name="message">The eception message.</param>
        /// <param name="inner">The inner exception.</param>
        public HttpClientConfigurationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientConfigurationException"/> class.
        /// </summary>
        /// <param name="info">The SerializationInfo.</param>
        /// <param name="context">The StreamingContext.</param>
        protected HttpClientConfigurationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
