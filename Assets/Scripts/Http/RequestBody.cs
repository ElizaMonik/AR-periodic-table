using Newtonsoft.Json;

namespace Http
{
    public class RequestBody
    {
        public string ContentType { get; }

        public string Body { get; }

        private RequestBody(string contentType, string body)
        {
            ContentType = contentType;
            Body = body;
        }

        public static RequestBody Json(object json)
        {
            return new RequestBody("application/json", JsonConvert.SerializeObject(json));
        }
    }
}