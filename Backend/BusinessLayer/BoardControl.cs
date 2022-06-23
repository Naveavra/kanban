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
        log4net.ILog logger = Log.GetLogger();
        Validator validator = Validator.Instance; //dan
        Dictionary<string, List<Board>> boards; //borads pool <name, Board>
        Dictionary<Board, List<string>> boardUsers;
        private ColumnMapper columnMapper{get;}
        private BoardMapper boardMapper { get; }
        public BoardControl(){
            boards = new Dictionary<string, List<Board>>();
            boardUsers = new Dictionary<Board, List<string>>();
            columnMapper = ColumnMapper.Instance;
            boardMapper = BoardMapper.Instance;
            BoardCounter = boardMapper.GetLastID();
         /*   TaskMapper.loadData();
            columnMapper.loadData();
            boardMapper.loadData();*/
            }

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
        public void AddBoard(string email, string boardName){   
        if(String.IsNullOrWhiteSpace(boardName) || String.IsNullOrEmpty(boardName))
        {
            logger.Warn("Cannot add an invalid board name");
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
            logger.Info("Board added succesfully");
        }
        else 
        { 
            logger.Warn("A board with tha name already exists");
            throw new Exception("Board with this name already exists");
        }
    }
        internal int GetAndIncrement()
        {
/*            BoardCounter = boardMapper.GetLastID();
*/            int prev = BoardCounter;
            BoardCounter++;
            return prev;
        }
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
                        return new Response("The user has not yet joined the board",true);
                    }
                    
                    return b.AssignTask(email,columnOrdinal, taskId, emailAssignee);
                }
                return r;
            }
            return new Response("Assigned email doesn't exist",true);
        }
        public Response GetBoard(string email,string boardname) //new function uses reponse to let us know if the board exists
        {
            //LoadData();
           
            foreach (Board b in boards[email])
            {
                if (b.GetName() == boardname)
                {
                    return new Response(b);
                }
            }
            
            logger.Warn($"Board: {boardname}, doesn't exist.");
            throw new Exception("Board doesn't exist");   
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
            logger.Warn("BoardID: " + id + "doesn't exist.");
            return new Response("Board doesnt exist", true);
        }
/*        internal Response GetColumnForRel(string email, string boardName, int columnOrdinal)
        {
            Board b = GetBoard(email, boardName).ReturnValue as Board;
            return GetBoard(email, boardName).GetColumnForReal(columnOrdinal);
        }*/

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

        internal void AdvanceTask(string email, string boardName, int taskId)
        {
            Response r = GetBoard(email, boardName);
            if (!r.ErrorOccured())
            {
                Board b = r.ReturnValue as Board;
                b.AdvanceTask(taskId,email);
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
                logger.Info("User doesn't have a board with that name yet");
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
            return new Response();
            /*Dictionary<string,List<BoardDTO>> dtos = new Dictionary<string,List<BoardDTO>>();
            Dictionary<string, List<ColumnDTO>> cols = new Dictionary<string, List<ColumnDTO>>();
            Dictionary<string, List<TaskDTO>> tsks = new Dictionary<string, List<TaskDTO>>();
            foreach (string s in boards.Keys)
            {
                List<BoardDTO> b = new List<BoardDTO>();
                foreach(Board board in boards[s])
                {
                    b.Add(new BoardDTO(board.getID(), board.name, board.boardowner, board.getID()));
                    List<ColumnDTO> col = new List<ColumnDTO>();
                    int i = 0;
                    foreach(Column c in board.columns)
                    {
                        col.Add(new ColumnDTO(c.getName(), c.getlimit(), i, board.getID()));
                        i++;
                        List<TaskDTO> tsk = new List<TaskDTO>();
                        foreach(Task t in c.GetTasks().Values)
                        {
                            tsk.Add(new TaskDTO(t,board.getID(),i,board.boardowner));
                        }
                        tsks.Add(board.name, tsk);
                    }
                    cols.Add(board.name, col);
                }
                dtos.Add(s, b);
            }
            try
            {
                boardMapper.deleteData(dtos);
                return new Response("Data Deleted Successfully");
            }catch(Exception e)
            {
                return new Response("Something went wrong in delete BoardData board-control");
            }*/
            
            
        }

        /*        public string GetDone(string email, string BoardName)
                {
                    string res = "";
                    foreach (Board b in boards[email].Values)
                    {
                        res += b.GetDone();
                    }
                    return res;

                }*/

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

        public void RemoveBoard(string email, string BoardName)
        {
            Response r = GetBoard(email, BoardName);
            Board b = r.ReturnValue as Board;
            if (b.boardowner == email)
            {
                boards[email].Remove(b);
                boardUsers.Remove(b);
                boardMapper.DeleteBoard(b);
                columnMapper.RemoveColumn(b.getID());
                logger.Info("Board removed successfully");
                return;
            }
            throw new Exception("Cannot remove board, user is not the Board owner.");
            
        }
         

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

        public void LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            Board b= GetBoard(email, boardName).ReturnValue as Board;
            b.LimitColumn(columnOrdinal, limit);
            columnMapper.Update(b.getID(), columnOrdinal, "Lim", limit);
            logger.Info("Successfully Limited column");   
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



        // NEW IMPL HERE MILESTONE 2
    
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
            if (!isUniqueBoardName(board.name, email))
                return new Response("the user already has a board with the same name",true);
            return Add2Boards(email, ref board);
        }
        
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
            return new Response("Cannot leave board user is the owner.",true);
            
        }
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
                return new Response("User is not board owner");
                
            }
            return res.ErrorOccured() ? res : res2 ;
        }
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
