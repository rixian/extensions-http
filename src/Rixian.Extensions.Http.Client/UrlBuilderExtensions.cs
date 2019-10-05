// Copyright (c) Rixian. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE file in the project root for full license information.

namespace Rixian.Extensions.Http.Client
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

    /// <summary>
    /// Extensions for working with an IUrlBuilder.
    /// </summary>
    public static class UrlBuilderExtensions
    {
        /// <summary>
        /// Replaces tokenized values in the url path.
        /// </summary>
        /// <param name="urlBuilder">The IUrlBuilder.</param>
        /// <param name="token">The token to replace in the path.</param>
        /// <param name="value">The replacement value.</param>
        /// <returns>The updated IUrlBuilder.</returns>
        public static IUrlBuilder ReplaceToken(this IUrlBuilder urlBuilder, string token, object value)
        {
            if (urlBuilder is null)
            {
                throw new ArgumentNullException(nameof(urlBuilder));
            }

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException(Properties.Resources.ParameterStringEmptyError, nameof(token));
            }

            urlBuilder.Path = urlBuilder.Path.Replace(token, Uri.EscapeDataString(ConvertToString(value, CultureInfo.InvariantCulture)));

            return urlBuilder;
        }

        /// <summary>
        /// Sets a single query parameter.
        /// </summary>
        /// <param name="urlBuilder">The IUrlBuilder.</param>
        /// <param name="key">The query parameter key.</param>
        /// <param name="value">The query parameter value.</param>
        /// <param name="ignoreIfNull">Ignores this query parameter if the value is null.</param>
        /// <param name="escapeValue">Choose to uri escape the value.</param>
        /// <returns>The updated IUrlBuilder.</returns>
        public static IUrlBuilder SetQueryParam(this IUrlBuilder urlBuilder, string key, object value, bool ignoreIfNull = true, bool escapeValue = true)
        {
            if (urlBuilder is null)
            {
                throw new ArgumentNullException(nameof(urlBuilder));
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException(Properties.Resources.ParameterStringEmptyError, nameof(key));
            }

            if (ignoreIfNull && value == null)
            {
                return urlBuilder;
            }

            var stringValue = ConvertToString(value, CultureInfo.InvariantCulture);
            if (escapeValue)
            {
                stringValue = Uri.EscapeDataString(stringValue);
            }

            urlBuilder.QueryParams.Add(new KeyValuePair<string, string>(key, stringValue));
            return urlBuilder;
        }

        /// <summary>
        /// Materialized the current url into an IHttpRequestMessageBuilder.
        /// </summary>
        /// <param name="urlBuilder">The IUrlBuilder.</param>
        /// <returns>The IHttpRequestMessageBuilder.</returns>
        public static IHttpRequestMessageBuilder ToRequest(this IUrlBuilder urlBuilder)
        {
            if (urlBuilder is null)
            {
                throw new ArgumentNullException(nameof(urlBuilder));
            }

            return HttpRequestMessageBuilder.Create(urlBuilder.Uri);
        }

        /// <summary>
        /// Materializes the query parameters into a query string.
        /// </summary>
        /// <param name="urlBuilder">The IUrlBuilder.</param>
        /// <returns>The query string.</returns>
        public static string ToQueryString(this IUrlBuilder urlBuilder)
        {
            if (urlBuilder is null)
            {
                throw new ArgumentNullException(nameof(urlBuilder));
            }

            ICollection<KeyValuePair<string, string>> queryParams = urlBuilder.QueryParams;
            if (queryParams.Count == 0)
            {
                return string.Empty;
            }

            var values = queryParams.Select(p => $"{p.Key}={p.Value}").ToArray();
            return "?" + string.Join("&", values);
        }

        private static string ConvertToString(object value) => ConvertToString(value, CultureInfo.InvariantCulture);

        private static string ConvertToString(object value, CultureInfo cultureInfo)
        {
            switch (value)
            {
                case Enum e when value is Enum:
                    {
                        Type type = e.GetType();
                        string name = Enum.GetName(type, e);
                        if (name == null)
                        {
                            break;
                        }

                        FieldInfo field = type.GetTypeInfo().GetDeclaredField(name);
                        if (field == null)
                        {
                            break;
                        }

                        if (field.GetCustomAttribute(typeof(EnumMemberAttribute)) is EnumMemberAttribute attribute)
                        {
                            return attribute.Value ?? name;
                        }

                        break;
                    }

                case bool b when value is bool:
                    {
#pragma warning disable CA1308 // Normalize strings to uppercase
                        return Convert.ToString(b, cultureInfo).ToLowerInvariant();
#pragma warning restore CA1308 // Normalize strings to uppercase
                    }

#pragma warning disable SA1011 // Closing square brackets should be spaced correctly
                case byte[] a when value is byte[]:
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
                    {
                        return Convert.ToBase64String(a);
                    }

                case Array a when value != null && value.GetType().IsArray:
                    {
                        IEnumerable<string> array = a.OfType<object>().Select(o => ConvertToString(o, cultureInfo));
                        return string.Join(",", a);
                    }

                default:
                    break;
            }

            return Convert.ToString(value, cultureInfo);
        }
    }
}
