using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Http
{
    public static class WebRequests
    {
        public static WebRequest<T> Get<T>(string url)
        {
            var request = UnityWebRequest.Get(url);
            return new WebRequest<T>(request, WebRequestUtils.GetBodyData<T>);
        }

        public static WebRequest<T> Post<T>(string url, object body = null)
        {
            var request = WebRequestUtils.ResolvePostRequest(url, body);
            return new WebRequest<T>(request, WebRequestUtils.GetBodyData<T>);
        }

        public static WebRequest<T> Post<T>(Uri uri, object body = null)
        {
            var request = WebRequestUtils.ResolvePostRequest(uri, body);
            return new WebRequest<T>(request, WebRequestUtils.GetBodyData<T>);
        }

        public static WebRequest<T> Put<T>(string url, object body = null)
        {
            var request = WebRequestUtils.ResolvePutRequest(url, body);
            return new WebRequest<T>(request, WebRequestUtils.GetBodyData<T>);
        }

        public static WebRequest<T> Put<T>(Uri uri, object body = null)
        {
            var request = WebRequestUtils.ResolvePutRequest(uri, body);
            return new WebRequest<T>(request, WebRequestUtils.GetBodyData<T>);
        }

        public static WebRequest<T> Delete<T>(string url)
        {
            var request = UnityWebRequest.Delete(url);
            return new WebRequest<T>(request, WebRequestUtils.GetBodyData<T>);
        }

        public static WebRequest<T> Delete<T>(Uri uri)
        {
            var request = UnityWebRequest.Delete(uri);
            return new WebRequest<T>(request, WebRequestUtils.GetBodyData<T>);
        }

        public static WebRequest<AssetBundle> GetAssetBundle(string url)
        {
            var request = UnityWebRequestAssetBundle.GetAssetBundle(url);
            return new WebRequest<AssetBundle>(request, DownloadHandlerAssetBundle.GetContent);
        }

        public static WebRequest<AssetBundle> GetAssetBundle(Uri uri)
        {
            var request = UnityWebRequestAssetBundle.GetAssetBundle(uri);
            return new WebRequest<AssetBundle>(request, DownloadHandlerAssetBundle.GetContent);
        }

        public static WebRequest<Texture2D> GetTexture(Uri uri)
        {
            var request = UnityWebRequestTexture.GetTexture(uri);
            return new WebRequest<Texture2D>(request, DownloadHandlerTexture.GetContent);
        }

        public static WebRequest<Texture2D> GetTexture(string url)
        {
            var request = UnityWebRequestTexture.GetTexture(url);
            return new WebRequest<Texture2D>(request, DownloadHandlerTexture.GetContent);
        }

        public static WebRequest<AudioClip> GetAudioClip(string url, AudioType audioType)
        {
            var request = UnityWebRequestMultimedia.GetAudioClip(url, audioType);
            return new WebRequest<AudioClip>(request, DownloadHandlerAudioClip.GetContent);
        }

        public static WebRequest<AudioClip> GetAudioClip(Uri uri, AudioType audioType)
        {
            var request = UnityWebRequestMultimedia.GetAudioClip(uri, audioType);
            return new WebRequest<AudioClip>(request, DownloadHandlerAudioClip.GetContent);
        }
    }
}