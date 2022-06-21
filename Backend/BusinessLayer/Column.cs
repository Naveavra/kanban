using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    internal class Column
    {
        log4net.ILog logger = Log.GetLogger();
        private string ColumnName;
        private int limit = -1;
        private Dictionary<int, Task> Tasks;
        private TaskMapper Mapper { get; }

        public Column(string name, int limit = -1)
        {
            this.ColumnName = name;
            this.limit = limit;
            this.Tasks = new Dictionary<int, Task>();
            Mapper = TaskMapper.Instance;
        }

        public int getlimit()
        {
            return limit;
        }
        public bool canAdd()
        {
            if (Tasks.Count < limit || limit == -1)
            {
                return true;
            }
            return false;
        }
        public bool SetColumnLimit(int limit)
        {
            if (limit < Tasks.Count || limit % 1 !=0)
            {
                return false;
            }
            this.limit = limit;
            return true;
        }

        public string getName()
        {
            return ColumnName;
        }

        public void SetName(string name)
        {
            ColumnName = name;
        }

        public void AddTask(Task task,int boardID,string boardOwner)
        {
            if ( Tasks.Count() < limit || limit == -1) //if the limit is -1 we can add all the tasks we want
            {
                if (!Tasks.ContainsKey(task.gettaskID()))
                {
                    Tasks.Add(task.gettaskID(), task);
                    task.setDTO( new TaskDTO(task, boardID, 0, boardOwner));
                    Mapper.addData(task.DTO);
                    logger.Info("Task: "+task.gettitle()+" added Successfully");
                    return;
                }
                logger.Warn("Task ID: "+task.gettaskID()+" Already exists.");
                throw new Exception($"Task ID: {task.gettaskID()} already exists.");                
            }
            else
            {
                logger.Warn("This column has reached its' max capacity. Please get your shit together blood.");
                throw new Exception("This column has reached its' max capacity. Please get your shit together blood.");
            }
        }

        public string RemoveTask(int taskID)
        {
            if (Tasks.ContainsKey(taskID))
            {
                Tasks.Remove(taskID);
                return "Success";
            }
            else
            {
                return "this column doest contain the task you seeking";
            }

        }

        public Task GetTask(int taskID)
        {
            if (Tasks.ContainsKey(taskID))
            {
                return Tasks[taskID];
            }
            else
            {
                return null;
            }
        }

        public Dictionary<int, Task> GetTasks()
        {
            return Tasks;
        }
        
        override
        public string ToString()
        {
            string res = "The tasks in the " + ColumnName + " are:\n";
            foreach (Task task in Tasks.Values)
            {
                res += task.ToString() + "\n"; 
            }
            return res;
        }
        

        



        

    }
}
