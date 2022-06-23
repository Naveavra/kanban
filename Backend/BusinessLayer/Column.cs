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

        ///
        public int getlimit()
        {
            return limit;
        }
        /// <summary>
        /// checks f its possible to add new task or we already reached the column limit
        /// </summary>
        /// <returns></returns> true if we can add otherwise false
        public bool canAdd()
        {
            if (Tasks.Count < limit || limit == -1)
            {
                return true;
            }
            return false;
        }
        ///
        public bool SetColumnLimit(int limit)
        {
            if (limit < Tasks.Count || limit % 1 !=0)
            {
                return false;
            }
            this.limit = limit;
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getName()
        {
            return ColumnName;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public void SetName(string name)
        {
            ColumnName = name;
        }
        /// <summary>
        /// adds new task to the backlog column in the board
        /// </summary>
        /// <param name="task"></param>
        /// <param name="boardID"></param>
        /// <param name="boardOwner"></param> the email of the board owner
        /// <exception cref="Exception"></exception> throws exception  if we cand add or somehing wrong the the params
        public void AddTask(Task task,int boardID,string boardOwner)
        {
            if ( Tasks.Count() < limit || limit == -1) //if the limit is -1 we can add all the tasks we want
            {
                if (!Tasks.ContainsKey(task.gettaskID()))
                {
                    Tasks.Add(task.gettaskID(), task);
                    task.setDTO( new TaskDTO(task, boardID, 0, boardOwner));
                    Mapper.addData(task.DTO);
                    return;
                }
                throw new Exception($"Task ID: {task.gettaskID()} already exists.");                
            }
            else
            {
                throw new Exception("This column has reached its' max capacity. Please get your shit together blood.");
            }
        }
        /// <summary>
        /// removes the task
        /// </summary>
        /// <param name="taskID"></param>
        /// <returns></returns>
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
        ///
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
        /// <summary>
        /// getter
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Task> GetTasks()
        {
            return Tasks;
        }
        
        ///
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
