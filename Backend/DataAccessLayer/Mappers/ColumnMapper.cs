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
    internal class ColumnMapper // 0-boardName, 1-ordinal,2-ColumnName,3-limit
    {
        DBConnector conn = DBConnector.Instance;
        public Dictionary<int, List<ColumnDTO>> idMap { get; }
        private static ColumnMapper _instance = null;
        private static readonly object padlock = new object();
        private ColumnMapper()
        {
            idMap = new Dictionary<int, List<ColumnDTO>>();
        }
        public static ColumnMapper Instance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new ColumnMapper();
                    }
                    return _instance;
                }
            }
        }
        public Dictionary<int, List<ColumnDTO>> getColumns() { return idMap; }
        public void loadData()
        {
            idMap.Clear();
            SQLiteDataReader result = conn.readData("Columns");
            while (result.Read())
            {
                int boardID= result.GetInt32(0);
                int ordinal = result.GetInt32(1);
                string columnName = result.GetString(2);
                int limit = result.GetInt32(3);
                ColumnDTO c = new ColumnDTO(columnName,limit,ordinal,boardID);
                if (!idMap.ContainsKey(boardID))
                {
                    idMap.Add(boardID, new List<ColumnDTO>());
                }
                idMap[boardID].Add(c);
            }
            conn.reset();
        }
        public void deleteData(Dictionary<string,List<ColumnDTO>> columns)
        {
            string UpdateQuery = "UPDATE Columns SET boardName =: boardName, ordinal =: ordinal, columnName =: columnName,limit =: limit";
            try
            {
                foreach (List<ColumnDTO> c in columns.Values)
                {
                    foreach (ColumnDTO column in c)
                    {
                        addData(column);
                        SQLiteCommand cmd = conn.prepare();
                        cmd.CommandText = UpdateQuery;
                        cmd.Parameters.Add("boardName", System.Data.DbType.String).Value = column.boardID;
                        cmd.Parameters.Add("ordinal", System.Data.DbType.Int32).Value = column.ordinal;
                        cmd.Parameters.Add("columnName", System.Data.DbType.String).Value = column.ColumnName;
                        cmd.Parameters.Add("limit", System.Data.DbType.Int32).Value = column.limit;
                        cmd.ExecuteNonQuery();                    }

                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
            idMap.Clear();
        }

        public void addData(ColumnDTO column)
        {
            SQLiteCommand cmd = conn.prepare();
            string ReplaceOrInsert = $"REPLACE INTO Columns (boardID,ordinal,columnName,Lim) "+
            $"VALUES (@boardID,@ordinal,@columnName,@Lim)";
            /*cmd.CommandText = $"REPLACE INTO Boards (BoardId,boardName,owner,taskID) " +
                    $"VALUES (@BoardId,@boardName,@owner,@taskID);";*/
            cmd.CommandText = ReplaceOrInsert;
            SQLiteParameter boardName = new SQLiteParameter(@"boardID", column.boardID);
            SQLiteParameter ordinal= new SQLiteParameter(@"ordinal", column.ordinal);
            SQLiteParameter columnName= new SQLiteParameter(@"columnName", column.ColumnName);
            SQLiteParameter limit = new SQLiteParameter(@"Lim", column.limit);
            cmd.Parameters.Add(boardName);
            cmd.Parameters.Add(ordinal);
            cmd.Parameters.Add(columnName);
            cmd.Parameters.Add(limit);
            cmd.Prepare();
            cmd.ExecuteNonQuery ();
        }

        internal void RemoveColumn(int v)
        {
            SQLiteCommand cmd = conn.prepare();
            cmd.CommandText = $"DELETE FROM Columns WHERE boardID = @bID;";
            //SQLiteParameter tablename = new SQLiteParameter(@"tableName", first);
            SQLiteParameter bID = new SQLiteParameter(@"bID", v);
            //cmd.Parameters.Add(tablename);  
            cmd.Parameters.Add(bID);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
        public void Update(int boardID, int ordinal,string attributeName, object attributeValue)
        {
            SQLiteCommand cmd = conn.prepare();
            cmd.CommandText = $"update Columns set '{attributeName}'=@{attributeName} where boardID=@boardID and ordinal = {ordinal}";
            SQLiteParameter value = new SQLiteParameter(attributeName, attributeValue);
            SQLiteParameter id = new SQLiteParameter(@"boardID", boardID);
            cmd.Parameters.Add(value);
            cmd.Parameters.Add(id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}
