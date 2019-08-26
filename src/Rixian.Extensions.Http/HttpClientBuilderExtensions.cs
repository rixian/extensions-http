// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using System.Globalization;
    using System.Net.Http.Headers;
    using Rixian.Extensions.Http;
    using Rixian.Extensions.Tokens;

    /// <summary>
    /// Provides extension methods for configuring an HttpClient.
    /// </summary>
    public static class HttpClientBuilderExtensions
    {
        /// <summary>
        /// Configures the HttpClient to use the AccessToken retrieved from an ITokenClient in the Authorization header.
        /// </summary>
        /// <param name="httpClientBuilder">The IHttpClintBuilder.</param>
        /// <param name="tokenClientName">The logical name of the ITokenClient.</param>
        /// <returns>The same IHttpClientBuilder.</returns>
        public static IHttpClientBuilder AddTokenClient(this IHttpClientBuilder httpClientBuilder, string tokenClientName)
        {
            return httpClientBuilder.AddHttpMessageHandler(svc =>
            {
                ITokenClientFactory tokenClientFactory = svc.GetRequiredService<ITokenClientFactory>();
                if (tokenClientFactory == null)
                {
                    throw new HttpClientConfigurationException("No ITokenClientFactory registered in the DI container.");
                }

                ITokenClient tokenClient = tokenClientFactory.GetTokenClient(tokenClientName);
                if (tokenClientFactory == null)
                {
                    throw new HttpClientConfigurationException(string.Format(CultureInfo.InvariantCulture, "No ITokenClient registered with the name '{0}'.", tokenClientName));
                }

                var handler = new TokenClientMessageHandler(tokenClient);
                return handler;
            });
        }

        /// <summary>
        /// Configures the HttpClient to use the AccessToken retrieved from an ITokenClient in the Authorization header.
        /// </summary>
        /// <param name="httpClientBuilder">The IHttpClintBuilder.</param>
        /// <param name="getTokenClient">Delegate that pulls the ITokenCLient instance from the DI container.</param>
        /// <returns>The same IHttpClientBuilder.</returns>
        public static IHttpClientBuilder AddTokenClient(this IHttpClientBuilder httpClientBuilder, Func<IServiceProvider, ITokenClient> getTokenClient)
        {
            return httpClientBuilder.AddHttpMessageHandler(svc => new TokenClientMessageHandler(getTokenClient(svc)));
        }

        /// <summary>
        /// Configures the HttpClient to use the AccessToken retrieved from an ITokenClient in the Authorization header.
        /// </summary>
        /// <param name="httpClientBuilder">The IHttpClintBuilder.</param>
        /// <param name="tokenClient">The ITokenClient to use.</param>
        /// <returns>The same IHttpClientBuilder.</returns>
        public static IHttpClientBuilder AddTokenClient(this IHttpClientBuilder httpClientBuilder, ITokenClient tokenClient)
        {
            return httpClientBuilder.AddHttpMessageHandler(() => new TokenClientMessageHandler(tokenClient));
        }

        /// <summary>
        /// Configures the HttpClient to use the bearer token in the Authorization header.
        /// </summary>
        /// <param name="httpClientBuilder">The IHttpClintBuilder.</param>
        /// <param name="bearerToken">The bearer token.</param>
        /// <returns>The same IHttpClientBuilder.</returns>
        public static IHttpClientBuilder AddBearerToken(this IHttpClientBuilder httpClientBuilder, string bearerToken)
        {
            return httpClientBuilder.AddAuthorizationHeader("Bearer", bearerToken);
        }

        /// <summary>
        /// Configures the HttpClient to use a specific Authorization header.
        /// </summary>
        /// <param name="httpClientBuilder">The IHttpClintBuilder.</param>
        /// <param name="scheme">The scheme to use.</param>
        /// <param name="parameter">The auth value to place in the header.</param>
        /// <returns>The same IHttpClientBuilder.</returns>
        public static IHttpClientBuilder AddAuthorizationHeader(this IHttpClientBuilder httpClientBuilder, string scheme, string parameter)
        {
            return httpClientBuilder.ConfigureHttpClient(client => client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, parameter));
        }

        /// <summary>
        /// Configures the HttpClient to use a specific header.
        /// </summary>
        /// <param name="httpClientBuilder">The IHttpClintBuilder.</param>
        /// <param name="name">The name of the header.</param>
        /// <param name="value">The header value.</param>
        /// <returns>The same IHttpClientBuilder.</returns>
        public static IHttpClientBuilder AddHeader(this IHttpClientBuilder httpClientBuilder, string name, string value)
        {
            return httpClientBuilder.ConfigureHttpClient(client => client.DefaultRequestHeaders.TryAddWithoutValidation(name, value));
        }
    }
}
