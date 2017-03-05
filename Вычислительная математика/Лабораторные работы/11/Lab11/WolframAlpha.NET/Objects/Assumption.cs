using System.Collections.Generic;

namespace Lab11Namespace.Objects
{
    public class Assumption
    {
        public string Type { get; set; }
        public string Word { get; set; }
        public string Template { get; set; }
        public List<Value> Values { get; set; }
    }
}