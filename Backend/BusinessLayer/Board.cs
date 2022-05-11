using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    internal class Board
    {
        string name;
        List<Column> columns;
        string boardowner;
        int taskID; //task counter per board hold the index of the last task we added


        public Board(string boardowner, string boardname)
        {
            this.boardowner = boardowner;
            this.name = boardname;
            this.taskID = 0;
            this.columns = new List<Column>(); // column[0]=backlog, [1]=inprogress, [2]=isdone

        }

        public string AddTask(string title, string desc, string dueDate, int ID) //we allready passs the validator stage in the boardservice 
        {
            taskID += 1;
            Task task = new Task(title, desc, dueDate, taskID);
            task.ChangeStatus(1);
            return columns[0].AddTask(task);
        }

        public string ToInProgress(int taskID)
        {
            Task task;
            if ( columns[0].GetTask(taskID) != null)
            {
                task = columns[0].GetTask(taskID);
                task.ChangeStatus(2);
                columns[0].RemoveTask(taskID);
                return columns[1].AddTask(task);
            }
            else
            {
                return "task not found";
            }
        }

        public string ToIsDone(int taskID)
        {
            Task task;
            if (columns[1].GetTask(taskID) != null)
            {
                task = columns[1].GetTask(taskID);
                task.ChangeStatus(3);
                columns[1].RemoveTask(taskID);
                return columns[2].AddTask(task);
            }
            else
            {
                return "task not found";
            }
        }

        public string LimitColumn(int colomunIDX, int limit) //can we assume the user will not limit the column after 
            //he already added tasks?
        {
            columns[colomunIDX].SetColumnLimit(limit);
            return "success";
        }

        public string RenameColumn(int columnIDX, string name)
        {
            columns[columnIDX].SetName(name);
            return "success";   
        }

        public string UpdateTaskDue(int taskID, string newDue)
        {
            foreach(Column column in columns)
            {
                if (column.GetTask(taskID) != null)
                {
                    if (column.GetTask(taskID).EditTaskDueDate(newDue))
                    {
                        return "success";
                    }
                    else
                    {
                        return "Task is allready Done";
                    }
                }
            }
            return "Task not found";
        }

        public string UpdateTaskDesc(int taskID, string newDesc)
        {
            foreach (Column column in columns)
            {
                if (column.GetTask(taskID) != null)
                {
                    if (column.GetTask(taskID).EditTaskDesc(newDesc))
                    {
                        return "success";
                    }
                    else
                    {
                        return "Task is allready Done";
                    }
                }
            }
            return "Task not found";
        }

        public string UpdateTaskTitle(int taskID, string newTitle)
        {
            foreach (Column column in columns)
            {
                if (column.GetTask(taskID) != null)
                {
                    if (column.GetTask(taskID).EditTaskTitle(newTitle))
                    {
                        return "success";
                    }
                    else
                    {
                        return "Task is allready Done";
                    }
                }
            }
            return "Task not found";
        }
         public string GetInProgress()
        {
            return columns[1].ToString(); //todo coloumn.tostring
        }

        public Task GetTask(int taskID, int columnIDX)
        {
            return columns[columnIDX].GetTask(taskID); 
        }





    }
}
