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
    /// Options for configuring an api version via the query string.
    /// </summary>
    public class ApiVersionQueryOptions
    {
        /// <summary>
        /// Gets or sets the query parameter name to use.
        /// </summary>
        public string QueryParamName { get; set; } = "api-version";

        /// <summary>
        /// Gets or sets the api version value.
        /// </summary>
        public string? Value { get; set; }
    }
}
