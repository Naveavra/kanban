using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    enum Status
    {
        Backlog,
        Inprogress,
        Done
    }
    [Serializable]
    internal class Task
    {
        public int Id { get; }
        public DateTime CreationTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        private string Assignee { get;  set; }
        private Status Status { get; set; }
        [JsonIgnore] //i think that if we delete the get ans set the json will skip it 
        public TaskDTO? DTO { get;private set; }
        public Task(string title, string description, DateTime dueDate,int taskID)
        {
            this.Status = Status.Backlog;
            this.Title = title;
            this.Description = description;
            this.DueDate = dueDate;
            //this.currentBoard = currentBoard;
            this.Id = taskID;
            Assignee = "";
            CreationTime = DateTime.Now.Date;

        }
        public void setDTO(TaskDTO t)
        {
            this.DTO = t;
        }
        public int gettaskID(){return this.Id; }
        public string gettitle() { return this.Title; }

        public string getAssignee() { return this.Assignee; }
        public string getDescription() { return this.Description; } 
        //public string getCurrentBoard() { return this.currentBoard; }   
        public DateTime getdueDate() { return this.DueDate; }
        public string getCreationTime() { return this.CreationTime.ToString();  ; }
/*        public override string ToString()
        {
            string status1 = "Backlog";
            string status2 = "In Progress";
            string status3 = "Done";
            string theStatus;
            if (this.Status == 1) { theStatus = status1; }
            else if (this.Status == 2) { theStatus = status2; }
            else if (this.Status == 3) { theStatus = status3; }
            string result = "";
            result = result + "\n" + "Task ID: " + this.taskID +
                              "\n" + "Task title: " + this.title +
                              "\n" + "Task description: " + this.description +
                              "\n" + "The creation time: " + this.creationDate +
                              "\n" + "Task status: " + Status +
                              "\n" + "The due time: " + this.dueDate + "\n";
            return result;
            
        }*/
        public void setAssignee(string assignee)
        {
            this.Assignee = assignee;
            if (DTO != null)
            {
                DTO.Assignee = assignee;
            }
        }
        public void EditTaskDesc(string email,string newDesc)
        {
            if(this.Status.ToString().Equals("Done") || Assignee!=email)
            {
                throw new Exception("User is not assigned to this Task.");
            }
            DTO.Description = newDesc;
            Description = newDesc;
        }
    

    internal void EditTaskDueDate(string email,DateTime newDue)
        {
            if (DateTime.Compare(newDue, DateTime.Now) < 0)
                throw new Exception("Invalid update task due, date given has already passed");
            if (this.Status.ToString().Equals("Done")|| Assignee!= email)
            {
                throw new Exception("User is not assigned to this task");
            }
            DTO.DueDate = newDue;
            DueDate = newDue;
        }

        internal void EditTaskTitle(string email,string newTitle)
        {
            if(this.Status.ToString().Equals("Done")||Assignee!=email)
            {
                throw new Exception("User isn't assigned to this task");
            }
            DTO.Title = newTitle;
            Title = newTitle;
        }
        internal void ChangeStatus(int Stat)
        {
            switch(Stat)
                {
                case 0: this.Status = Status.Backlog; break;
                case 1: this.Status = Status.Inprogress; break;
                case 2: this.Status = Status.Done; break;
            }
        }

        public override string ToString()
        {
            return  "Task title: " + Title + "\nTask Due Date: " + "\nTask ID: " + Id + "\nTask Description: " + 
                "\nCreated in: " + CreationTime.ToString();
        }
    }
}
