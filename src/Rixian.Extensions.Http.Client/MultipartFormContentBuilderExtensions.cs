// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Http.Client
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;

    /// <summary>
    /// Extensions for working with multipart form content.
    /// </summary>
    public static class MultipartFormContentBuilderExtensions
    {
        /// <summary>
        /// Adds stream content to the multipart form content on the request.
        /// </summary>
        /// <param name="builder">The IMultipartFormContentBuilder instance.</param>
        /// <param name="name">The content name.</param>
        /// <param name="stream">The data stream.</param>
        /// <param name="fileName">The file name.</param>
        /// <param name="contentType">The file content type.</param>
        /// <returns>The updated IMultipartFormContentBuilder instance.</returns>
        public static IMultipartFormContentBuilder WithFile(this IMultipartFormContentBuilder builder, string name, Stream stream, string fileName, string contentType)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

#pragma warning disable CA2000 // Dispose objects before losing scope
            var content = new StreamContent(stream);
#pragma warning restore CA2000 // Dispose objects before losing scope

            content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
            builder.Content.Add(content, name, fileName);

            return builder;
        }

        /// <summary>
        /// Adds string content to the multipart form content on the request.
        /// </summary>
        /// <param name="builder">The IMultipartFormContentBuilder instance.</param>
        /// <param name="name">The content name.</param>
        /// <param name="content">The string content.</param>
        /// <returns>The updated IMultipartFormContentBuilder instance.</returns>
        public static IMultipartFormContentBuilder WithString(this IMultipartFormContentBuilder builder, string name, string content)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

#pragma warning disable CA2000 // Dispose objects before losing scope
            var content_ = new StringContent(content);
#pragma warning restore CA2000 // Dispose objects before losing scope

            builder.Content.Add(content_, name);

            return builder;
        }

        /// <summary>
        /// Adds string content to the multipart form content on the request.
        /// </summary>
        /// <param name="builder">The IMultipartFormContentBuilder instance.</param>
        /// <param name="name">The content name.</param>
        /// <param name="content">The string content.</param>
        /// <param name="encoding">The string content encoding.</param>
        /// <returns>The updated IMultipartFormContentBuilder instance.</returns>
        public static IMultipartFormContentBuilder WithString(this IMultipartFormContentBuilder builder, string name, string content, Encoding encoding)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

#pragma warning disable CA2000 // Dispose objects before losing scope
            var content_ = new StringContent(content, encoding);
#pragma warning restore CA2000 // Dispose objects before losing scope

            builder.Content.Add(content_, name);

            return builder;
        }

        /// <summary>
        /// Adds string content to the multipart form content on the request.
        /// </summary>
        /// <param name="builder">The IMultipartFormContentBuilder instance.</param>
        /// <param name="name">The content name.</param>
        /// <param name="content">The string content.</param>
        /// <param name="encoding">The string content encoding.</param>
        /// <param name="mediaType">The string content media type.</param>
        /// <returns>The updated IMultipartFormContentBuilder instance.</returns>
        public static IMultipartFormContentBuilder WithString(this IMultipartFormContentBuilder builder, string name, string content, Encoding encoding, string mediaType)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

#pragma warning disable CA2000 // Dispose objects before losing scope
            var content_ = new StringContent(content, encoding, mediaType);
#pragma warning restore CA2000 // Dispose objects before losing scope

            builder.Content.Add(content_, name);

            return builder;
        }

        /// <summary>
        /// Adds string content with a JSON serialized value.
        /// </summary>
        /// <param name="builder">The IMultipartFormContentBuilder instance.</param>
        /// <param name="name">The content name.</param>
        /// <param name="content">The value to serialize as JSON.</param>
        /// <param name="encoding">The JSON content encoding.</param>
        /// <returns>The updated IMultipartFormContentBuilder instance.</returns>
        public static IMultipartFormContentBuilder WithJsonString(this IMultipartFormContentBuilder builder, string name, object content, Encoding encoding)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);

            return builder.WithString(name, json, encoding, "application/json");
        }

        /// <summary>
        /// Adds string content with a JSON serialized value.
        /// </summary>
        /// <param name="builder">The IMultipartFormContentBuilder instance.</param>
        /// <param name="name">The content name.</param>
        /// <param name="content">The value to serialize as JSON.</param>
        /// <returns>The updated IMultipartFormContentBuilder instance.</returns>
        public static IMultipartFormContentBuilder WithJsonString(this IMultipartFormContentBuilder builder, string name, object content) =>
            builder.WithJsonString(name, content, Encoding.UTF8);

        /// <summary>
        /// Adds byte array content to the multipart form content on the request.
        /// </summary>
        /// <param name="builder">The IMultipartFormContentBuilder instance.</param>
        /// <param name="name">The content name.</param>
        /// <param name="content">The byte array content.</param>
        /// <returns>The updated IMultipartFormContentBuilder instance.</returns>
        public static IMultipartFormContentBuilder WithByteArray(this IMultipartFormContentBuilder builder, string name, byte[] content)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

#pragma warning disable CA2000 // Dispose objects before losing scope
            var content_ = new ByteArrayContent(content);
#pragma warning restore CA2000 // Dispose objects before losing scope

            builder.Content.Add(content_, name);

            return builder;
        }
    }
}
