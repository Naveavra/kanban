using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    interface BoardService
    {
        /// <summary>
        /// 
        /// </summary> Method that will add a task to a certain board
        /// <param name="email">the user email </param>
        /// <param name="boradname">the board name that the user would like to add task to</param>
        /// <param name="title">the task's title.</param>
        /// <param name="desc">Description of the task.</param>
        /// <param name="dueDate">the task's due date</param>
        /// <returns>The string "successfully added the task", unless an error occurs</returns>
        public string AddTask(string email, string boradname, string title,
            string desc, string dueDate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary> method that will get the board
        /// <param name="email">the user's email address</param>
        /// <param name="boardname">The name of the board we trying to get</param>
        /// <returns>response -we will use the toString method of the board to represent the board using string 
        /// ,unlees an error occures.</returns>
        public string GetBoard(string email, string boardname)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// this method will return all the tasks in the InProgress column of the board
        /// </summary>
        /// <param name="email">the user's email address.</param>
        /// <param name="boradname">the borad's name he is intresting to get the InProgress column from.</param>
        /// <returns>response -string that represents all the tasks in that column, unless an error occurs.</returns>
        public string GetInProgress(string email, string boradname)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///this method will return all the tasks in the Done column of the board
        /// </summary>
        /// <param name="email">the user's email address.</param>
        /// <param name="boardname">the borad's name he is intresting to get the InProgress column from.</param>
        /// <returns></returns>response -string that represents all the tasks in that column, unless an error occurs.</returns>

        public string GetDone(string email, string boardname)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// moves a specific task from the backlog column to the InProgress column
        /// </summary>
        /// <param name="email">the user's email address.</param>
        /// <param name="BoardName">the board's name</param>
        /// <param name="taskID">the task's id</param>
        /// <returns>response - string with the value "action succeeded",  unless an error occures.</returns>
        public string ToInProgress(string email, string BoardName, int taskID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// moves a specific task to the IsDone column.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardname"></param>
        /// <param name="taskID"></param>
        /// <returns>>response - string with the value "action succeeded",  unless an error occures.</returns>
        public string ToIsDone(string email, string boardname, int taskID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// this method will get the task
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardname"></param>
        /// <param name="taskID"></param>
        /// <returns>response - string representaion of the task (toString), unless an error occures.</returns>
        public string GetTask(string email, string boardname, int taskID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// This method will change the task's name.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardname"></param>
        /// <param name="taskID"></param>
        /// <param name="newname">the new name the user wants for the task</param>
        /// <returns>The string "successfully renamed", unless an error occurs</returns>
        public string RenameColumn(string email, string boardname, int taskID, string newname)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// this method will determine the max number of tasks this column can contain 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardname"></param>
        /// <param name="taskID"></param>
        /// <param name="limit">max tasks for this column</param>
        /// <returns>The string "successfully limited the column", unless an error occurs</returns>
        public string LimitColumn(string email, string boardname, int taskID, int limit)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// update the due date of the task
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <param name="taskID"></param>
        /// <param name="newDate"></param>
        /// <returns>The string "successfully updated", unless an error occurs</returns>

        public string UpdateTaskDue(string email, string boardName, int taskID, string newDate)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// updating the task description
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <param name="taskID"></param>
        /// <param name="newDate"></param>
        /// <returns>The string "successfully updated", unless an error occurs.</returns>
        public string UpdateTaskDesc(string email, string boardName, int taskID, string newDate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// this method will update the task title
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <param name="taskID"></param>
        /// <param name="newTitle"></param>
        /// <returns>The string "successfully updated", unless an error occurs.</returns>

        public string UpdateTaskTitle(string email, string boardName, int taskID, string newTitle)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// this method will remove a user's board
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <returns>The string "board removed successfully" unless an error ocuures.</returns>
        public string RemoveBoard(string email, string boardName)
        {
            throw new NotImplementedException();

        }

        /// <summary>
        /// this method will return all the user's boards names
        /// </summary>
        /// <param name="email"></param>
        /// <returns>String that contains all of the boards's names unless an error occures.</returns>
        public string GetBoardNames(string email)
        {
            throw new NotImplementedException();
        }

    }
}
