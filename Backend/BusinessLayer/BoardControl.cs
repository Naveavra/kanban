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
    internal class BoardControl
    {
        public int BoardCounter; //this counter will give every new board his id
        Validator validator = Validator.Instance; 
        Dictionary<string, List<Board>> boards; //borads pool <name, Board>
        Dictionary<Board, List<string>> boardUsers;
        private ColumnMapper columnMapper{get;}
        private BoardMapper boardMapper { get; }
        public BoardControl(){
            boards = new Dictionary<string, List<Board>>();
            boardUsers = new Dictionary<Board, List<string>>();
            columnMapper = ColumnMapper.Instance;
            boardMapper = BoardMapper.Instance;
            BoardCounter = 0;
         /*   TaskMapper.loadData();
            columnMapper.loadData();
            boardMapper.loadData();*/
            }
        /// <summary>
        /// checks if the user have other boards with the same name
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <returns></returns> true if he have otherwise not
        public bool isUniqueBoardName(string email,string boardName)
        {
            try 
            { 
                GetBoard(email, boardName); 
                return false;
            }catch (Exception ex)
            {
                return true;
            }
            
        }
        /// <summary>
        /// adds new board for the user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <exception cref="Exception"></exception>throws expection if the user have a board with the same name
        public void AddBoard(string email, string boardName){   
        if(String.IsNullOrWhiteSpace(boardName) || String.IsNullOrEmpty(boardName))
        {
            throw new Exception("Invalid Board Name");
        }
        
            if (isUniqueBoardName(email,boardName) || boards[email].Count == 0 /*|| boardMapper.Contain(email, boardName)*/)
            {
                Board board = new Board(email, boardName, GetAndIncrement());
                if (!boards.ContainsKey(email))
                {
                    boards.Add(email, new List<Board>());
                }
            boards[email].Add(board);
            if (!boardUsers.ContainsKey(board))
            {
                boardUsers.Add(board, new List<string>());
            }
            boardUsers[board].Add(email);
            boardMapper.addData(new BoardDTO(board));
            boardMapper.addBoardUser(new BoardUserDTO(board.getID(), email)); 
        }
        else 
        { 
            throw new Exception("Board with this name already exists");
        }
    }
        
        internal int GetAndIncrement()
        {
            int prev;
            if (!(BoardCounter == 0))
            {
                BoardCounter = boardMapper.GetLastID();
                prev = BoardCounter;
                BoardCounter++;
                return prev;
            }
            prev = BoardCounter;
            BoardCounter++;
            return prev;

        }
        /// <summary>
        /// assign the task for some bord memeber, if its unassigned it can be dobe by anyone
        ///other wise it can be done only by the assignee
        /// </summary>
        /// <param name="email"></param> the user that wants to assign
        /// <param name="boardName"></param>
        /// <param name="columnOrdinal"></param>
        /// <param name="taskId"></param>
        /// <param name="emailAssignee"></param> the assignee
        /// <returns></returns>
        /// <exception cref="Exception"></exception> if the user isnt a board member or the  user try to assigned can do it(see above)
        public Response AssignTask(string email,string boardName, int columnOrdinal, int taskId, string emailAssignee)
        {
            if (validator.ValidateEmailUsingRegex(emailAssignee))
            {
                Response r = GetBoard(email, boardName);
                if (!r.ErrorOccured())
                {
                    Board b = r.ReturnValue as Board;
                    if (!boardUsers[b].Contains(emailAssignee))
                    {
                        throw new Exception("The user has not yet joined the board");
                    }
                    
                    return b.AssignTask(email,columnOrdinal, taskId, emailAssignee);
                }
                return r;
            }
            throw new Exception("Assigned Email doesn't exist");
        }
        public Response GetBoard(string email,string boardname) //new function uses reponse to let us know if the board exists
        {
            //LoadData();
            if (!boards.ContainsKey(email))
                throw new Exception("User doesn't have boards yet");
           
            foreach (Board b in boards[email])
            {
                if (b.GetName() == boardname)
                {
                    return new Response(b);
                }
            }
            
            throw new Exception($"Board: {boardname}, doesn't exist.");   
        }
        /*public bool isOwner(string boardName, string email)
        {
            bool res = false;
            foreach(Board b in boards[email])
            {
                if(b.name == boardName)
                {
                    res = true;
                }
            }
            return res;
        }
        public string FindOwner(string boardName,string email)
        {
            foreach(Board b in boardUsers.Keys)
            {
                if (boardUsers[b].Contains(email)) 
            }
            return null;
        }*/
        public Response getBoardByID(int id)
        {
            foreach (string s in boards.Keys)
            {
                foreach (Board b in boards[s])
                {
                    if (b.getID() == id)
                    {
                        return new Response(b);
                    }
                }
            }
            throw new Exception("Board doesnt exist");
        }
/*        internal Response GetColumnForRel(string email, string boardName, int columnOrdinal)
        {
            Board b = GetBoard(email, boardName).ReturnValue as Board;
            return GetBoard(email, boardName).GetColumnForReal(columnOrdinal);
        }*/
        /// <summary>
        /// gets all the tasks in the coumn
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <param name="columnOrdinal"></param>
        /// <returns></returns> response with a list off al the column's tasks
        internal Response GetColumn(string email, string boardName, int columnOrdinal)
        {
            Response r = GetBoard(email, boardName);
            if (!r.ErrorOccured())
            {
                Board b = r.ReturnValue as Board;
                return b.GetColumn(columnOrdinal);
            }
            else
            {
                return r;
            }
        }
/*        public Board GetBoardForRel(string email, string boardname) //this function allready knows the board exist and just returns it
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

        }*/

       
        /// <summary>
        /// adds new task
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardname"></param>
        /// <param name="title"></param>
        /// <param name="desc"></param>
        /// <param name="duedate"></param>
        /// <exception cref="Exception"></exception>
        public void AddTask(string email, string boardname,string title, string desc, DateTime duedate)
        {

            validator.ValidateTaskTitle(title);
            validator.ValidateTaskDesc(desc);
            Response r = GetBoard(email, boardname);
            if(!r.ErrorOccured())
            {
                Board b = r.ReturnValue as Board;
                b.AddTask(title, desc, duedate);
            }
            else
            {
                throw new Exception(r.ErrorMessage);
            }
      
        }
        /// <summary>
        /// advance the task to next column unsless its already done
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <param name="taskId"></param>
        internal void AdvanceTask(string email, string boardName,int columnOrdinal, int taskId)
        {
            Response r = GetBoard(email, boardName);
            if (!r.ErrorOccured())
            {
                Board b = r.ReturnValue as Board;
                b.AdvanceTask(taskId,columnOrdinal,email);
            }
        }

/*        public bool UserBoardsExist(string email, string boardname)
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
        }*/
        /// <summary>
        /// this function will go all the way just to make suer everything is ok then we will call the real get column
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
 /*       public void GetInProgressChecker(string email) 
        {
            if (!boards.ContainsKey(email))
            {
                logger.Warn("User doesn't have a board with that name yet");
                throw new Exception("User doesn't have a board with this name yet.");
            }

        }*/

        
        
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="coulumnID"></param>
        /// <returns></returns>
        private Response GetColumnForAllBoards(string email, int coulumnID)
        {
            //TODO CHECK WHY CREATING LIST TWICE
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

        internal Response GetInProgress(string email)
        {
            List<Task> inprogress = new List<Task>(); 
            if (!boards.ContainsKey(email) || boards[email].Count==0)
            { 
                return new Response(inprogress);
            }
            foreach(Board b in boards[email])
            {
                inprogress.AddRange(b.getAssignedInProgress(email));
                /*foreach(Task task in b.getAssignedInProgress(email)) 
                {
                    inprogress.Add(task);
                }*/
            }
            return new Response(inprogress);
        }

        internal Response DeleteData()
        {
            boards.Clear();
            boardUsers.Clear();
            DBConnector.Instance.RemoveTables();
            BoardCounter = 0;
            return new Response();
        }

        ///updating the task due date unless its done
        public void UpdateTaskDue(string email, string boardName, int taskID, int columnOrdinal, DateTime newDue)
        {
            Response r = GetBoard(email, boardName);
            if (r.ErrorOccured())
            {
                throw new Exception(r.ErrorMessage);
            }
            Board b = r.ReturnValue as Board;
            b.UpdateTaskDue(taskID,columnOrdinal, email, newDue);
        }
        /// <summary>
        /// modify the information we have on some task
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <param name="columnOrdinal"></param>
        /// <param name="taskID"></param>
        /// <param name="newDesc"></param>
        public void UpdateTaskDesc(string email, string boardName,int columnOrdinal, int taskID, string newDesc)
        { 
            Response r = GetBoard(email, boardName);
            validator.ValidateTaskDesc(newDesc);
            if (!r.ErrorOccured())
            {
                Board b = r.ReturnValue as Board;
                b.UpdateTaskDesc(taskID,columnOrdinal, email, newDesc);
            }            
        }
        /// <summary>
        /// updating task title
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <param name="columnOrdinal"></param>
        /// <param name="taskID"></param>
        /// <param name="newTitle"></param>
        /// <exception cref="Exception"></exception>throws expection if done ir doesnt exist
        public void UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskID, string newTitle)
        {
            validator.ValidateTaskTitle(newTitle); 
            Response r = GetBoard(email, boardName);
            if (r.ErrorOccured())
            {
                throw new Exception("Board doesn't exist");
            }
            Board b = r.ReturnValue as Board;
            b.UpdateTaskTitle(taskID, email, newTitle, columnOrdinal);
        }
        /// <summary>
        /// delete the board and his task
        /// </summary>
        /// <param name="email"></param>
        /// <param name="BoardName"></param>
        /// <exception cref="Exception"></exception>
        public void RemoveBoard(string email, string BoardName)
        {
            Response r = GetBoard(email, BoardName);
            Board b = r.ReturnValue as Board;
            if (b.boardowner == email)
            {
                boards[email].Remove(b);
                boardUsers.Remove(b);
                RemoveFromAllUsers(b);
                boardMapper.DeleteBoard(b);
                columnMapper.RemoveColumn(b.getID());
                return;
            }
            throw new Exception("Cannot remove board, user is not the Board owner.");
            
        }
        internal void RemoveFromAllUsers(Board b)
        {
            foreach(string email in boards.Keys)
            {
                foreach(Board board in boards[email])
                {
                    if (board.getID() == b.getID())
                        boards[email].Remove(board);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Response GetBoardsIDs(string email)
        {
            List<int> ids = new List<int>();
            if (boards.ContainsKey(email))
            {
                foreach (Board board in boards[email])
                {
                    ids.Add(board.getID());
                    
                }
                if (ids.Count == 0)
                {
                    List<string> lst = new List<string>();
                    return new Response(lst);
                }
                else
                {
                    return new Response(ids);
                }
            }
            List<string> lst1 = new List<string>();
            return new Response(lst1);
        }
        /// <summary>
        /// limit the column capacity
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardName"></param>
        /// <param name="columnOrdinal"></param>
        /// <param name="limit"></param> new limit
        public void LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            Board b= GetBoard(email, boardName).ReturnValue as Board;
            b.LimitColumn(columnOrdinal, limit);
            columnMapper.Update(b.getID(), columnOrdinal, "Lim", limit);
        }
        
        public Response GetCloumnLimit(string email, string boardName, int columnOrdinal)
        {
            Board board = GetBoard(email, boardName).ReturnValue as Board;
            return board.GetColumnLimit(columnOrdinal);

        }

        internal Response GetColumnName(string email, string boardName, int columnOrdinal)
        {
            Board b = GetBoard(email, boardName).ReturnValue as Board;
            return b.GetColumnName(columnOrdinal);   
        }



        
        /// <summary>
        /// loading the data
        /// </summary>
        public void LoadData()
        {
            
            boards.Clear();
            boardUsers.Clear();
            boardMapper.loadData();
            columnMapper.loadData();
            TaskMapper.Instance.loadData();
            Dictionary<string, List<BoardDTO>> brds = boardMapper.idMap; //email,boards
            Dictionary<string, List<BoardUserDTO>> boardusers = boardMapper.boardUserMap;
            Dictionary<int, List<ColumnDTO>> colums = columnMapper.idMap; // boardID,columns
            Dictionary<int, List<TaskDTO>> tasks = TaskMapper.Instance.idMap;// boardID,tasks
            int boardCounter = 0;
            foreach(string email in brds.Keys) //email
            {
                foreach(BoardDTO b in brds[email])
                {
                    boardCounter++;
                    if (!boards.ContainsKey(email))
                    {
                        boards.Add(email, new List<Board>());
                    }
                    Board board = new Board(b.boardowner, b.name, b.boardID);
                    board.taskID = b.taskID;
                    boards[email].Add(board);
                    boardUsers.Add(board, new List<string>());
                    //restoring columns
                    board.RestoreColumns(colums[board.getID()]);
                    //restoring tasks
                    if (tasks.ContainsKey(b.boardID))
                    {
                        board.RestoreTasks(tasks[b.boardID]);
                    }
                }
            }
            foreach (string s in boardusers.Keys)
            {
                foreach (BoardUserDTO buDTO in boardusers[s])
                {
                    Response r = getBoardByID(buDTO.boardID);
                    Board b = r.ReturnValue as Board;
                    boardUsers[b].Add(s);
                    if (!boards.ContainsKey(buDTO.user))
                    {
                        boards.Add(buDTO.user, new List<Board>());
                    }
                    if(!boards[buDTO.user].Contains(b))
                        boards[buDTO.user].Add(b);
                }
            }
            BoardCounter = boardCounter;
                        
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        internal Response JoinBoard(string email, int boardID)
        {
            /*Board board = null;
            foreach (List<Board> Boards in boards.Values)
            {
                foreach (Board b in Boards)
                {
                    if (b.GetName() == boardName)
                    {
                        board = b;
                    }
                }
            }*/
            Response r = getBoardByID( boardID);
            if (r.ErrorOccured())
            {
                throw new Exception("Board doesnt exist");
            }
            Board board = r.ReturnValue as Board;
            if (!isUniqueBoardName(email, board.name))
                return new Response("the user already has a board with the same name",true);
            return Add2Boards(email, ref board);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="boardID"></param>
        /// <returns></returns>
        internal Response LeaveBoard(string email, int boardID)
        {
            Response response = getBoardByID( boardID);
            if (response.ErrorOccured())
            {
                return response;
            }
            Board b = response.ReturnValue as Board;
            if(b.boardowner!= email)
            {
                b.unAssign(email);
                boards[email].Remove(b);
                boardUsers[b].Remove(email);
                boardMapper.deleteBoardUser(new BoardUserDTO(b.getID(), email));
                return new Response(null);
            }
            throw new Exception("Cannot leave board user is the owner.");            
        }
        
        ///
        internal Response changeOwnership(string email,string boardname,string newOwnerEmail)
        {
            Response res = GetBoard(email, boardname);
            Response res2 = GetBoard(newOwnerEmail, boardname);
            if (!res.ErrorOccured() && !res2.ErrorOccured()) {
               Board b = res.ReturnValue as Board;
                if(b.boardowner == email)
                {
                    b.boardowner= newOwnerEmail;
                    BoardDTO boardDTO = new BoardDTO(b);
                    boardMapper.addData(boardDTO);
                    return new Response(null);
                }
                throw new Exception("User is not board owner");                
            }
            return res.ErrorOccured() ? res.Exception() : res2.Exception() ;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private Response Add2Boards(string email, ref Board b)
        {
            if (!boards.ContainsKey(email))
            {
                boards.Add(email, new List<Board>());
            }
            boards[email].Add(b);
            boardUsers[b].Add(email);
            boardMapper.addBoardUser(new BoardUserDTO(b.getID(), email));
            return new Response("Successfully joined a board.");
        }

    }
}
