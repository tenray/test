using System;
using System.Collections.Generic;
using System.Text;
using Test.Core.DTO;

namespace Test.Core.Interfaces
{
    public interface ISortStrategy
    {
        IEnumerable<int> Sort(SortDTO dto);
    }
}
