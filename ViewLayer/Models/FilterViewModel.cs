using System.Collections;
using System.Collections.Generic;

namespace ViewLayer.Models
{
    public class FilterViewModel
    {
        public Dictionary<string, IEnumerable<string>> Filter { get; set; }
        public string Sort { get; set; }
    }
}