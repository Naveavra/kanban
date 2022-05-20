using IntroSE.Kanban.Backend.ServiceLayer;
using IntroSE.Kanban.Backend.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    internal class BoardControl
    {
        int BoardCounter; //this counter will give every new board his id
        int TaskCounter;
        log4net.ILog logger = Log.GetLogger();
        Validator validator = Validator.Instance; //dan
        Dictionary<string, List<Board>> boards; //borads pool <user email, List<boards>>

        public BoardControl(){
            boards = new Dictionary<string, List<Board>>();
            }

/*        private static BoardControl instance = null;
*/
/*        public static BoardControl Instance { get { return instance ?? (instance = new BoardControl()); } }
*/        public Response AddBoard(string email, string boardName) 
        {   
            if(String.IsNullOrWhiteSpace(boardName) || String.IsNullOrEmpty(boardName))
            {
                logger.Warn("Cannot add an invalid board name");
                return new Response("Cannot add an invalid board name",true);
            }
            boardName = boardName.ToLower();
            if (!boards.ContainsKey(email))
            {
                boards.Add(email, new List<Board>());
            }
            if (GetBoard(email, boardName).ErrorOccured() || boards[email].Count == 0 )
            {
                Board board = new Board(email, boardName);
                boards[email].Add(board);
                logger.Info("Board added succesfully");
                return new Response("Succesfully added new board");
            }
            else 
            { 
                logger.Warn("A board with tha name already exists");
                return new Response("Board with this name already exists", true);
            }

            /*            logger.Warn("Cannot add board");
            */
            return new Response("user doest registered", true);
        }

        //is there a problem having two tasks with the same name?

        public Response GetBoard(string email,string boardname) //new function uses reponse to let us know if the board exists
        {
            boardname = boardname.ToLower();
            try
            {
                foreach (Board b in boards[email])
                {
                    if (b.GetName() == boardname)
                    {
                        return new Response("Success");
                    }
                }
/*                logger.Warn("Board: " + boardname + "doesn't exist.");
*/                throw new Exception("Board doesn't exist");
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, true);
            }
        }

        internal Response GetColumnForReal(string email, string boardName, int columnOrdinal)
        {
            return GetBoardForReal(email, boardName).GetColumnForReal(columnOrdinal);
        }

        internal Response GetColumn(string email, string boardName, int columnOrdinal)
        {
            if (!GetBoard(email, boardName).ErrorOccured())
            {
                return GetBoardForReal(email, boardName).GetColumn(columnOrdinal);
            }
            else
            {
                return GetBoard(email, boardName);
            }
        }
        public Board GetBoardForReal(string email, string boardname) //this function allready knows the board exist and just returns it
        {
            boardname=boardname.ToLower();
            foreach (Board b in boards[email])
            {
                if (b.GetName() == boardname)
                {
                    return b;
                }
            }
            return null; // this optin not possible 

        }

        public int GetandIncrement() //newfunction
        {
            int previd = TaskCounter;
            TaskCounter++;
            return previd;
        }

        public Response AddTask(string email, string boardname, string title, string desc, DateTime duedate)
        {
            if (!(validator.ValidateTaskTitle(title)) || !(validator.ValidateTaskDesc(desc)))
            {
                return new Response("Not a valid input for the task", true);
            }
            if(!GetBoard(email, boardname).ErrorOccured())
            {
                return GetBoardForReal(email, boardname).AddTask(title, desc, duedate, GetandIncrement());
            }
            else
            {
                return GetBoard(email, boardname);
            }
      
        }

        internal Response AdvanceTask(string email, string boardName, int taskId)
        {
            if (!GetBoard(email, boardName).ErrorOccured())
            {
                return GetBoardForReal(email, boardName).AdvanceTask(taskId);
            }
            return GetBoard(email, boardName);
        }

        public bool UserBoardsExist(string email, string boardname)
        {
            bool exist = false;
            if (boards.ContainsKey(email))
            {
                foreach(Board b in boards[email])
                {
                    if (b.GetName() == boardname)
                    {
                        exist = true;
                    }
                }
            }
            return exist;
        }
        /// <summary>
        /// this function will go all the way just to make suer everything is ok then we will call the real get column
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Response GetInProgressChecker(string email) 
        {
            if (!boards.ContainsKey(email))
            {
                logger.Warn("User doesn't have a board with that name yet");
                return new Response("User doesnt have any boards yet", true);
            }
            else
            {
                return new Response("{}");
            }
        }

        public Response GetColumnsByName(string email, string ColumnName)
        {
            if (ColumnName.Equals("BackLog"))
            {
                return GetColumnForAllBoards(email, 0);
            }
            else if (ColumnName.Equals("InProgress"))
            {
                return GetColumnForAllBoards(email, 1);
            }
            else if(ColumnName.Equals("IsDone"))
            {
                return GetColumnForAllBoards(email, 2);
            }
            return new Response(new List<Task>());

        }

        private Response GetColumnForAllBoards(string email, int coulumnID)
        {
            List<Task> result = new List<Task>();
            foreach (Board b in boards[email])
            {
                foreach(Task t in b.GetColumnTaskList(coulumnID))
                {
                    result.Add(t);
                }
            }
            return new Response(result);
        }

        public string GetDone(string email, string BoardName)
        {
            string res = "";
            foreach (Board b in boards[email])
            {
                res += b.GetDone();
            }
            return res;

        }

