using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer;
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

        public void getReady() //for testing purposes only
        {
            DBConnector.Instance.RemoveTables();
        }

        /*        private static BoardService instance = null;
        */
        /*        public static BoardService Instance { get { return instance ?? (instance = new BoardService()); } }
        */
        public string AddTask(string email, string boradname, string title,string desc, DateTime dueDate)
        {
            try
            {
                boardControl.AddTask(email, boradname, title, desc, dueDate);
                return new Response().Serialize();
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, true).Serialize();
            }
            
        }

        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string AddBoard(string email, string name)
        {
            try
            {
                boardControl.AddBoard(email, name);
                /*                return new Response("").Serialize();
                */
                return new Response().Serialize();
            }
            catch (Exception ex)
            {
                return new Response(ex.Message,true).Serialize();
            }
            
        }

        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>

        public string UpdateTaskDueDate(string email, string boardName ,int taskID, int columnOrdinal, DateTime newDate)
        {
            try
            {
                boardControl.UpdateTaskDue(email, boardName, taskID,columnOrdinal, newDate);
                return new Response().Serialize();
            }
            catch(Exception ex)
            {
                return new Response(ex.Message,true).Serialize();
            }
        }


        /// <returns>Response with  a list of the column's tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string GetColumn(string email, string boardName, int columnOrdinal)
        {
            try
            {
                return boardControl.GetColumn(email, boardName, columnOrdinal).Serialize();
            }
            catch(Exception e)
            {
                return new Response(e.Message,true).Serialize();
            }
            
        }

        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string UpdateTaskTitle(string email, string boardName,int columnOrdinal, int taskID, string newTitle)
        {

            try
            {
                boardControl.UpdateTaskTitle(email, boardName,columnOrdinal, taskID, newTitle);
                return new Response().Serialize();
            }
            catch (Exception e)
            {
                return new Response(e.Message, true).Serialize();
            }
            
        }


        /// <summary>
        /// updating the task description
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <param name="taskID"></param>
        /// <param name="newDate"></param>
        ///<returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>

        public string UpdateTaskDesc(string email, string boardName,int columnOrdinal, int taskID, string NewDescription)
        {
            try
            {
                boardControl.UpdateTaskDesc(email, boardName,columnOrdinal, taskID, NewDescription);
                return new Response().Serialize();
            }
            catch(Exception ex)
            {
                return new Response(ex.Message, true).Serialize();
            }
            
        }

        /// <returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        
        public string AdvanceTask(string email, string boardName, int taskId)
        {
            try
            {
                boardControl.AdvanceTask(email, boardName, taskId);
                return new Response().Serialize();
            }
            catch(Exception e)
            {
                return new Response(e.Message, true).Serialize();
            }
            
        }

        /// <summary>
        /// this method will remove a user's board
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        ///<returns>The string "{}", unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string RemoveBoard(string email, string boardName)
        {
            try
            {
                boardControl.RemoveBoard(email, boardName);
                return new Response().Serialize();
            }
            catch(Exception e)
            {
                return new Response(e.Message,true).Serialize();
            }
            

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
            try
            {
                return boardControl.GetBoard(email, boardname).Serialize();
            }
            catch (Exception ex)
            {
                return new Response(ex.Message,true).Serialize();
            }
            
        }


        /// <returns>Response with  a list of the in progress tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string InProgressTasks(string email)
        {
            try
            {
                return boardControl.GetInProgress(email).Serialize();
            }
            catch(Exception ex)
            {
                return new Response(ex.Message,true).Serialize();
            }


        }
        /// <summary>
        /// this method will return all the tasks in the InProgress column of the board
        /// </summary>
        /// <param name="email">the user's email address.</param>
        /// <param name="boradname">the borad's name he is intresting to get the InProgress column from.</param>
        /// <returns>response -string that represents all the tasks in that column, unless an error occurs.</returns>



        public string LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            try
            {
                boardControl.LimitColumn(email, boardName, columnOrdinal, limit);
                return new Response().Serialize();
            }
            catch(Exception e)
            {
                return new Response(e.Message, true).Serialize();
            }
            
        }

        /// <returns>Response with column limit value, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            try
            {
                return boardControl.GetCloumnLimit(email, boardName, columnOrdinal).Serialize();
            }
            catch(Exception ex)
            {
                return new Response(ex.Message).Serialize();
            }
            
        }

        public string DeleteData()
        {
            return boardControl.DeleteData().Serialize() ;
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
            try
            {
                return boardControl.GetColumnName(email, boardName, columnOrdinal).Serialize();
            }
            catch (Exception ex)
            {
                return new Response(ex.Message).Serialize();
            }
        }


        ///<summary>This method loads all persisted data.
        ///<para>
        ///<b>IMPORTANT:</b> When starting the system via the GradingService - do not load the data automatically, only through this method. 
        ///In some cases we will call LoadData when the program starts and in other cases we will call DeleteData. Make sure you support both options.
        ///</para>
        /// </summary>
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string LoadData()
        {
            try
            {
                boardControl.LoadData();
                return new Response().Serialize();
            }
            catch(Exception e)
            {
                return new Response(e.Message,true).Serialize();
            }
        }
        /// <summary>
        /// This method adds a user as member to an existing board.
        /// </summary>
        /// <param name="email">The email of the user that joins the board. Must be logged in</param>
        /// <param name="boardID">The board's ID</param>
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string JoinBoard(string email, int boardID)
        {
            try
            {
                boardControl.JoinBoard(email, boardID).Serialize();
                return new Response(null).Serialize();
            }
            catch(Exception e)
            {
                return new Response(e.Message, true).Serialize();
            }
        }

        /// <summary>
        /// This method removes a user from the members list of a board.
        /// </summary>
        /// <param name="email">The email of the user. Must be logged in</param>
        /// <param name="boardID">The board's ID</param>
        /// <returns>An empty response, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string LeaveBoard(string email, int boardID)
        {
            try
            {
                return boardControl.LeaveBoard(email, boardID).Serialize();
            }
            catch (Exception ex) { return new Response(ex.Message, true).Serialize(); }

        }

        public string AssignTask(string email, string boardName, int columnOrdinal, int taskID, string emailAssignee)
        {

            try
            {
                return boardControl.AssignTask(email, boardName, columnOrdinal, taskID, emailAssignee).Serialize();
            }
            catch(Exception ex) { return new Response(ex.Message,true).Serialize(); }
        }
        /// <summary>
        /// this method will return all the user's boards names
        /// </summary>
        /// <param name="email"></param>
        /// <returns>String that contains all of the boards's names unless an error occures.</returns>
        public string GetBoardIDs(string email)
        {
            try
            {
                return boardControl.GetBoardsIDs(email).Serialize();
            }
            catch (Exception ex) { return new Response(ex.Message, true).Serialize(); }

        }

        public string TransferOwnership(string currentOwnerEmail, string newOwnerEmail, string boardName)
        {
            try
            {
                return boardControl.changeOwnership(currentOwnerEmail, boardName, newOwnerEmail).Serialize();
            }
            catch (Exception ex) { return new Response(ex.Message, true).Serialize(); }

        }

    }


}

