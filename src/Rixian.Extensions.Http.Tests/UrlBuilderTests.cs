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
using Rixian.Extensions.Http.Client;
using Rixian.Extensions.Tokens;
using Xunit;
using Xunit.Abstractions;

public class UrlBuilderTests
{
    private readonly ITestOutputHelper logger;

    public UrlBuilderTests(ITestOutputHelper logger)
    {
        this.logger = logger;
    }

    [Fact]
    public void SetSingleQueryParam()
    {
        var tenantId = Guid.NewGuid();
        var libraryId = Guid.NewGuid();
        
        Uri uri = UrlBuilder
                  .Create("libraries/{libraryId}/cmd/exists")
                  .ReplaceToken("{libraryId}", libraryId)
                  .SetQueryParam("path", "/foo")
                  .SetQueryParam("tenantId", tenantId)
                  .Uri;

        uri.Should().NotBeNull();
    }
}
