// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Http.Client
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using Rixian.Extensions.Errors;
    using static Rixian.Extensions.Errors.Prelude;

    /// <summary>
    /// Represents a file response from an http call.
    /// </summary>
    public class HttpFileResponse : IDisposable
    {
        private readonly IDisposable responseMessage;
        private bool disposedValue; // To detect redundant calls

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpFileResponse"/> class.
        /// </summary>
        /// <param name="statusCode">The status code of the response.</param>
        /// <param name="headers">The response headers.</param>
        /// <param name="stream">The response data stream.</param>
        /// <param name="responseMessage">Handle to the response object.</param>
        private HttpFileResponse(HttpStatusCode statusCode, IReadOnlyDictionary<string, IEnumerable<string>> headers, Stream stream, IDisposable responseMessage)
        {
            this.StatusCode = statusCode;
            this.Headers = headers;
            this.Stream = stream;
            this.responseMessage = responseMessage;

            var cdHeader = headers?["Content-Disposition"]?.FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(cdHeader))
            {
                this.ContentDispositionHeader = new ContentDisposition(cdHeader);
            }

            var ctHeader = headers?["Content-Type"]?.FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(ctHeader))
            {
                this.ContentTypeHeader = new ContentType(ctHeader);
            }
        }

        /// <summary>
        /// Gets the response status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Gets the response headers.
        /// </summary>
        public IReadOnlyDictionary<string, IEnumerable<string>> Headers { get; private set; }

        /// <summary>
        /// Gets the raw data stream.
        /// </summary>
        public Stream Stream { get; private set; }

        /// <summary>
        /// Gets the Content-Type header.
        /// </summary>
        public ContentType? ContentTypeHeader { get; private set; }

        /// <summary>
        /// Gets the Content-Disposition header.
        /// </summary>
        public ContentDisposition? ContentDispositionHeader { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the response contains the request data range.
        /// </summary>
        public bool IsPartial
        {
            get { return this.StatusCode == HttpStatusCode.PartialContent; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpFileResponse"/> class.
        /// </summary>
        /// <param name="statusCode">The status code of the response.</param>
        /// <param name="headers">The response headers.</param>
        /// <param name="stream">The response data stream.</param>
        /// <param name="responseMessage">Handle to the response object.</param>
        /// <returns>The HttpFileReponse.</returns>.
        public static Result<HttpFileResponse> Create(HttpStatusCode statusCode, IReadOnlyDictionary<string, IEnumerable<string>> headers, Stream stream, IDisposable responseMessage)
        {
            return new HttpFileResponse(statusCode, headers, stream, responseMessage);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpFileResponse"/> class.
        /// </summary>
        /// <param name="response">The response message containing the file response.</param>
        /// <returns>The HttpFileReponse.</returns>.
        public static async Task<Result<HttpFileResponse>> CreateAsync(HttpResponseMessage response)
        {
            if (response is null)
            {
                return NullArgumentDisallowedError(Properties.Resources.ParameterStringNullError, nameof(response)).ToResult<HttpFileResponse>();
            }

            Stream responseStream = response.Content == null ? Stream.Null : await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
            if (response.Content != null && response.Content.Headers != null)
            {
                foreach (KeyValuePair<string, IEnumerable<string>> item_ in response.Content.Headers)
                {
                    headers[item_.Key] = item_.Value;
                }
            }

            return new HttpFileResponse(response.StatusCode, headers, responseStream, response);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes all class resources.
        /// </summary>
        /// <param name="disposing">Value indicating whether the object is disposing or not.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    if (this.Stream != null)
                    {
                        this.Stream.Dispose();
                    }

                    if (this.responseMessage != null)
                    {
                        this.responseMessage.Dispose();
                    }
                }

                this.disposedValue = true;
            }
        }
    }
}
