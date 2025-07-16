using System;
using UnityEngine.Networking;

namespace Http
{
    public class WebRequestException : Exception
    {
        public UnityWebRequest Request { get; }

        public WebRequestException(string message, UnityWebRequest request) : base(message)
        {
            Request = request;
        }

        public T GetBody<T>() => WebRequestUtils.GetBodyData<T>(Request);
    }

}