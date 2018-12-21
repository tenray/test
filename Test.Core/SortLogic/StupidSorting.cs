using System;
using System.Collections.Generic;
using System.Text;
using Test.Core.DTO;
using Test.Core.Infrastructure;
using Test.Core.Interfaces;
using System.Linq;

namespace Test.Core.SortLogic
{
    public class StupidSorting : ISortStrategy
    {
        IDataService db;
        bool IsAscending { get; set; }

        public StupidSorting(IDataService dataService)
        {
            db = dataService;
        }

        public IEnumerable<int> Sort(SortDTO dto)
        {
            StorageDTO data = db.GetData();

            if (data != null && data.Values != null && data.Values.Count > 0)
            {
                this.IsAscending = dto.IsAscending;
                var sortedData = StupidSort(data.Values);
                return sortedData.Take(dto.Length);
            }
            else
                throw new ValidationException("no data to sort", "");
        }

        private List<int> StupidSort(List<int> arr)
        {
            int i = 0, tmp;
            while (i < arr.Count - 1)
            {
                if (arr[i + 1] > arr[i])
                {
                    tmp = arr[i];
                    arr[i] = arr[i + 1];
                    arr[i + 1] = tmp;
                    i = 0;
                }
                else i++;
            }
            return arr;
        }
    }
}
