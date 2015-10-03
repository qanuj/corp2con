using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Talent21.Data.Core
{
    [ComplexType]
    public class RangeData<T>
    {
        public T Start { get; set; }
        public T End { get; set; }
    }

    [ComplexType]public class DateRangeData : RangeData<DateTime> { }
    [ComplexType]public class IntRangeData : RangeData<int> { }

}