using RestSharp.Deserializers;

namespace Lab11Namespace.Objects
{
    public class RelatedExample
    {
        public string Input { get; set; }
        
        [DeserializeAs(Name = "desc")]
        public string Description { get; set; }
        public string Category { get; set; }
        public string CategoryThumb { get; set; }
        public string CategoryPage { get; set; }
    }
}