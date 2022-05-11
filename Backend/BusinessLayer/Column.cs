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

        public Column(string name, int limit = -1)
        {
            this.ColumnName = name;
            this.limit = limit;
            this.Tasks = new Dictionary<int, Task>();
        }

        public int getlimit()
        {
            return limit;
        }

        public void SetColumnLimit(int limit)
        {
            this.limit = limit;
        }

        public string getName()
        {
            return ColumnName;
        }

        public void SetName(string name)
        {
            ColumnName = name;
        }

        public string AddTask(Task task)
        {
            if ( Tasks.Count() < limit || limit < 0) //if the limit is -1 we can add all the tasks we want
            {
                Tasks.Add(task.gettaskID(), task);
                return "Success";
            }
            else
            {
                return "this column allready reached max capacity. Please get your shit together blood";
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
            string res = "";
            foreach (Task task in Tasks.Values)
            {
                res += task.ToString() + "\n"; 
            }
            return res;
        }
        

        



        

    }
}
