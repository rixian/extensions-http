// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Microsoft.Extensions.DependencyInjection
{
    using System;
    using System.Net.Http.Headers;
    using Rixian.Extensions.Http;
    using Rixian.Extensions.Tokens;

    /// <summary>
    /// Provides extension methods for
    /// </summary>
    public static class HttpClientBuilderExtensions
    {
        public static IHttpClientBuilder AddTokenClient(this IHttpClientBuilder httpClientBuilder, string tokenClientName)
        {
            return httpClientBuilder.AddHttpMessageHandler(svc =>
            {
                ITokenClientFactory tokenClientFactory = svc.GetRequiredService<ITokenClientFactory>();
                //if (tokenClientFactory == null)
                ITokenClient tokenClient = tokenClientFactory.GetTokenClient(tokenClientName);
                var handler = new TokenClientMessageHandler(tokenClient);
                return handler;
            });
        }

        public static IHttpClientBuilder AddTokenClient(this IHttpClientBuilder httpClientBuilder, Func<IServiceProvider, ITokenClient> getTokenClient)
        {
            return httpClientBuilder.AddHttpMessageHandler(svc => new TokenClientMessageHandler(getTokenClient(svc)));
        }

        public static IHttpClientBuilder AddTokenClient(this IHttpClientBuilder httpClientBuilder, ITokenClient tokenClient)
        {
            return httpClientBuilder.AddHttpMessageHandler(() => new TokenClientMessageHandler(tokenClient));
        }

        public static IHttpClientBuilder AddBearerToken(this IHttpClientBuilder httpClientBuilder, string bearerToken)
        {
            return httpClientBuilder.AddToken("Bearer", bearerToken);
        }

        public static IHttpClientBuilder AddToken(this IHttpClientBuilder httpClientBuilder, string scheme, string token)
        {
            return httpClientBuilder.ConfigureHttpClient(client => client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, token));
        }

        public static IHttpClientBuilder AddHeader(this IHttpClientBuilder httpClientBuilder, string name, string value)
        {
            return httpClientBuilder.ConfigureHttpClient(client => client.DefaultRequestHeaders.TryAddWithoutValidation(name, value));
        }
    }
}
