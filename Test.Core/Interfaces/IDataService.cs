using System;
using System.Collections.Generic;
using System.Text;
using Test.Core.DTO;

namespace Test.Core.Interfaces
{
    public interface IDataService
    {
        void SetData(StorageDTO storage);
        StorageDTO GetData(); 
    }
}
