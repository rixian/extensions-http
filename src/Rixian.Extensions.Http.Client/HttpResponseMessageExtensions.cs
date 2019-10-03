// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Http.Client
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Extensions for working with HttpResponseMessages.
    /// </summary>
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Deserializes the response content as JSON into an object.
        /// </summary>
        /// <typeparam name="T">The target type for deserialization.</typeparam>
        /// <param name="responseMessage">The HttpResponseMessage.</param>
        /// <returns>The deserialized content.</returns>
        public static async Task<T> DeserializeJsonContentAsync<T>(this HttpResponseMessage responseMessage)
        {
            if (responseMessage is null)
            {
                throw new ArgumentNullException(nameof(responseMessage));
            }

            var json = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            T result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            return result;
        }
    }
}
