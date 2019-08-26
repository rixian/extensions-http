// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using RichardSzalay.MockHttp;
using Rixian.Extensions.Tokens;
using Xunit;
using Xunit.Abstractions;

public class HttpClientExtenstionsTests
{
    private readonly ITestOutputHelper logger;

    public HttpClientExtenstionsTests(ITestOutputHelper logger)
    {
        this.logger = logger;
    }

    [Fact]
    public async Task AddBearerToken_Default_Success()
    {
        var bearerToken = Guid.NewGuid().ToString();
        var mockHttp = new MockHttpMessageHandler();
        MockedRequest request = mockHttp
            .When("*")
            .WithHeaders("Authorization", $"Bearer {bearerToken}")
            .Respond(HttpStatusCode.OK);

        HttpClient httpClient = GetTestHttpClient(mockHttp, b =>
        {
            b.AddBearerToken(bearerToken);
        });

        _ = await httpClient.GetAsync(new Uri("http://localhost")).ConfigureAwait(false);

        mockHttp.GetMatchCount(request).Should().Be(1);
    }

    [Fact]
    public async Task AddBearerToken_NullToken_Success()
    {
        string bearerToken = null;
        var mockHttp = new MockHttpMessageHandler();
        MockedRequest request = mockHttp
            .When("*")
            .WithHeaders("Authorization", $"Bearer")
            .Respond(HttpStatusCode.OK);

        HttpClient httpClient = GetTestHttpClient(mockHttp, b =>
        {
            b.AddBearerToken(bearerToken);
        });

        _ = await httpClient.GetAsync(new Uri("http://localhost")).ConfigureAwait(false);

        mockHttp.GetMatchCount(request).Should().Be(1);
    }

    [Fact]
    public async Task AddBearerToken_EmptyToken_Success()
    {
        string bearerToken = null;
        var mockHttp = new MockHttpMessageHandler();
        MockedRequest request = mockHttp
            .When("*")
            .WithHeaders("Authorization", $"Bearer")
            .Respond(HttpStatusCode.OK);

        HttpClient httpClient = GetTestHttpClient(mockHttp, b =>
        {
            b.AddBearerToken(bearerToken);
        });

        _ = await httpClient.GetAsync(new Uri("http://localhost")).ConfigureAwait(false);

        mockHttp.GetMatchCount(request).Should().Be(1);
    }

    [Fact]
    public async Task AddHeader_Default_Success()
    {
        string headerName = "Foo";
        string headerValue = "Bar";
        var mockHttp = new MockHttpMessageHandler();
        MockedRequest request = mockHttp
            .When("*")
            .WithHeaders(headerName, headerValue)
            .Respond(HttpStatusCode.OK);

        HttpClient httpClient = GetTestHttpClient(mockHttp, b =>
        {
            b.AddHeader(headerName, headerValue);
        });

        _ = await httpClient.GetAsync(new Uri("http://localhost")).ConfigureAwait(false);

        mockHttp.GetMatchCount(request).Should().Be(1);
    }

    [Fact]
    public async Task AddTokenClient_Instance_Success()
    {
        var accessToken = Guid.NewGuid().ToString();
        ITokenInfo tokenInfo = Substitute.For<ITokenInfo>();
        tokenInfo.AccessToken.Returns(accessToken);
        ITokenClient tokenClient = Substitute.For<ITokenClient>();
        tokenClient.GetTokenAsync(Arg.Any<bool>()).Returns(tokenInfo);

        var mockHttp = new MockHttpMessageHandler();
        MockedRequest request = mockHttp
            .When("*")
            .WithHeaders("Authorization", $"Bearer {accessToken}")
            .Respond(HttpStatusCode.OK);

        HttpClient httpClient = GetTestHttpClient(mockHttp, b =>
        {
            b.AddTokenClient(tokenClient);
        });

        _ = await httpClient.GetAsync(new Uri("http://localhost")).ConfigureAwait(false);

        mockHttp.GetMatchCount(request).Should().Be(1);
    }

    [Fact]
    public async Task AddTokenClient_ServiceLookup_Success()
    {
        var accessToken = Guid.NewGuid().ToString();
        ITokenInfo tokenInfo = Substitute.For<ITokenInfo>();
        tokenInfo.AccessToken.Returns(accessToken);
        ITokenClient tokenClient = Substitute.For<ITokenClient>();
        tokenClient.GetTokenAsync(Arg.Any<bool>()).Returns(tokenInfo);
        ITokenClientFactory tokenClientFactory = Substitute.For<ITokenClientFactory>();
        tokenClientFactory.GetTokenClient(Arg.Any<string>()).Returns(tokenClient);

        var mockHttp = new MockHttpMessageHandler();
        MockedRequest request = mockHttp
            .When("*")
            .WithHeaders("Authorization", $"Bearer {accessToken}")
            .Respond(HttpStatusCode.OK);

        HttpClient httpClient = GetTestHttpClient(
            mockHttp,
            b =>
            {
                b.AddTokenClient(svc => svc.GetRequiredService<ITokenClientFactory>().GetTokenClient());
            },
            svc =>
            {
                svc.AddSingleton(tokenClientFactory);
            });

        _ = await httpClient.GetAsync(new Uri("http://localhost")).ConfigureAwait(false);

        mockHttp.GetMatchCount(request).Should().Be(1);
    }

    [Fact]
    public async Task AddTokenClient_NameLookup_Success()
    {
        var accessToken = Guid.NewGuid().ToString();
        var tokenClientName = Guid.NewGuid().ToString();

        ITokenInfo tokenInfo = Substitute.For<ITokenInfo>();
        tokenInfo.AccessToken.Returns(accessToken);
        ITokenClient tokenClient = Substitute.For<ITokenClient>();
        tokenClient.GetTokenAsync(Arg.Any<bool>()).Returns(tokenInfo);
        ITokenClientFactory tokenClientFactory = Substitute.For<ITokenClientFactory>();
        tokenClientFactory.GetTokenClient(tokenClientName).Returns(tokenClient);

        var mockHttp = new MockHttpMessageHandler();
        MockedRequest request = mockHttp
            .When("*")
            .WithHeaders("Authorization", $"Bearer {accessToken}")
            .Respond(HttpStatusCode.OK);

        HttpClient httpClient = GetTestHttpClient(
            mockHttp,
            b =>
            {
                b.AddTokenClient(tokenClientName);
            },
            svc =>
            {
                svc.AddSingleton(tokenClientFactory);
            });

        _ = await httpClient.GetAsync(new Uri("http://localhost")).ConfigureAwait(false);

        mockHttp.GetMatchCount(request).Should().Be(1);
    }

    private static HttpClient GetTestHttpClient(MockHttpMessageHandler mockHttpMessageHandler, Action<IHttpClientBuilder> configureClient, Action<IServiceCollection> configureServices = null)
    {
        var serviceCollection = new ServiceCollection();
        IHttpClientBuilder httpClientBuilder = serviceCollection.AddHttpClient("test");
        httpClientBuilder.ConfigurePrimaryHttpMessageHandler(() => mockHttpMessageHandler);
        configureClient?.Invoke(httpClientBuilder);
        configureServices?.Invoke(serviceCollection);
        ServiceProvider services = serviceCollection.BuildServiceProvider();
        HttpClient httpClient = services.GetRequiredService<IHttpClientFactory>().CreateClient("test");
        return httpClient;
    }
}