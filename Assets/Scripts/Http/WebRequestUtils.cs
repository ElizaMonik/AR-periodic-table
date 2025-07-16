using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Http
{
    public static class WebRequestUtils
    {
        private static readonly Dictionary<Type, Func<string, object>> TypeParsers = new()
        {
            { typeof(string), s => s },
            { typeof(int), s => int.Parse(s) },
            { typeof(float), s => float.Parse(s) },
            { typeof(bool), s => bool.Parse(s) },
            { typeof(double), s => double.Parse(s) },
            { typeof(long), s => long.Parse(s) },
            { typeof(short), s => short.Parse(s) },
            { typeof(ushort), s => ushort.Parse(s) },
            { typeof(uint), s => uint.Parse(s) },
            { typeof(ulong), s => ulong.Parse(s) },
            { typeof(char), s => char.Parse(s) },
            { typeof(decimal), s => decimal.Parse(s) }
        };

        public static T GetBodyData<T>(UnityWebRequest request)
        {
            var text = request.downloadHandler.text;
            var data = request.downloadHandler.data;
            var type = typeof(T);

            if (TypeParsers.TryGetValue(type, out var parser))
            {
                return To<T>(parser(text));
            }

            if (type.IsEnum)
            {
                return To<T>(Enum.Parse(type, text));
            }

            if (type == typeof(byte[])) return To<T>(data);
            var contentType = request.GetResponseHeader("Content-Type");
            if (contentType == null)
            {
                throw new NullReferenceException("Content-Type header is null, this is required to infer response type");
            }

            if (contentType.Contains("application/json"))
            {
                return JsonConvert.DeserializeObject<T>(text);
            }

            throw new InvalidOperationException("Unsupported type: " + type + ", url: " + request.url);
        }


        public static UnityWebRequest ResolvePostRequest(string url, object body)
        {
            return body switch
            {
                null => UnityWebRequest.Post(url, string.Empty, "text/plain"),
                string text => UnityWebRequest.Post(url, text, "text/plain"),
                byte[] bytes => UnityWebRequest.Post(url, bytes.ToString(), "application/octet-stream"),
                RequestBody requestBody => UnityWebRequest.Post(url, requestBody.Body, requestBody.ContentType),
                WWWForm form => UnityWebRequest.Post(url, form),
                List<IMultipartFormSection> multipartForm => UnityWebRequest.Post(url, multipartForm),
                Dictionary<string, string> dictionary => UnityWebRequest.Post(url, dictionary),
                _ => throw new ArgumentException("Unsupported body type")
            };
        }

        public static UnityWebRequest ResolvePostRequest(Uri uri, object body)
        {
            return body switch
            {
                null => UnityWebRequest.Post(uri, string.Empty, "text/plain"),
                string text => UnityWebRequest.Post(uri, text, "text/plain"),
                byte[] bytes => UnityWebRequest.Post(uri, bytes.ToString(), "application/octet-stream"),
                RequestBody requestBody => UnityWebRequest.Post(uri, requestBody.Body, requestBody.ContentType),
                WWWForm form => UnityWebRequest.Post(uri, form),
                List<IMultipartFormSection> multipartForm => UnityWebRequest.Post(uri, multipartForm),
                Dictionary<string, string> dictionary => UnityWebRequest.Post(uri, dictionary),
                _ => throw new ArgumentException("Unsupported body type")
            };
        }

        public static UnityWebRequest ResolvePutRequest(string url, object body)
        {
            switch (body)
            {
                case null:
                {
                    return UnityWebRequest.Put(url, string.Empty);
                }
                case byte[] bytes:
                {
                    var request = UnityWebRequest.Put(url, bytes);
                    request.SetRequestHeader("Content-Type", "application/octet-stream");
                    return request;
                }
                case string text:
                {
                    var request = UnityWebRequest.Put(url, text);
                    request.SetRequestHeader("Content-Type", "text/plain");
                    return request;
                }
                case RequestBody requestBody:
                {
                    var request = UnityWebRequest.Put(url, requestBody.Body);
                    request.SetRequestHeader("Content-Type", requestBody.ContentType);
                    return request;
                }
                default:
                {
                    throw new ArgumentException("Unsupported body type");
                }
            }
        }

        public static UnityWebRequest ResolvePutRequest(Uri uri, object body)
        {
            switch (body)
            {
                case null:
                    return UnityWebRequest.Put(uri, string.Empty);
                case byte[] bytes:
                {
                    var request = UnityWebRequest.Put(uri, bytes);
                    request.SetRequestHeader("Content-Type", "application/octet-stream");
                    return request;
                }
                case string text:
                {
                    var request = UnityWebRequest.Put(uri, text);
                    request.SetRequestHeader("Content-Type", "text/plain");
                    return request;
                }
                case RequestBody requestBody:
                {
                    var request = UnityWebRequest.Put(uri, requestBody.Body);
                    request.SetRequestHeader("Content-Type", requestBody.ContentType);
                    return request;
                }
                default:
                {
                    throw new ArgumentException("Unsupported body type for PUT request");
                }
            }
        }

        private static T To<T>(object value) => (T)Convert.ChangeType(value, typeof(T));
    }
}