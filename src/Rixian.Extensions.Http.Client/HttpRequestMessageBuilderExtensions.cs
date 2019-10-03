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
    /// Extensions for building HttpRequestMessages.
    /// </summary>
    public static class HttpRequestMessageBuilderExtensions
    {
        private const string ApplicationJsonContentType = "application/json";

        private static readonly Lazy<MediaTypeWithQualityHeaderValue> ApplicationOctetHeader = new Lazy<MediaTypeWithQualityHeaderValue>(() =>
            MediaTypeWithQualityHeaderValue.Parse(MediaTypeNames.Application.Octet));

        private static readonly Lazy<MediaTypeWithQualityHeaderValue> ApplicationJsonHeader = new Lazy<MediaTypeWithQualityHeaderValue>(() =>
            MediaTypeWithQualityHeaderValue.Parse(ApplicationJsonContentType));

        private static readonly Lazy<MediaTypeWithQualityHeaderValue> TextXmlHeader = new Lazy<MediaTypeWithQualityHeaderValue>(() =>
            MediaTypeWithQualityHeaderValue.Parse(MediaTypeNames.Text.Xml));

        private static readonly Lazy<MediaTypeWithQualityHeaderValue> TextPlainHeader = new Lazy<MediaTypeWithQualityHeaderValue>(() =>
            MediaTypeWithQualityHeaderValue.Parse(MediaTypeNames.Text.Plain));

        /// <summary>
        /// Sets the HttpMethod on the request.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <param name="httpMethod">The http method to use.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder WithHttpMethod(this IHttpRequestMessageBuilder builder, HttpMethod httpMethod)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (httpMethod is null)
            {
                throw new ArgumentNullException(nameof(httpMethod));
            }

            builder.Request.Method = httpMethod;

            return builder;
        }

        /// <summary>
        /// Creates a builder for HttpMethods.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpMethodBuilder WithHttpMethod(this IHttpRequestMessageBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return new HttpMethodBuilder(builder);
        }

        /// <summary>
        /// Sets the specified header on the request.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <param name="name">The header name.</param>
        /// <param name="value">The header value.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder WithHeader(this IHttpRequestMessageBuilder builder, string name, string value)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(Properties.Resources.ParameterStringEmptyError, nameof(name));
            }

            builder.Request.Headers.Add(name, value);

            return builder;
        }

        /// <summary>
        /// Sets the Accept header with the specified value.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <param name="value">The header value.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder WithAcceptHeader(this IHttpRequestMessageBuilder builder, MediaTypeWithQualityHeaderValue value)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Request.Headers.Accept.Add(value);

            return builder;
        }

        /// <summary>
        /// Sets the Accept header with the specified value.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <param name="value">The header value.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder WithAcceptHeader(this IHttpRequestMessageBuilder builder, string value) =>
            builder.WithAcceptHeader(MediaTypeWithQualityHeaderValue.Parse(value));

        /// <summary>
        /// Sets the Accept header with the value 'application/octet-stream'.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder WithAcceptApplicationOctet(this IHttpRequestMessageBuilder builder) =>
            builder.WithAcceptHeader(ApplicationOctetHeader.Value);

        /// <summary>
        /// Sets the Accept header with the value 'application/json'.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder WithAcceptApplicationJson(this IHttpRequestMessageBuilder builder) =>
            builder.WithAcceptHeader(ApplicationJsonHeader.Value);

        /// <summary>
        /// Sets the Accept header with the value 'text/xml'.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder WithAcceptTextXml(this IHttpRequestMessageBuilder builder) =>
            builder.WithAcceptHeader(TextXmlHeader.Value);

        /// <summary>
        /// Sets the Accept header with the value 'text/plain'.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder WithAcceptTextPlain(this IHttpRequestMessageBuilder builder) =>
            builder.WithAcceptHeader(TextPlainHeader.Value);

        /// <summary>
        /// Sets the Authorization header.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <param name="scheme">The authorization scheme.</param>
        /// <param name="parameter">The authorization parameter.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder WithAuthorization(this IHttpRequestMessageBuilder builder, string scheme, string parameter)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Request.Headers.Authorization = new AuthenticationHeaderValue(scheme, parameter);

            return builder;
        }

        /// <summary>
        /// Sets the Authorization header with a scheme of 'Bearer'.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <param name="token">The bearer token to use in the authorization parameter.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder WithAuthorizationBearer(this IHttpRequestMessageBuilder builder, string token) =>
            builder.WithAuthorization("Bearer", token);

        /// <summary>
        /// Sets the request content.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <param name="content">The request content.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder WithContent(this IHttpRequestMessageBuilder builder, HttpContent content)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Request.Content = content;

            return builder;
        }

        /// <summary>
        /// Sets the request content with a JSON serialized value.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <param name="body">The value to JSON serialize.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder WithContentJson(this IHttpRequestMessageBuilder builder, object body)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(body);
            builder.WithContent(new StringContent(json, Encoding.UTF8, ApplicationJsonContentType));

            return builder;
        }

        /// <summary>
        /// Creates a builder for craeting multipart form content.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <returns>The IMultipartFormContentBuilder instance.</returns>
        public static IMultipartFormContentBuilder WithMultipartFormContent(this IHttpRequestMessageBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var contentBuilder = new MultipartFormContentBuilder(builder);
            builder.WithContent(contentBuilder.Content);
            return contentBuilder;
        }

        /// <summary>
        /// Sets the request content with a form url encoded value.
        /// </summary>
        /// <param name="builder">The IHttpRequestBuilder instance.</param>
        /// <param name="content">The values for form url encode.</param>
        /// <returns>The updated IHttpRequestBuilder instance.</returns>
        public static IHttpRequestMessageBuilder WithFormUrlEncodedContent(this IHttpRequestMessageBuilder builder, IEnumerable<KeyValuePair<string, string>> content)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.WithContent(new FormUrlEncodedContent(content));

            return builder;
        }
    }
}
