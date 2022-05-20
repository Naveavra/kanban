using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    internal class Board
    {
        log4net.ILog logger = Log.GetLogger();
        string name;
        List<Column> columns;
        string boardowner;
        int taskID; //task counter per board hold the index of the last task we added

        public Board(string boardowner, string boardname)
        {
            this.boardowner = boardowner;
            this.name = boardname;
            this.taskID = 0;
            this.columns = new List<Column>();
            Column backlog = new Column("backlog");
            Column inProgress = new Column("in progress");
            Column done = new Column("done");
            columns.Add(backlog);
            columns.Add(inProgress);    
            columns.Add(done);


            // column[0]=backlog, [1]=inprogress, [2]=isdone

        }


        public Response AddTask(string title, string desc, DateTime dueDate, int ID) //we allready passs the validator stage in the boardservice 
        {
            Task task = new Task(title, desc, dueDate, ID);
            task.ChangeStatus(1);
            return columns[0].AddTask(task);
        }

/*        public string ToInProgress(int taskID)
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
        }*/

        public Response LimitColumn(int colomunIDX, int limit) //can we assume the user will not limit the column after 
            //he already added tasks?
        {
            if (columns[colomunIDX].SetColumnLimit(limit))
            {
                return new Response("{}");
            }
            return new Response("column capacity exceeds the new limitation please choose higher limitation", true);
        }

        public string RenameColumn(int columnIDX, string name)
        {
            columns[columnIDX].SetName(name);
            return "success";   
        }

        public Response UpdateTaskDue(int taskID, DateTime newDue)
        {
            for(int i=0; i<2; i++)
            {
                if (columns[i].GetTask(taskID) != null)
                {
                    if (columns[i].GetTask(taskID).EditTaskDueDate(newDue))
                    {
                        logger.Info("Task due date has been set succesfully");
                        return new Response("{}");
                    }
                    
                }
            }
            if (columns[2].GetTask(taskID)!= null)
            {
                logger.Warn("Task is already done.");
                return new Response("Task is already Done", true);
            }
            logger.Warn("Task doesn't exist, Task ID: "+taskID);
            return new Response("Task not found", true);
        }

        internal Response GetColumnForReal(int columnOrdinal)
        {
            List<Task> tasks = new List<Task>();
            foreach(Task task in columns[columnOrdinal].GetTasks().Values)
            {
                tasks.Add(task);
            }
            return new Response(tasks);
        }

        internal Response GetColumn(int columnOrdinal)
        {
            if(columnOrdinal > columns.Count())
            {
                return new Response("Column Index doesnt exist", true);
            }
            if (columns[columnOrdinal].GetTasks().Count == 0)
            {
                return new Response("{}");
            }
            return new Response("{}");

    /*            List<Task> tasks = new List<Task>();
                foreach(Task task in columns[columnOrdinal].GetTasks().Values)
                {
                    tasks.Add(task);
                }
            }*/

        }

        public Response UpdateTaskDesc(int taskID, string newDesc)
        {
            for (int i = 0; i < 2; i++)
            {
                if (columns[i].GetTask(taskID) != null)
                {
                    if (columns[i].GetTask(taskID).EditTaskDesc(newDesc))
                    {
                        logger.Info("Updated Task description.");
                        return new Response("success");
                    }
                }
            }
            if (columns[2].GetTask(taskID)!= null)
            {
            logger.Warn("Cannot edit finished tasks.");
                return new Response("Task is already Done", true);
            }
            return new Response("Task not found", true);
        }
            
            
        

        public Response UpdateTaskTitle(int taskID, string newTitle)
        {
            for (int i = 0; i<2; i++)
            {
                if (columns[i].GetTask(taskID) != null)
                {
                    if (columns[i].GetTask(taskID).EditTaskTitle(newTitle))
                    {
                        logger.Info("Task title edited successfully");
                        return new Response("{}");
                    }
                    else
                    {
                        logger.Warn("Cannot edit finished tasks.");
                        return new Response("task already done", true);
                    }
                }
                if (columns[2].GetTask(taskID) != null)
                {
                    logger.Warn("Task is already Done");
                    return new Response("task is alreay done", true);
                }
            }
            logger.Warn("Task not found");
            return new Response("Task not found", true);
        }

        public Response AdvanceTask(int taskid)
        {
            for(int i = 0; i< 2; i++ )
            {
                if (columns[i].GetTasks().ContainsKey(taskid))
                {
                    Task task = columns[i].GetTasks()[taskid];
                    if (columns[i+1].canAdd()) //What happens if you limit the column and try to advance task?
                    {
                        columns[i].RemoveTask(taskid); // you dont want to delete before you check wether you can add the task or not.
                        logger.Info("Advanced task successfully.");
                        return columns[i + 1].AddTask(task);
                    }
                    logger.Warn("Cannot advance Task, column has reached it's max capacity");
                    return new Response("Cannot advance task", true);
                    }
                }
            if (columns[2].GetTasks().ContainsKey(taskid))
            {
                logger.Warn("Task is already Done");
                return new Response("Task is already done G", true);
            }
            else
            {
                logger.Warn("Task doesn't exist.");
                return new Response("Task isnt found", true);
            }
            }
        

        public string GetInProgress()
        {
            return columns[1].ToString(); //todo coloumn.tostring
        }

        public List<Task> GetColumnTaskList(int coulumnID)
        {
            List<Task> tasks = new List<Task>();
            foreach (Task task in columns[coulumnID].GetTasks().Values)
            {
                tasks.Add(task);
            }
            return tasks;
        }

        public string GetDone()
        {
            return columns[2].ToString();
        }

        public Task GetTask(int taskID, int columnIDX)
        {
            return columns[columnIDX].GetTask(taskID); 
        }

        public string GetName()
        {
            return name;
        }

        internal Response GetColumnLimit(int columnOrdinal)
        {
            try
            {
                int limit = columns[columnOrdinal].getlimit();
                return new Response(limit.ToString());
            }
            catch (Exception ex)
            {
                return new Response("Column Ordinal doesnt exist", true);
            }
        }

        internal Response GetColumnName(int columnOrdinal)
        {
            try
            {
                string name = columns[columnOrdinal].getName();
                return new Response(name);
            }
            catch (Exception ex)
            {
                return new Response("Column Ordinal doesnt exist", true);
            }
        }
    }
}
