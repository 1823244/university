using System.Collections.Generic;
using RestSharp.Deserializers;
using Lab11Namespace.Objects.Output;

namespace Lab11Namespace.Objects
{
    public class SubPod
    {
        public string Title { get; set; }
        public string Plaintext { get; set; }

        [DeserializeAs(Name = "img")]
        public Image Image { get; set; }
        public List<State> States { get; set; }
    }
}