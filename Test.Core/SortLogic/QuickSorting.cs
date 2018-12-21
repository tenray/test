using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.Core.DTO;
using Test.Core.Infrastructure;
using Test.Core.Interfaces;

namespace Test.Core.SortLogic
{
    public class QuickSorting : ISortStrategy
    {
        IDataService db;
        bool isAscending { get; set; }

        public QuickSorting(IDataService dataService)
        {
            db = dataService;
        }

        public IEnumerable<int> Sort(SortDTO dto)
        {
            StorageDTO data = db.GetData();

            if (data != null && data.Values != null && data.Values.Count > 0)
            {                
                this.isAscending = dto.IsAscending;

                List<int> arr = data.Values;
                int count = arr.Count;
                Quick_Sort(ref arr, 0, arr.Count - 1, ref count);               

                return arr.Take(dto.Length);
            }
            else
                throw new ValidationException("no data to sort", "");
        }

        public void Quick_Sort(ref List<int> data, int left, int right, ref int count)
        {
            int i;
            int j;
            int pivot;
            int temp;
            i = left;
            j = right;
            pivot = data[(left + right) / 2];
            do
            {
                if(isAscending)
                {
                    while ((data[i] < pivot) && (i < right)) i++;
                    count++;
                    while ((pivot < data[j]) && (j > left)) j--;
                    count++;
                }
                else
                {
                    while ((data[i] > pivot) && (i < right)) i++;
                    count++;
                    while ((pivot > data[j]) && (j > left)) j--;
                    count++;
                }
                
                if (i <= j)
                {
                    temp = data[i];
                    data[i] = data[j];
                    data[j] = temp;
                    i++;
                    j--;
                    count++;
                }
            } while (i <= j);
            if (left < j) Quick_Sort(ref data, left, j, ref count);
            if (i < right) Quick_Sort(ref data, i, right, ref count);
        }
      
    }
}
