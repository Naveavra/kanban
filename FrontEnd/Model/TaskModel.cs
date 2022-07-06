using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace FrontEnd.Model
{/*CreationTime { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    private string Assignee*/
    public class TaskModel : INotifyPropertyChanged
    {
        public string Title;
        public string CreationTime;
        public string Description;
        public string DueDate;
        public string Id;
        public TaskModel(string Json)
        {

            JObject jObject = JObject.Parse(Json);
            Id = jObject["Id"].ToString();
            Title = jObject["Title"].ToString();
            CreationTime = jObject["CreationTime"].ToString();
            Description = jObject["Description"].ToString();
            DueDate = jObject["DueDate"].ToString();
        }
        public TaskModel(string title,string desc,string dueDate,string assignee)
        {
            Title = title;
            Description = desc;
            DueDate = dueDate;
        }
        public override string ToString()
        {
            return Title+$"\n ID:{Id}";
        }
        public string FullString()
        {
            return $"ID: {Id}\n\n" +
                $"Title: {Title}\n\n" +
                $"Description: {Description}\n\n" +
                $"CreationTime: {CreationTime}\n\n" +
                $"DueDate: {DueDate}.";
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
