using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Services
{
    internal class FileIOService
    {

        private readonly string PATH;

        public FileIOService(string path)
        {
            PATH = path;
        }

        public BindingList<TodoModel> LoadData()
        {
           var fileExist = File.Exists(PATH);
           if (!fileExist)
           {
               File.CreateText(PATH).Dispose();
               return new BindingList<TodoModel>();
           }
           using (var reader = File.OpenText(PATH))
           {
               var fileText = reader.ReadToEnd();
               return JsonConvert.DeserializeObject<BindingList<TodoModel>>(fileText);
           }
        }

        public void SaveData(object todoDataList) 
        {
            using (StreamWriter Writer = File.CreateText(PATH))
            {
                string output = JsonConvert.SerializeObject(todoDataList);
                Writer.Write(output);
            }
        }
    }
}
