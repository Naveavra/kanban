using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class BoardMapper // 0-id ,1-boardName,2-owner,3-taskID(counter)
    {
        DBConnector conn = DBConnector.Instance;
        public Dictionary<string, List<BoardDTO>> idMap { get; }
        public Dictionary<string, List<BoardUserDTO>> boardUserMap { get; } //useremail,boarduserdto
        private static BoardMapper _instance = null;
        private static readonly object padlock = new object();
        private bool loaded = false;
        public static BoardMapper Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new BoardMapper();
                    }
                    return _instance;
                }
            }
        }
        public bool Loaded()
        {
            return loaded;
        }
        public void setLoaded(bool pred)
        {
            loaded = pred;
        }
        internal int GetLastID()
        {
            int ans = 0;
            try
            {
                SQLiteDataReader result = conn.readData("Boards");
                while (result.Read())
                {
                    ans = result.GetInt32(0);
                }
            }
            catch (Exception E) { return 0; }
            return ans+1;
        }

        private BoardMapper()
        {
            idMap = new Dictionary<string, List<BoardDTO>>();
            boardUserMap = new Dictionary<string, List<BoardUserDTO>>();
        }
        public Dictionary<string, List<BoardDTO>> getBoards() { return idMap; }
        public void loadData()
        {   //if(loaded) return;
            loaded = true;
            idMap.Clear();
            SQLiteDataReader result = conn.readData("Boards");
            string boardName="";
            int id=0;
            string owner="";
            int taskID=0;
            while (result.Read())
            {
                id = result.GetInt32(0);
                boardName = result.GetString(1);
                owner = result.GetString(2);
                taskID = result.GetInt32(3);
                /*BoardDTO b = new BoardDTO(id,boardName,owner, taskID, );
                b.taskID = taskID;
                idMap.Add(userEmail, b);*/

                
                BoardDTO bDTO = new BoardDTO(id, boardName, owner, taskID);
                if (!idMap.ContainsKey(owner))
                {
                    idMap.Add(owner, new List<BoardDTO>());
                }
                idMap[owner].Add(bDTO);
            }
            conn.reset();
            SQLiteDataReader users = conn.readData("BoardUsers"); // 0- boardName, 1-userEmail
            while (users.Read())
            {
                int BID = users.GetInt32(0);
                string username = users.GetString(1);
                if (!boardUserMap.ContainsKey(username))
                {
                    boardUserMap.Add(username,new List<BoardUserDTO>());
                }
                boardUserMap[username].Add(new BoardUserDTO(BID, username));
            }
        }

        internal bool Contain(string email, string boardName)
        {
            SQLiteDataReader result = conn.readDataWhere("Boards","boardName",boardName);
            Dictionary<int,string> ids_names = new Dictionary<int,string>();
            while (result.Read())
            {
                int id = result.GetInt32(0);
                string owner = result.GetString(2);
                ids_names.Add(id, owner);
                if (owner == email)
                    return true;
            }
            foreach(int id in ids_names.Keys)
            {
                result = conn.readDataWhere("BoardUsers", "boardID", id.ToString());
                while (result.Read())
                {
                    string usr = result.GetString(1);
                    if (usr == email)
                        return true;
                }

            }
            return false;

        }

        public void deleteData(Dictionary<string,List<BoardDTO>> boards)
        {
            loaded = false;
            foreach(List<BoardDTO> board in idMap.Values)
            {
                foreach (BoardDTO boardDTO in board)
                {
                    string remove = "DELETE FROM BoardUsers WHERE boardName =: boardName";
                    SQLiteCommand cmd = conn.prepare();
                    cmd.CommandText = remove;
                    cmd.Parameters.Add("boardName", System.Data.DbType.String).Value=boardDTO.name;
                    cmd.ExecuteNonQuery();

                }
            }
            string UpdateQuery1 = "UPDATE Boards SET owner =: owner, taskID =: taskID WHERE id =: id";
            foreach(List<BoardDTO> boardDtos in boards.Values)
            {
                foreach(BoardDTO boardDTO in boardDtos)
                {
                    addData(boardDTO);
                    SQLiteCommand cmd = conn.prepare();
                    cmd.CommandText = UpdateQuery1;
                    cmd.Parameters.Add("owner", System.Data.DbType.String).Value = boardDTO.boardowner;
                    cmd.Parameters.Add("taskID", System.Data.DbType.Int32).Value = boardDTO.taskID;
                    cmd.Parameters.Add("id", System.Data.DbType.Int32).Value = boardDTO.boardID;
                    cmd.ExecuteNonQuery();                
                }
            }
            idMap.Clear();
        }

        

        public void Update(int BoardID, string attributeName, object attributeValue)
        {
            SQLiteCommand cmd = conn.prepare();
            cmd.CommandText = $"update Boards set '{attributeName}'=@{attributeName} where BoardId=@bID";
            SQLiteParameter value = new SQLiteParameter(attributeName, attributeValue);
            SQLiteParameter id = new SQLiteParameter(@"bID", BoardID);
            cmd.Parameters.Add(value);
            cmd.Parameters.Add(id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        public void addData(BoardDTO board)
        {
            try
            {
                SQLiteCommand cmd = conn.prepare();

                cmd.CommandText = $"REPLACE INTO Boards (BoardId,boardName,owner,taskID) " +
                        $"VALUES (@BoardId,@boardName,@owner,@taskID);";

                SQLiteParameter boardID = new SQLiteParameter(@"BoardId", board.boardID);
                SQLiteParameter name = new SQLiteParameter(@"boardName", board.name);
                SQLiteParameter boardOwner = new SQLiteParameter(@"owner",board.boardowner);
                SQLiteParameter taskID = new SQLiteParameter(@"taskId", board.taskID);
                cmd.Parameters.Add(boardID);
                cmd.Parameters.Add(name);
                cmd.Parameters.Add(boardOwner);
                cmd.Parameters.Add(taskID);
                cmd.Prepare();
                cmd.ExecuteNonQuery();  
            
                
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex; 
            }
            
        }
        public void addBoardUser(BoardUserDTO buDTO)
        {
            //insert into boardUsers
            SQLiteCommand cmd = conn.prepare();
            cmd.CommandText = $"REPLACE INTO BoardUsers (boardID,userEmail) " +
                    $"VALUES (@Name,@Email);";
            SQLiteParameter id = new SQLiteParameter(@"Name", buDTO.boardID);
            SQLiteParameter boarduser = new SQLiteParameter(@"Email", buDTO.user);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(boarduser);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        public void deleteBoardUser(BoardUserDTO buDTO)
        {
            SQLiteCommand cmd = conn.prepare();
            cmd.CommandText = $"DELETE FROM BoardUsers WHERE boardID = @bID and userEmail = @Email;";
            SQLiteParameter bID = new SQLiteParameter(@"bID", buDTO.boardID);
            SQLiteParameter email = new SQLiteParameter(@"Email", buDTO.user);
            cmd.Parameters.Add(bID);
            cmd.Parameters.Add(email);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
     
        public void DeleteBoard(Board board)
        {
            string first = "Boards";
            SQLiteCommand cmd = conn.prepare();
            cmd.CommandText = $"DELETE FROM Boards WHERE BoardId = @bID;";
            //SQLiteParameter tablename = new SQLiteParameter(@"tableName", first);
            SQLiteParameter bID = new SQLiteParameter(@"bID", board.getID());
            //cmd.Parameters.Add(tablename);  
            cmd.Parameters.Add(bID);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            //first = "BoardUsers";
            cmd = conn.prepare();
            cmd.CommandText = $"DELETE FROM BoardUsers WHERE boardID = @bID;"; 
            cmd.Parameters.Add(bID);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            


        }
    }
}
