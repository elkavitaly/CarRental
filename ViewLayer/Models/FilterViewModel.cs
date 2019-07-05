using System.Collections.Generic;

namespace ViewLayer.Models
{
    /// <summary>
    /// Model for filtering data
    /// </summary>
    public class FilterViewModel
    {
        public Dictionary<string, IEnumerable<string>> Filter { get; set; }
        public string Sort { get; set; }
    }
}