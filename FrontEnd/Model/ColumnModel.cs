using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace FrontEnd.Model
{
    internal class ColumnModel : INotifyPropertyChanged
    {
        public string Title;
        public ObservableCollection<TaskModel> Tasks;
        public string Json;
        public ColumnModel(string title,string json) { Title = title; 
            Json = json; 
            Tasks = new ObservableCollection<TaskModel>();
            JsonNode node = JsonNode.Parse(Json);
            JsonArray arr = node!["ReturnValue"]!.AsArray();
            for(int i = 0; i < arr.Count; i++)
            {
                var task = arr[i];
                Tasks.Add(new TaskModel(task.ToJsonString()));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public override string ToString()
        {
            return Json;
        }

 

        internal TaskModel GetTask(string taskID)
        {
            foreach(TaskModel task in Tasks)
            {
                if(task.Id == taskID)
                {
                    return task;
                }
            }
            return null;
        }
    }
}
