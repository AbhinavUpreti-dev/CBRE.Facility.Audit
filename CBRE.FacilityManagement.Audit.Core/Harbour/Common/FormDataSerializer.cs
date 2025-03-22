// // <copyright file="FormDataSerializer.cs" company="CBRE">
// // Copyright (c) CBRE. All rights reserved.
// // </copyright>

namespace CBRE.FacilityManagement.Audit.Core.Harbour.Common
{ 
    using System;
    using System.Collections.Generic;
    using Slapper;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using CBRE.FacilityManagement.Audit.Core.Harbour.Models.Incident;

    /// <summary>
    /// The form data serializer.
    /// </summary>
    public static class FormDataSerializer
    {
        /// <summary>
        /// The json serializer settings
        /// </summary>
        public static JsonSerializerSettings JsonSerializerSettings =
            new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver(), Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore };

        /// <summary>
        /// Initializes static members of the <see cref="FormDataSerializer"/> class.
        /// </summary>
        static FormDataSerializer()
        {
            AutoMapper.Configuration.TypeConverters.Add(new DateTimeConverter());
            AutoMapper.Configuration.TypeConverters.Add(new JsonComplexTypeConverter<Incident.NameOfInjuredWrapper>());
            AutoMapper.Configuration.TypeConverters.Add(new JsonComplexTypeConverter<Incident.ReturnToWorkWrapper>());
            AutoMapper.Configuration.TypeConverters.Add(new JsonComplexTypeConverter<Incident.MatrixWrapper>());
        }

        /// <summary>
        /// Upsert the value.
        /// </summary>
        /// <typeparam name="T">The type of value</typeparam>
        /// <param name="formData">The form data.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void UpsertValue<T>(this Dictionary<string, object> formData, string key, T value)
        {
            dynamic valueToAdd;

            if (typeof(T).IsClass)
            {
                valueToAdd =
                    JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(value, JsonSerializerSettings));
            }
            else
            {
                valueToAdd = value;
            }


