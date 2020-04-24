// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Http
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Rixian.Extensions.Errors;
    using Rixian.Extensions.Tokens;

    /// <summary>
    /// Configures the Authentication header with the Bearer scheme and uses the AccessToken property of the ITokenClient.
    /// </summary>
    public class TokenClientDelegatingHandler : DelegatingHandler
    {
        private readonly ITokenClient tokenClient;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenClientDelegatingHandler"/> class.
        /// </summary>
        /// <param name="tokenClient">The ITokenClient to use.</param>
        /// <param name="logger">The ILogger instance.</param>
        public TokenClientDelegatingHandler(ITokenClient tokenClient, ILogger logger)
        {
            this.tokenClient = tokenClient;
            this.logger = logger;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenClientDelegatingHandler"/> class.
        /// </summary>
        /// <param name="tokenClient">The ITokenClient to use.</param>
        /// <param name="logger">The ILogger instance.</param>
        public TokenClientDelegatingHandler(Result<ITokenClient> tokenClient, ILogger logger)
        {
            this.tokenClient = tokenClient.Value;
            this.logger = logger;
        }

        /// <inheritdoc/>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            Result<ITokenInfo> token = await this.tokenClient.GetTokenAsync().ConfigureAwait(false);
            if (token.IsSuccess)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.AccessToken);
            }
            else
            {
                this.logger.LogError("Failed to retreive token.\nCode: {Code}\nTarget: {Target}\nMessage: {Message}\nDetails: {Details}", token.Error.Code, token.Error.Target, token.Error.Message, token.Error.Details);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