/*        public string ToInProgress(string email, string boardname, int taskID)
        {
            return GetBoard(email, boardname).ToInProgress(taskID);
        }

        public string ToIsDone(string email, string boardname, int taskID)
        {
            return GetBoard(email, boardname).ToIsDone(taskID);
        }

        public string LimitColumn(string email, string boardName, int columnIDX, int limit)
        {
            return GetBoard(email, boardName).LimitColumn(columnIDX, limit);
        }

        public string RenameColumn(string email, string boardName, int columnIDX, string newName)
        {
            return GetBoard(email, boardName).RenameColumn(columnIDX, newName);
        }
*/
        public Response UpdateTaskDue(string email, string boardName, int taskID, DateTime newDue)
        {
            if (GetBoard(email, boardName).ErrorOccured())
            {
                return GetBoard(email, boardName);
            }
            return GetBoardForReal(email, boardName).UpdateTaskDue(taskID, newDue);
        }

        public Response UpdateTaskDesc(string email, string boardName, int taskID, string newDesc)
        {
            if (validator.ValidateTaskDesc(newDesc))
            {
                if (!GetBoard(email, boardName).ErrorOccured())
                {
                    return GetBoardForReal(email, boardName).UpdateTaskDesc(taskID, newDesc);
                }
                return GetBoard(email, boardName);
            }
            logger.Warn(" New Description is Invalid");
            return new Response("Description is too long", true);
        }

        public Response UpdateTaskTitle(string email, string boardName, int taskID, string newTitle)
        {
            if (validator.ValidateTaskTitle(newTitle))
            {
                if (GetBoard(email, boardName).ErrorOccured())
                {
                    return GetBoard(email, boardName);
                }
                return GetBoardForReal(email, boardName).UpdateTaskTitle(taskID, newTitle);
            }
            logger.Warn("new Task title isn't Valid");
            return new Response("Task title isnt valid", true);
        }

        public Response RemoveBoard(string email, string BoardName)
        {
            try
            {
                if (!GetBoard(email, BoardName).ErrorOccured())
                {
                    boards[email].Remove(GetBoardForReal(email, BoardName));
                    logger.Info("Board removed successfully");
                    return new Response("Board Removed");
                }
                else
                {
                    logger.Warn("Board doesn't exist");
                    return GetBoard(email, BoardName);
                }
            }catch (Exception ex)
            {
                return new Response(ex.Message, true);
            }
         }

        public string GetBoardsNames(string email)
        {
            string res = "";
            if (boards.ContainsKey(email))
            {
                foreach (Board board in boards[email])
                {
                    int counter = 1;
                    res = counter.ToString() + ".-\n" + board.GetName() +"\n";
                    counter++;
                }
                if (res.Length == 0)
                {
                    return "User doesnt have any boards yet";
                }
                else
                {
                    return res;
                }
            }
                return "User doesnt exist in our system";
            }

        public Response LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            if (GetBoard(email, boardName).ErrorOccured())
            {
                return new Response("Board doesnt exist", true);
            }
            else
            {
                Response r = GetBoardForReal(email, boardName).LimitColumn(columnOrdinal, limit);
                if (r.ErrorOccured())
                {
                    logger.Warn("Cannot limit column");
                }
                else logger.Info("Successfully Limited column");
                return r;
            }
        }

        public Response GetCloumnLimit(string email, string boardName, int columnOrdinal)
        {
            if (GetBoard(email, boardName).ErrorOccured())
            {
                return new Response("Board doesnt exist", true);
            }
            else
            { 
                return GetBoardForReal(email, boardName).GetColumnLimit(columnOrdinal);
            }

        }

        internal Response GetColumnName(string email, string boardName, int columnOrdinal)
        {
            if (GetBoard(email, boardName).ErrorOccured())
            {
                return new Response("Board doesnt exist", true);
            }
            else
            {
                return GetBoardForReal(email, boardName).GetColumnName(columnOrdinal);
            }
        }
    }
}
