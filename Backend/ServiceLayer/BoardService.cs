using IntroSE.Kanban.Backend.BusinessLayer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class BoardService
    {
        BoardControl boardControl;
        /// <summary>
        /// 
        /// </summary> Method that will add a task to a certain board
        /// <param name="email">the user email </param>
        /// <param name="boradname">the board name that the user would like to add task to</param>
        /// <param name="title">the task's title.</param>
        /// <param name="desc">Description of the task.</param>
        /// <param name="dueDate">the task's due date</param>
        /// <returns>Response with user-email, unless an error occurs (see <see cref="GradingService"/>)</returns>
        /// 
        public BoardService()
        {
           boardControl = new BoardControl();
        }

/*        private static BoardService instance = null;
*/
/*        public static BoardService Instance { get { return instance ?? (instance = new BoardService()); } }
*/        public string AddTask(string email, string boradname, string title,string desc, DateTime dueDate)
        {
            Response res = boardControl.AddTask(email, boradname, title, desc, dueDate);
            if (res.ErrorOccured())
            {
                return res.Serialize();
            }
            return new Response(email).Serialize();
            
        }

        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string AddBoard(string email, string name)
        {
            Response response = boardControl.AddBoard(email, name);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return "{}";
        }

        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>

        public string UpdateTaskDueDate(string email, string boardName, int taskID, DateTime newDate)
        {
            Response response = boardControl.UpdateTaskDue(email, boardName, taskID, newDate);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return "{}";
        }


        /// <returns>Response with  a list of the column's tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string GetColumn(string email, string boardName, int columnOrdinal)
        {
            Response response = boardControl.GetColumn(email, boardName, columnOrdinal);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            Response response1 = boardControl.GetColumnForReal(email, boardName, columnOrdinal);
            return response1.Serialize();
        }

        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string UpdateTaskTitle(string email, string boardName, int taskID, string newTitle)
        {
            Response response = boardControl.UpdateTaskTitle(email, boardName, taskID, newTitle);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return "{}";
        }


        /// <summary>
        /// updating the task description
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <param name="taskID"></param>
        /// <param name="newDate"></param>
        ///<returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>

        public string UpdateTaskDesc(string email, string boardName, int taskID, string NewDescription)
        {
            Response response = boardControl.UpdateTaskDesc(email, boardName, taskID, NewDescription);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return "{}";
        }

        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        
        public string AdvanceTask(string email, string boardName, int taskId)
        {
            Response response = boardControl.AdvanceTask(email, boardName, taskId);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return "{}";
        }

        /// <summary>
        /// this method will remove a user's board
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        ///<returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string RemoveBoard(string email, string boardName)
        {
            Response response = boardControl.RemoveBoard(email, boardName);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return "{}";

        }

        /// <summary>
        /// 
        /// </summary> Method that will remove a task to a certain board
        /// <param name="email">the user email </param>
        /// <param name="boradname">the board name that the user would like to remove task to</param>
        /// <param name="title">the task's title.</param>
        /// <param name="desc">Description of the task.</param>
        /// <param name="dueDate">the task's due date</param>
        /// <param name="TaskID">the task's id</param>
        /// <returns>The string "successfully added the task", unless an error occurs</returns>
     /*   public string RemoveTask(string email, string boradname, string title, //T
            string desc, string dueDate,int TaskID)
        {
            throw new NotImplementedException();
        }*/

        /// <summary>
        /// 
        /// </summary> method that will get the board
        /// <param name="email">the user's email address</param>
        /// <param name="boardname">The name of the board we trying to get</param>
        /// <returns>response -we will use the toString method of the board to represent the board using string 
        /// ,unlees an error occures.</returns>
        public string GetBoard(string email, string boardname)
        {
            Response res = boardControl.GetBoard(email, boardname);
            if (res.ErrorOccured())
            {
                return res.Serialize();
            }
            return new Response(email).Serialize();
        }


        /// <returns>Response with  a list of the in progress tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string InProgressTasks(string email)
        {
            Response response = boardControl.GetInProgressChecker(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardControl.GetColumnsByName(email, "InProgress").Serialize();
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


        public string LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            Response response = boardControl.LimitColumn(email, boardName, columnOrdinal, limit);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return "{}";
        }

        /// <returns>Response with column limit value, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            Response response = boardControl.GetCloumnLimit(email, boardName, columnOrdinal);
            return response.Serialize();
        }

        /// <summary>
        /// update the due date of the task
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <param name="taskID"></param>
        /// <param name="newDate"></param>
        /// <returns>The string "successfully updated", unless an error occurs</returns>


        /// <returns>Response with column name value, unless an error occurs (see <see cref="GradingService"/>)</returns>

        public string GetColumnName(string email, string boardName, int columnOrdinal)
        {
            Response response = boardControl.GetColumnName(email, boardName, columnOrdinal);
            return response.Serialize();
        }


        /// <summary>
        /// this method will update the task title
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <param name="taskID"></param>
        /// <param name="newTitle"></param>
        /// <returns>The string "successfully updated", unless an error occurs.</returns>


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

