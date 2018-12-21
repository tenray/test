using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.Core.DTO;
using Test.Core.Infrastructure;
using Test.Core.Interfaces;

namespace Test.Core.SortLogic
{
    public class SelectionSorting : ISortStrategy
    {
        IDataService db;       

        public SelectionSorting(IDataService dataService)
        {
            db = dataService;
        }

        public IEnumerable<int> Sort(SortDTO dto)
        {
            StorageDTO data = db.GetData();

            if (data != null && data.Values != null && data.Values.Count > 0)
            {                
                List<int> sortedData;
                
                if(dto.IsAscending)
                    sortedData = SelectionASC(data.Values);
                else
                    sortedData = SelectionDSC(data.Values);

                return sortedData.Take(dto.Length);
            }
            else
                throw new ValidationException("no data to sort", "");
        }

        private List<int> SelectionASC(List<int> mas)
        {           
                for (int i = 0; i < mas.Count - 1; i++)
                {                    
                    int min = i;
                    for (int j = i + 1; j < mas.Count; j++)
                    {
                        if (mas[j] < mas[min])
                        {
                            min = j;
                        }
                    }
                   
                    int temp = mas[min];
                    mas[min] = mas[i];
                    mas[i] = temp;
                }
                return mas;
        }


        private List<int> SelectionDSC(List<int> mas)
        {
            for (int i = 0; i < mas.Count - 1; i++)
            {
                int max = i;
                for (int j = i + 1; j < mas.Count; j++)
                {
                    if (mas[j] > mas[max])
                    {
                        max = j;
                    }
                }

                int temp = mas[max];
                mas[max] = mas[i];
                mas[i] = temp;
            }
            return mas;
        }
    }
}
