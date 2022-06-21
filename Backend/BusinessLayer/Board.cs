﻿using IntroSE.Kanban.Backend.DataAccessLayer;
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
    internal class Board
    {
        int boardID;
        log4net.ILog logger = Log.GetLogger();
        public string name { get; set; }
        public List<Column> columns { get; set; }
        public string boardowner { get; set; }
        public ColumnMapper Mapper { get; }
        public int taskID{ get; set; } //task counter per board hold the index of the last task we added

        public Board(string boardowner, string boardname,int boardID)
        {
            this.boardID = boardID;
            Mapper = ColumnMapper.Instance;
            this.boardowner = boardowner;
            this.name = boardname;
            this.taskID = 0;
            this.columns = new List<Column>();
            Column backlog = new Column("backlog");
            Column inProgress = new Column("in progress");
            Column done = new Column("done");
            Mapper.addData(new ColumnDTO(backlog,0,boardID));
            Mapper.addData(new ColumnDTO(inProgress,1,boardID));
            Mapper.addData(new ColumnDTO(done,2,boardID));
            columns.Add(backlog);
            columns.Add(inProgress);    
            columns.Add(done);


            // column[0]=backlog, [1]=inprogress, [2]=isdone

        }
        public List<Task> getAssignedInProgress(string email)
        {
            List<Task> assigned = new List<Task>();
            foreach(Task task in columns[1].GetTasks().Values)
            {
                if(task.Assignee == email)  
                    assigned.Add(task);
            }
            return assigned;
        }

        
        public void AddTask(string title, string desc, DateTime dueDate) //we allready passs the validator stage in the boardservice 
        {
            Task task = new Task(title, desc, dueDate, GetAndIncrement());
            task.ChangeStatus(1);
            try
            {
                columns[0].AddTask(task, this.boardID, this.boardowner);
            }catch(Exception ex)
            {
                GetAndDecrement();
                throw new Exception(ex.Message);
            }
        }
        public int GetAndIncrement() //newfunction
        {
            int previd = taskID;
            taskID++;
            BoardMapper.Instance.Update(boardID,"taskID",taskID);
            return previd;
        }
        public int GetAndDecrement() //newfunction
        {
            int previd = taskID;
            taskID--;
            BoardMapper.Instance.Update(boardID, "taskID", taskID);
            return previd;
        }
        internal Response AssignTask(string email,int columnOrdinal, int taskId, string emailAssignee)
        {
            Task task = columns[columnOrdinal].GetTask(taskId);
            if(task != null)
            {
                if (task.Assignee == email || task.Assignee == "")
                {
                    task.setAssignee(emailAssignee);
                    return new Response("{}");
                }
                return new Response("Assign failed, can only assign by the assignee",true);
            }
            return new Response("No such Task",true);
        }
        internal void unAssign(string email)
        {
            int i = 0;
            while (i < columns.Count-1)
            {
                foreach (Task task in columns[i].GetTasks().Values)
                {
                    if (task.Assignee == email)
                    {
                        task.setAssignee("");
                    }
                }
                i++;
            }

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

        public void LimitColumn(int colomunIDX, int limit) //can we assume the user will not limit the column 
        {
            if (!columns[colomunIDX].SetColumnLimit(limit))
            {
                logger.Warn("Cannot limit column");
                throw new Exception("column capacity exceeds the new limitation please choose higher limitation");
            }

        }

        public string RenameColumn(int columnIDX, string name)
        {
            columns[columnIDX].SetName(name);
            return "success";   
        }

        public void UpdateTaskDue(int taskID, int columnOrdinal,string email, DateTime newDue)
        {
            Task task= null;
            if(! (columnOrdinal > columns.Count))
            { 
                task = columns[columnOrdinal].GetTask(taskID);
                if (columnOrdinal == 2)
                {
                    logger.Warn("Task is already done.");
                    throw new Exception("Task is already done G");
                }
                if (task != null)
                {
                    task.EditTaskDueDate(email, newDue);
                    return;
                }

            }
            logger.Warn("Task doesn't exist, Task ID: "+taskID);
            throw new Exception("Task doesn't exist");
        }


        internal Response GetColumn(int columnOrdinal)
        {
            if(columnOrdinal > columns.Count())
            {
                throw new Exception("Column ordinal doesn't exist");
            }
            else
            {
                /*List<Task> tasks = new List<Task>();
                foreach (Task task in )
                {
                    tasks.Add(task);
                }*/
                return new Response(columns[columnOrdinal].GetTasks().Values);
            }

        }

        public void UpdateTaskDesc(int taskID,int columnOrdinal, string email,string newDesc)
        {
            Task task = null;
            if (!(columnOrdinal > columns.Count())) { 
                task = columns[columnOrdinal].GetTask(taskID);
                if (columnOrdinal == 2)
                {
                    logger.Warn("Cannot edit finished tasks.");
                    throw new Exception("Task is already Done G");
                }
                if (task != null)
                {
                    task.EditTaskDesc(email, newDesc);
                    logger.Info("Updated Task description.");
                    return;
                }
                
            }
            logger.Warn("Task not found");
            throw new Exception("Task not found");
        }




        public void UpdateTaskTitle(int taskID, string email, string newTitle, int columnOrdinal)
        {
            Task task = null;
            if (!(columnOrdinal > columns.Count()))
            {
                task = columns[columnOrdinal].GetTask(taskID);
                if (columnOrdinal == 2)
                {
                    logger.Warn("Task is already Done");
                    throw new Exception("Task is already Done G");
                }
                if (task != null)
                {
                    task.EditTaskTitle(email, newTitle);
                    logger.Info("Task title edited successfully");
                    return;
                }
            }
            else
            { 
                logger.Warn("Task not found");
                throw new Exception("Task not found");
            }
        }

        public void AdvanceTask(int taskid,string email)
        {
            for(int i = 0; i< 2; i++ )
            {
                if (columns[i].GetTasks().ContainsKey(taskid))
                {
                    Task task = columns[i].GetTasks()[taskid];
                    if (columns[i+1].canAdd()) //What happens if you limit the column and try to advance task?
                    {
                        if (task.Assignee != email)
                            throw new Exception("Task isn't assigned to this user.");
                        columns[i].RemoveTask(taskid); // you dont want to delete before you check wether you can add the task or not.
                        logger.Info("Advanced task successfully.");
                        TaskMapper.Instance.Update(taskid, this.boardowner, "ordinal", i + 1);
                        columns[i + 1].AddTask(task,this.boardID,this.boardowner);
                        return;
                    }
                    logger.Warn("Cannot advance Task, column has reached it's max capacity");
                    throw new Exception("Cannot advance Task, Column has reached it's max capacity.");
                    }
                }
            if (columns[2].GetTasks().ContainsKey(taskid))
            {
                logger.Warn("Task is already Done");
                throw new Exception("Task is already Done G");
            }
            logger.Warn("Task doesn't exist.");
            throw new Exception("Task doesn't exist.");
        }
        

        public string GetInProgress()
        {
            return columns[1].ToString(); 
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
            if(columnOrdinal>3 || columnOrdinal<0)
                throw new Exception("Invalid column ordinal");
            return new Response(columns[columnOrdinal].getlimit().ToString());            
        }

        internal Response GetColumnName(int columnOrdinal)
        {
            try
            {
                return new Response(columns[columnOrdinal].getName());
            }
            catch (Exception ex)
            {
                throw new Exception("Column ordinal doesn't exist.");
            }
        }

        internal int getID()
        {
            return this.boardID;
        }

        internal void RestoreColumns(List<ColumnDTO> cols)
        {
            foreach(ColumnDTO c in cols)
            {
                Column column = new Column(c.ColumnName, c.limit);
                columns[c.ordinal] = column;
            }
        }
        internal void RestoreTasks(List<TaskDTO> tasks)
        {
            foreach(TaskDTO t in tasks)
            {
                Task task = new Task(t.Title, t.Description, t.DueDate, t.Id);
                task.CreationTime = t.CreationTime;
                task.setAssignee(t.Assignee);
                columns[t.ordinal].AddTask(task,this.boardID,this.boardowner); 
            }
           
                
            
        }
    }
}
