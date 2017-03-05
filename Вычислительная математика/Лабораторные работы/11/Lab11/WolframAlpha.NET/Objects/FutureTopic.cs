using RestSharp.Deserializers;

namespace Lab11Namespace.Objects
{
    public class FutureTopic
    {
        public string Topic { get; set; }

        [DeserializeAs(Name = "msg")]
        public string Message { get; set; }
    }
}