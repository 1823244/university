using System.Collections.Generic;
using RestSharp.Deserializers;
using Lab11Namespace.Objects.Output;

namespace Lab11Namespace.Objects
{
    public class Info
    {
        public string Text { get; set; }

        [DeserializeAs(Name = "img")]
        public Image Image { get; set; }
        public List<Link> Links { get; set; }
    }
}