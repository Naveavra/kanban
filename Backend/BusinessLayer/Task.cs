using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    internal class Task
    {
        private string title;
        private string description;
        //private string currentBoard;
        private int taskID;
        private int Status;
        private string dueDate;
        private DateTime creationDate;

        public Task(string title, string description, string dueDate,int taskID)
        {
            this.title = title;
            this.description = description;
            this.dueDate = dueDate;
            //this.currentBoard = currentBoard;
            this.taskID = taskID;
            creationDate = DateTime.Now.Date;

        }
        public int gettaskID(){return this.taskID; }
        public string gettitle() { return this.title; }
        public string getDescription() { return this.description; } 
        //public string getCurrentBoard() { return this.currentBoard; }   
        public string getdueDate() { return this.dueDate; }
        public string getCreationTime() { return this.creationDate.ToString();  ; }
        public override string ToString()
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
            
        }

        public bool EditTaskDesc(string newDesc)
        {
            if(this.Status.ToString().Equals("Done"))
            {
                return false;
            }
            description = newDesc;
            return true;
        }

        internal bool EditTaskDueDate(string newDue)
        {
            if (this.Status.ToString().Equals("Done"))
            {
                return false;
            }
            dueDate = newDue;
            return true;
        }

        internal bool EditTaskTitle(string newTitle)
        {
            if(this.Status.ToString().Equals("Done"))
            {
                return false;
            }
            title = newTitle;
            return true;
        }
        internal void ChangeStatus(int Status)
        {
            this.Status = Status;
        }
    }
}
