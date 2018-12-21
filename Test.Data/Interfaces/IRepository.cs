using System;
using System.Collections.Generic;
using System.Text;
using Test.Data.Entities;

namespace Test.Data.Interfaces
{
    public interface IRepository
    {
        //StorageModel Read();
        List<int> Read();
        void Write(int[] values);
        List<int> GetValues();
    }
}
