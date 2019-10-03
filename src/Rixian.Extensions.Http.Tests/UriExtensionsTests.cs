// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

using System;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using RichardSzalay.MockHttp;
using Rixian.Extensions.Tokens;
using Xunit;
using Xunit.Abstractions;

public class UriExtensionsTests
{
    private readonly ITestOutputHelper logger;

    public UriExtensionsTests(ITestOutputHelper logger)
    {
        this.logger = logger;
    }

    [Theory]
    [InlineData("http://localhost", "a", "b", "http://localhost?a=b")]
    [InlineData("http://localhost?c=d", "a", "b", "http://localhost?c=d&a=b")]
    [InlineData("http://localhost?a=b", "a", "c", "http://localhost?a=c")]
    [InlineData("http://localhost?a=b&a=c", "a", "d", "http://localhost?a=d")]
    public void SetSingleQueryParam(string initial, string name, string value, string expected)
    {
        new Uri(initial)
            .SetSingleQueryParam(name, value)
            .Should().Be(new Uri(expected));
    }
}
