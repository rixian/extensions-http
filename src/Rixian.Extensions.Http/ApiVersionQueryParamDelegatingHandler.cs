// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Http
{
    using System;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Configures the Authentication header with the Bearer scheme and uses the AccessToken property of the ITokenClient.
    /// </summary>
    public class ApiVersionQueryParamDelegatingHandler : DelegatingHandler
    {
        private readonly ApiVersionQueryOptions options;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiVersionQueryParamDelegatingHandler"/> class.
        /// </summary>
        /// <param name="options">The options used to configure this handler.</param>
        public ApiVersionQueryParamDelegatingHandler(IOptions<ApiVersionQueryOptions> options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            this.options = options.Value;
        }

        /// <inheritdoc/>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (!string.IsNullOrWhiteSpace(this.options.Value))
            {
                request.RequestUri = request.RequestUri.SetSingleQueryParam(this.options.QueryParamName ?? "api-version", this.options.Value!); // Checked with IsNullOrWhitespace
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
