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
    /// Convenience extensions for setting the HttpMethod.
    /// </summary>
    public static class HttpMethodBuilderExtensions
    {
        private static readonly HttpMethod PatchHttpMethod = new HttpMethod("PATCH");

        /// <summary>
        /// Sets the Http Method to POST.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder Post(this IHttpMethodBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Builder.WithHttpMethod(HttpMethod.Post);
        }

        /// <summary>
        /// Sets the Http Method to GET.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder Get(this IHttpMethodBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Builder.WithHttpMethod(HttpMethod.Get);
        }

        /// <summary>
        /// Sets the Http Method to PUT.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder Put(this IHttpMethodBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Builder.WithHttpMethod(HttpMethod.Put);
        }

        /// <summary>
        /// Sets the Http Method to DELETE.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder Delete(this IHttpMethodBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Builder.WithHttpMethod(HttpMethod.Delete);
        }

        /// <summary>
        /// Sets the Http Method to HEAD.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder Head(this IHttpMethodBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Builder.WithHttpMethod(HttpMethod.Head);
        }

        /// <summary>
        /// Sets the Http Method to OPTIONS.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder Options(this IHttpMethodBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Builder.WithHttpMethod(HttpMethod.Options);
        }

        /// <summary>
        /// Sets the Http Method to TRACE.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder Trace(this IHttpMethodBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Builder.WithHttpMethod(HttpMethod.Trace);
        }

        /// <summary>
        /// Sets the Http Method to PATCH.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder Patch(this IHttpMethodBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Builder.WithHttpMethod(PatchHttpMethod);
        }
    }
}
