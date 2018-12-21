using System;
using Test.Core.DTO;
using Test.Core.Infrastructure;
using Test.Core.Interfaces;
using Test.Data;
using Test.Data.Interfaces;

namespace Test.Core.Services
{
    public class DataService : IDataService
    {
        IRepository db;

        public DataService(IRepository _rep)
        {
            this.db = _rep;
        }

        public StorageDTO GetData()
        {
            var values = db.GetValues();

            if(values == null)
                throw new ValidationException("storage is empty", "");

            return new StorageDTO { Values = values };
        }

        public void SetData(StorageDTO storage)
        {
            //validation
            if (storage == null)
                throw new ValidationException("DTO null", "");

            if (storage.Values == null)
                throw new ValidationException("DTO values null", "");

            db.Write(storage.Values.ToArray());
        }
    }
}
