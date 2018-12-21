using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.Core.DTO;
using Test.Core.Infrastructure;
using Test.Core.Interfaces;

namespace Test.Core.SortLogic
{
    public class BubbleSorting : ISortStrategy
    {
        IDataService db;
        bool IsAscending { get; set; }
        
        public BubbleSorting(IDataService dataService)
        {
            db = dataService;
        }

        public IEnumerable<int> Sort(SortDTO dto)
        {
            StorageDTO data = db.GetData();

            if (data != null && data.Values != null && data.Values.Count > 0)
            {
                this.IsAscending = dto.IsAscending;
                var sortedData = BubbleSort(data.Values);
                return sortedData.Take(dto.Length);
            }
            else
                throw new ValidationException("no data to sort", "");

        }

        List<int> BubbleSort(List<int> mas)
        {              
                for (int i = 0; i < mas.Count; i++)
                {
                    for (int j = i + 1; j < mas.Count; j++)
                    {                        
                        Compare(mas, i, j);
                    }
                }
                return mas;
        }

        void Compare(List<int> mas, int i, int j)
        {
            int temp;
            if(IsAscending)
            {
                if (mas[i] > mas[j])
                {
                    temp = mas[i];
                    mas[i] = mas[j];
                    mas[j] = temp;
                }
            }
            else
            {
                if (mas[i] < mas[j])
                {
                    temp = mas[i];
                    mas[i] = mas[j];
                    mas[j] = temp;
                }
            }
        }
    }
}
