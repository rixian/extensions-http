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
        private const string ApplicationProblemJsonContentType = "application/problem+json";
        private const string ApplicationProblemXmlContentType = "application/problem+xml";
        private const string ApplicationProblemContentTypePrefix = "application/problem";

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

            if (responseMessage.Content == null)
            {
                return default;
            }

            var json = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            T result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            return result;
        }

        /// <summary>
        /// Checks if the content type of the response message is a problem detail.
        /// See: https://tools.ietf.org/html/rfc7807 for details.
        /// </summary>
        /// <param name="responseMessage">The HttpResponseMessage.</param>
        /// <returns>True if the content type is a problem, otherwise false.</returns>
        public static bool IsContentProblem(this HttpResponseMessage responseMessage)
        {
            return responseMessage?.Content?.Headers?.ContentType?.MediaType?.StartsWith(ApplicationProblemContentTypePrefix, StringComparison.OrdinalIgnoreCase) ?? false
                || string.Equals(responseMessage?.Content?.Headers?.ContentType?.MediaType, ApplicationProblemJsonContentType, StringComparison.OrdinalIgnoreCase)
                || string.Equals(responseMessage?.Content?.Headers?.ContentType?.MediaType, ApplicationProblemXmlContentType, StringComparison.OrdinalIgnoreCase);
        }
    }
}
