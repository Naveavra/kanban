using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    internal class Task
    {
        public int Id { get; }
        public DateTime CreationTime { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        //private string currentBoard;
        private int Status;
        public DateTime DueDate { get; set; }

        public Task(string title, string description, DateTime dueDate,int taskID)
        {
            this.Title = title;
            this.Description = description;
            this.DueDate = dueDate;
            //this.currentBoard = currentBoard;
            this.Id = taskID;
            CreationTime = DateTime.Now.Date;

        }
        public int gettaskID(){return this.Id; }
        public string gettitle() { return this.Title; }
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

        public bool EditTaskDesc(string newDesc)
        {
            if(this.Status.ToString().Equals("Done"))
            {
                return false;
            }
            Description = newDesc;
            return true;
        }

        internal bool EditTaskDueDate(DateTime newDue)
        {
            if (this.Status.ToString().Equals("Done"))
            {
                return false;
            }
            DueDate = newDue;
            return true;
        }

        internal bool EditTaskTitle(string newTitle)
        {
            if(this.Status.ToString().Equals("Done"))
            {
                return false;
            }
            Title = newTitle;
            return true;
        }
        internal void ChangeStatus(int Status)
        {
            this.Status = Status;
        }

        public override string ToString()
        {
            return  "Task title: " + Title + "\nTask Due Date: " + "\nTask ID: " + Id + "\nTask Description: " + 
                "\nCreated in: " + CreationTime.ToString();
        }
    }
}
