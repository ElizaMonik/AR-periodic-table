using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Http
{
    public class WebRequest<T>
    {
        private readonly UnityWebRequest _request;
        private readonly Func<UnityWebRequest, T> _resolveBody;

        private Action<T> _onSuccess;
        private Action<Exception> _onError;
        private Action _onFinish;

        public T Data { get; private set; }
        public Exception Error { get; private set; }

        public Dictionary<string, string> ResponseHeaders { get; private set; }

        public WebRequest(UnityWebRequest request, Func<UnityWebRequest, T> resolveBody)
        {
            _request = request;
            _resolveBody = resolveBody;
        }

        private static bool ShowLog => false;

        public WebRequest<T> Then(Action<T> onSuccess)
        {
            _onSuccess = onSuccess;
            return this;
        }

        public WebRequest<T> Catch(Action<Exception> onError)
        {
            _onError = onError;
            return this;
        }

        public WebRequest<T> Finally(Action onFinish)
        {
            _onFinish = onFinish;
            return this;
        }

        public WebRequest<T> WithHeaders(Dictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                WithHeader(header.Key, header.Value);
            }

            return this;
        }

        public WebRequest<T> WithHeader(string header, string value)
        {
            _request.SetRequestHeader(header, value);
            return this;
        }


        private static void ThrowIfContainsErrors(UnityWebRequest request)
        {
            switch (request.result)
            {
                case UnityWebRequest.Result.Success:
                    return;
                case UnityWebRequest.Result.ConnectionError:
                    throw new WebRequestException("Connection error: " + request.error, request);
                case UnityWebRequest.Result.ProtocolError:
                    throw new WebRequestException("Protocol error: " + request.error, request);
                case UnityWebRequest.Result.DataProcessingError:
                    throw new WebRequestException("Data processing error: " + request.error, request);
                case UnityWebRequest.Result.InProgress:
                    throw new WebRequestException("Request is still in progress: " + request.error, request);
                default:
                    throw new WebRequestException("Unknown error: " + request.error, request);
            }
        }

        private void DispatchRequest()
        {
            ResponseHeaders = _request.GetResponseHeaders();

            try
            {
                ThrowIfContainsErrors(_request);
                Data = _resolveBody(_request);
                _onSuccess?.Invoke(Data);
            }
            catch (Exception e)
            {
                if (ShowLog)
                {
                    Debug.LogError(
                        "Error: " + e.Message + "\n" +
                        "Request URL: " + _request.url + "\n" +
                        "Request Body: " + _request.downloadHandler.text
                    );
                }
                Error = e;
                _onError?.Invoke(Error);
            }
            finally
            {
                _onFinish?.Invoke();
                _request.Dispose();
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public IEnumerator Send()
        {
            if (ShowLog) Debug.LogFormat("Sending request: {0}, {1}", _request.method, _request.url);
            yield return _request.SendWebRequest();
            DispatchRequest();
        }

        public void SendAsync()
        {
            if (ShowLog) Debug.LogFormat("Sending request: {0}, {1}", _request.method, _request.url);
            var operation = _request.SendWebRequest();
            operation.completed += _ => DispatchRequest();
        }
    }
}