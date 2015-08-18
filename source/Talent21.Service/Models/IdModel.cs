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

    public class EnumList : Dictionary<string, IEnumerable<IdLabel<string>>>
    {
    }
}