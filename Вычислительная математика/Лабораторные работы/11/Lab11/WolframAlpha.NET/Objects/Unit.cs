using RestSharp.Deserializers;
using Lab11Namespace.Objects.Output;

namespace Lab11Namespace.Objects
{
    public class Unit
    {
        public string ShortName { get; set; }
        public string LongName { get; set; }

        [DeserializeAs(Name = "img")]
        public Image Image { get; set; }
    }
}