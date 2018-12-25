using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Test.Data.Entities;
using Test.Data.Interfaces;

namespace Test.Data.Repositories
{
    public class JsonRepository : IRepository
    {
        StorageModel storage;
        string FilePath { get; set; }

        public static object locker = new object();        

        public JsonRepository()
        {
            //get file path            
            Type currentType = this.GetType();
            string projectName = currentType.Assembly.GetName().Name;
            string fileName = Path.Combine(projectName, @"JSON\storage.json");
            string basePath = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();
            string JsonFilePath = Path.Combine(basePath, fileName);
            FilePath = JsonFilePath;

            //read
            storage = new StorageModel();
            storage.Values = Read();
                        
        }
               
        public List<int> GetValues()
        {           
            return storage.Values;
        }

        public List<int> Read()
        {
            if (!File.Exists(FilePath))
                return null;

            lock(locker)
            {
                string file = File.ReadAllText(FilePath);
                StorageModel st = JsonConvert.DeserializeObject<StorageModel>(file);
                if (st != null)
                    return st.Values;
                else
                    return null;
            }
        }

        public void Write(int[] values)
        {           
            lock(locker)
            {
                storage.Values = values.ToList();
                var json = JsonConvert.SerializeObject(storage, new JsonSerializerSettings());
                if (json != null)
                {
                    try
                    {                                
                        using (var fs = new FileStream(FilePath, FileMode.Create))
                        using (TextWriter tw = new StreamWriter(fs))
                        {
                            tw.WriteLine(json);                            
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }            
            }
        }
    }
}
