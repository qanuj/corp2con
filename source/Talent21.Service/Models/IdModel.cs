using System.Collections.Generic;

namespace Talent21.Service.Models
{
    public class IdModel
    {
        public int Id { get; set; }
    }

    public class IdLabel<T>
    {
        public T Id { get; set; }
        public string Label { get; set; }
    }

    public class CountLabel<T,L>
    {
        public T Count { get; set; }
        public L Label { get; set; }
    }

    public class CountLabel<T> : CountLabel<T, string> { }

    public class FilterLabel<T> : FilterLabel<T, string>
    {
    }

    public class FilterLabel<T,X> : CountLabel<T, X>
    {
        public bool? Selected { get; set; }
        public string Mode { get; set; } 
        public FilterLabel()
        {
            this.Mode = "string";
        } 
    }

    public class EnumList : Dictionary<string, IEnumerable<IdLabel<string>>>
    {
    }
}