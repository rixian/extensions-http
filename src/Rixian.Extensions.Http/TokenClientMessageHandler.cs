// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Http
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Rixian.Extensions.Tokens;

    /// <summary>
    /// Configures the Authentication header with the Bearer scheme and uses the AccessToken property of the ITokenClient.
    /// </summary>
    internal class TokenClientMessageHandler : DelegatingHandler
    {
        private readonly ITokenClient tokenClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenClientMessageHandler"/> class.
        /// </summary>
        /// <param name="tokenClient">The ITokenClient to use.</param>
        public TokenClientMessageHandler(ITokenClient tokenClient)
        {
            this.tokenClient = tokenClient;
        }

        /// <inheritdoc/>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ITokenInfo token = await this.tokenClient.GetTokenAsync().ConfigureAwait(false);
            if (token != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
