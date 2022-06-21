using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class TaskMapper // 0-boardID, 1-id, 2-assigned,3-title,4-desc,5-dueDate,6-created,7- columnOrdinal
    {
        DBConnector conn = DBConnector.Instance;
        public Dictionary<int, List<TaskDTO>> idMap { get; }
        private static TaskMapper _instance = null;
        private static readonly object padlock = new object();
        public static TaskMapper Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new TaskMapper();
                    }
                    return _instance;
                }
            }
        }
        private TaskMapper()
        {
            idMap = new Dictionary<int, List<TaskDTO>>(); //boardID, List<Tasks>
        }
        public Dictionary<int, List<TaskDTO>> getTasks() { return idMap; }
        public void addData(TaskDTO dto)
        {
            SQLiteCommand cmd = conn.prepare();
            string InsertOrIgnore = $"Insert OR ignore INTO Tasks (BoardId,id,owner,assigned,title,desc,dueDate,created,ordinal) "+
            $"VALUES (@BoardId,@id,@owner,@assigned,@title,@desc,@dueDate,@created,@ordinal)";
            /*cmd.CommandText = $"REPLACE INTO Boards (BoardId,boardName,owner,taskID) " +
                    $"VALUES (@BoardId,@boardName,@owner,@taskID);";*/
            cmd.CommandText = InsertOrIgnore;
            SQLiteParameter BoardId = new SQLiteParameter(@"BoardId", dto.boardID);
            SQLiteParameter id = new SQLiteParameter(@"id", dto.Id);
            SQLiteParameter owner = new SQLiteParameter(@"owner",dto.owner);
            SQLiteParameter assigned= new SQLiteParameter(@"assigned", dto.Assignee);
            SQLiteParameter title= new SQLiteParameter(@"title", dto.Title);
            SQLiteParameter description= new SQLiteParameter(@"desc", dto.Description);
            SQLiteParameter dueDate = new SQLiteParameter(@"dueDate", dto.DueDate);
            SQLiteParameter creation = new SQLiteParameter(@"created",dto.CreationTime);
            SQLiteParameter ordinal = new SQLiteParameter(@"ordinal",dto.ordinal);
            cmd.Parameters.Add(BoardId);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(owner);
            cmd.Parameters.Add(assigned);
            cmd.Parameters.Add(title);
            cmd.Parameters.Add(description);
            cmd.Parameters.Add(dueDate);
            cmd.Parameters.Add(creation);
            cmd.Parameters.Add(ordinal);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        public void loadData()
        {
            idMap.Clear();
            SQLiteDataReader result = conn.readData("Tasks");
            while (result.Read())
            {
                int boardID = result.GetInt32(0);
                int id = result.GetInt32(1);
                string owner = result.GetString(2);
                string assigned = result.GetString(3);
                string title = result.GetString(4);
                string desc = result.GetString(5);
                DateTime dueDate = result.GetDateTime(6);
                DateTime creation = result.GetDateTime(7);
                int ordinal = result.GetInt32(8);
                TaskDTO t = new TaskDTO(id,creation,title,desc,ordinal,boardID,dueDate,assigned,owner);
                
                if (!idMap.ContainsKey(boardID))
                {
                    idMap.Add(boardID, new List<TaskDTO>());
                }
                idMap[boardID].Add(t);
            }
            conn.reset();
        }
        public void Update(int taskID,string email, string attributeName, object attributeValue)
        {
            SQLiteCommand cmd = conn.prepare();
            cmd.CommandText = $"update Tasks set '{attributeName}'=@{attributeName} where id=@taskID and owner=@Email";
            SQLiteParameter value = new SQLiteParameter(attributeName, attributeValue);
            SQLiteParameter id = new SQLiteParameter(@"taskID",taskID);
            SQLiteParameter boardNam = new SQLiteParameter(@"Email",email);
            cmd.Parameters.Add(value);
            cmd.Parameters.Add(id);
            cmd.Parameters.Add(boardNam);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        public void deleteData(Dictionary<string,List<TaskDTO>> tasks)
        {
            string updateQuery = "UPDATE Tasks SET boardName =: boardname, id =: id, assigned =: assigned, title =: title,desc =:desc,dueDate =: dueDate,created=: created,ordinal = ordinal, Status =: status WHERE boardName =: boardname AND id =: id"; 
            try
            {
                foreach (List<TaskDTO> t in tasks.Values)
                {
                    foreach (TaskDTO task in t)
                    {
                        addData(task);
                        SQLiteCommand cmd = conn.prepare();
                        cmd.CommandText = updateQuery;
                        cmd.Parameters.Add("boardID", System.Data.DbType.String).Value = task.boardID;
                        cmd.Parameters.Add("id", System.Data.DbType.Int32).Value = task.Id;
                        cmd.Parameters.Add("assigned", System.Data.DbType.String).Value = task.Assignee;
                        cmd.Parameters.Add("title", System.Data.DbType.String).Value = task.Title;
                        cmd.Parameters.Add("desc", System.Data.DbType.String).Value = task.Description;
                        cmd.Parameters.Add("dueDate", System.Data.DbType.String).Value = task.DueDate;
                        cmd.Parameters.Add("creation", System.Data.DbType.String).Value = task.CreationTime;
                        cmd.Parameters.Add("ordinal", System.Data.DbType.Int32).Value = task.ordinal;
                        cmd.ExecuteNonQuery();
                    }
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            idMap.Clear();

        }
    }
}