            if (formData.ContainsKey(key))
            {
                formData[key] = valueToAdd;
            }
            else
            {
                formData.Add(key, valueToAdd);
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="formData">The form data.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static T GetValue<T>(this Dictionary<string, object> formData, string key)
            where T : class
        {
            if (formData.ContainsKey(key))
            {
                var val = formData[key];

                if (val != null)
                {
                    try
                    {
                        return JsonConvert.DeserializeObject<T>(val.ToString());
                    }
                    catch (Exception e)
                    {
                        // swallow 
                    }
                }
            }

            return default(T);
        }

        /// <summary>
        /// Gets the value as string.
        /// </summary>
        /// <param name="formData">The form data.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string GetValueAsString(this Dictionary<string, object> formData, string key)
        {
            if (formData.ContainsKey(key))
            {
                var val = formData[key];
                return val?.ToString();
            }

            return null;
        }

        /// <summary>
        /// Gets the value as boolean.
        /// </summary>
        /// <param name="formData">The form data.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static bool GetValueAsBoolean(this Dictionary<string, object> formData, string key)
        {
            if (formData.ContainsKey(key))
            {
                var val = formData[key];
                if (Boolean.TryParse(val?.ToString(), out var result))
                {
                    return result;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the value as boolean.
        /// </summary>
        /// <param name="formData">The form data.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static bool? GetValueAsNullableBoolean(this Dictionary<string, object> formData, string key)
        {
            if (formData.ContainsKey(key))
            {
                var val = formData[key];
                if (Boolean.TryParse(val?.ToString(), out var result))
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the value as int.
        /// </summary>
        /// <param name="formData">The form data.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static int GetValueAsInt(this Dictionary<string, object> formData, string key)
        {
            if (formData.ContainsKey(key))
            {
                var val = formData[key];
                if (int.TryParse(val?.ToString(), out var result))
                {
                    return result;
                }
            }

            return -1;
        }

        /// <summary>
        /// Gets the value as boolean with default.
        /// </summary>
        /// <param name="formData">The form data.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <returns></returns>
        public static bool GetValueAsBooleanWithDefault(this Dictionary<string, object> formData, string key, bool defaultValue)
        {
            if (formData.ContainsKey(key))
            {
                var val = formData[key];
                if (Boolean.TryParse(val?.ToString(), out var result))
                {
                    return result;
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// Gets the value as date time.
        /// </summary>
        /// <param name="formData">The form data.</param>
        /// <param name="key">The key.</param>
        /// <returns>The date time</returns>
        public static DateTime GetValueAsDateTime(this Dictionary<string, object> formData, string key)
        {
            if (formData.ContainsKey(key))
            {
                var val = formData[key];
                if (DateTime.TryParse(val?.ToString(), out var result))
                {
                    return result;
                }
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Gets the value as date time.
        /// </summary>
        /// <param name="formData">The form data.</param>
        /// <param name="key">The key.</param>
        /// <returns>The date time</returns>
        public static DateTime? GetValueAsNullableDateTime(this Dictionary<string, object> formData, string key)
        {
            if (formData.ContainsKey(key))
            {
                var val = formData[key];
                if (DateTime.TryParse(val?.ToString(), out var result))
                {
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// The deserialize.
        /// </summary>
        /// <param name="formData">
        /// The form data.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T Deserialize<T>(dynamic formData)
        {
            var dic = new Dictionary<string, object>();
            KeyValuePair<string, object>[] kvps =
                JsonConvert.DeserializeObject<KeyValuePair<string, object>[]>(formData.ToString());
            foreach (var keyValuePair in kvps)
            {
                var key = keyValuePair.Key.Replace(".", "_");
                if (!dic.ContainsKey(key))
                {
                    dic.Add(key, keyValuePair.Value);
                }
            }

            return AutoMapper.Map<T>(dic);
        }

        /// <summary>
        /// The date time converter.
        /// </summary>
        public class DateTimeConverter : AutoMapper.Configuration.ITypeConverter
        {
            /// <summary>
            /// Gets the order.
            /// </summary>
            public int Order => 100;

            /// <summary>
            /// The can convert.
            /// </summary>
            /// <param name="value">
            /// The value.
            /// </param>
            /// <param name="type">
            /// The type.
            /// </param>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            public bool CanConvert(object value, Type type)
            {
                var type1 = Nullable.GetUnderlyingType(type);
                if ((object)type1 == null)
                {
                    type1 = type;
                }

                return type1 == typeof(DateTime);
            }

            /// <summary>
            /// The convert.
            /// </summary>
            /// <param name="value">
            /// The value.
            /// </param>
            /// <param name="type">
            /// The type.
            /// </param>
            /// <returns>
            /// The <see cref="object"/>.
            /// </returns>
            public object Convert(object value, Type type)
            {
                var enumType = Nullable.GetUnderlyingType(type);
                if ((object)enumType == null)
                {
                    enumType = type;
                }

                if (DateTime.TryParse(value.ToString(), out var dt))
                {
                    return dt;
                }

                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// The json complex type converter.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        public class JsonComplexTypeConverter<T> : AutoMapper.Configuration.ITypeConverter
        {
            /// <summary>
            /// Gets the order.
            /// </summary>
            public int Order => 100;

            /// <summary>
            /// The can convert.
            /// </summary>
            /// <param name="value">
            /// The value.
            /// </param>
            /// <param name="type">
            /// The type.
            /// </param>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            public bool CanConvert(object value, Type type)
            {
                var type1 = Nullable.GetUnderlyingType(type);
                if ((object)type1 == null)
                {
                    type1 = type;
                }

                return type1 == typeof(T);
            }

            /// <summary>
            /// The convert.
            /// </summary>
            /// <param name="value">
            /// The value.
            /// </param>
            /// <param name="type">
            /// The type.
            /// </param>
            /// <returns>
            /// The <see cref="object"/>.
            /// </returns>
            public object Convert(object value, Type type)
            {
                var enumType = Nullable.GetUnderlyingType(type);
                if ((object)enumType == null)
                {
                    enumType = type;
                }

                try
                {
                    return JsonConvert.DeserializeObject<T>(value.ToString());
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}