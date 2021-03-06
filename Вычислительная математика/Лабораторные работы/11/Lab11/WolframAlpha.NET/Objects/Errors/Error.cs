using RestSharp.Deserializers;

namespace Lab11Namespace.Objects.Errors
{
    public class Error
    {
        public int Code { get; set; }

        [DeserializeAs(Name = "msg")]
        public string Message { get; set; }
    }
}