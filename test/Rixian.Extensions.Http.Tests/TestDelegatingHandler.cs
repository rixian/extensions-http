// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

using System.Net.Http;

public class TestDelegatingHandler : DelegatingHandler
{
    public TestDelegatingHandler(HttpMessageHandler httpMessageHandler)
        : base(httpMessageHandler)
    {
    }
}
