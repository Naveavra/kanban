using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    internal class DBConnector
    {
        public SQLiteConnection conn;
        // Create a new database connection:
        static string _db_name = "kanban.db";
        string tempPath = "C:\\Users\\Nave Avraham\\Desktop\\kanbannew\\2021-2022-kanban-2021-2022-36\\kanban.db";
        private static DBConnector _instance = null;
        private static readonly object padlock = new object();
        //relative path 
        string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), _db_name));
        private DBConnector()
        {
            conn = CreateConnection();
            CreateTables(conn);
        }
        public static DBConnector Instance
        {
            get { 
                lock (padlock) { if(_instance == null)
                    {
                        _instance = new DBConnector();
                    }
                    return _instance;
                } 
            }
        }
        internal SQLiteConnection getConn()
        {
            return conn;
        }
        public void RemoveTables()
        {
            SQLiteCommand cmd = prepare();
            /*cmd.CommandText = @"DROP TABLE IF EXISTS Users;
                DROP TABLE IF EXISTS Boards;
                DROP TABLE IF EXISTS BoardUsers;
                DROP TABLE IF EXISTS Columns;
                DROP TABLE IF EXISTS Tasks;
                ";*/
            cmd.CommandText = @"DELETE FROM Users;
                                DELETE FROM Boards;
                                DELETE FROM BoardUsers;
                                DELETE FROM Columns;
                                DELETE FROM Tasks;";
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            CreateTables(conn);
        }
        internal void CreateTables(SQLiteConnection conn)
        {
            /*if (!File.Exists(tempPath))
            {*/
/*            RemoveTables();
*/            SQLiteCommand sqlite_cmd;
            string Createsql = @"CREATE TABLE IF NOT EXISTS Users (
            'email' varchar(30) NOT NULL UNIQUE,
            'password' varchar(20) NOT NULL,
             Primary Key('email')
                );
            CREATE TABLE IF NOT EXISTS Boards(
            'BoardId' INTEGER NOT NULL UNIQUE,
            'boardName' TEXT NOT NULL,
            'owner' varchar(30) NOT NULL,
            'taskID' INTEGER NOT NULL,
            PRIMARY KEY('BoardId'),
            FOREIGN KEY('owner') references Users('email') on update no action on delete cascade
            );
            CREATE TABLE IF NOT EXISTS BoardUsers(
            'boardID' INTEGER NOT NULL,
            'userEmail' TEXT NOT NULL,
            PRIMARY KEY('boardID','userEmail'),
            FOREIGN KEY('boardID') references Boards('BoardId') on delete cascade,
            FOREIGN KEY('userEmail') references Users('email') on delete cascade
            );
            CREATE TABLE IF NOT EXISTS Columns(
            'boardID' INTEGER NOT NULL,
            'ordinal' INTEGER NOT NULL,
            'columnName' TEXT,
            'Lim' INTEGER,
            PRIMARY KEY('boardID','ordinal'),
            FOREIGN KEY('boardID') references Boards('BoardId') on delete cascade
            );
            CREATE TABLE IF NOT EXISTS Tasks(
            'BoardId' INTEGER NOT NULL,
            'id' INTEGER NOT NULL,
            'owner' TEXT NOT NULL,
            'assigned' TEXT,
            'title' varchar(50),
            'desc' varchar(300),
            'dueDate' TEXT,
            'created' TEXT,
            'ordinal' INTEGER NOT NULL,
            PRIMARY KEY('boardID','id'),
            FOREIGN KEY('boardID') REFERENCES Boards('BoardId') on update no action on delete cascade
            );
            "; 
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Createsql;
            sqlite_cmd.ExecuteNonQuery();

            
            
                
            //}

        }
        internal SQLiteConnection CreateConnection()
        {

            SQLiteConnection conn;
            
            Console.WriteLine(path);
            conn = new SQLiteConnection($"Data Source= {path}; Version = 3;");
            // Open the connection:
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return conn;
        }
        public SQLiteDataReader readData(string field)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM {field}";
            return sqlite_cmd.ExecuteReader();
        }
        public SQLiteDataReader readDataWhere(string tableName,string field,string value)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT * FROM {tableName} WHERE '{field}' = {value}";
            return sqlite_cmd.ExecuteReader();
        }
       /* public void Update(string query,List<string> paramNames, params object[] values)
        {
            SQLiteCommand cmd;
            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = query;
                for (int i = 0; i < paramNames.Count; i++)
                {
                    cmd.Parameters.Add(paramNames[i],System.Data).Value = values[i])
                }
                
                sqlite_cmd.Parameters.Add("col1", System.Data.DbType.String).Value = "amazing ";
                sqlite_cmd.Parameters.Add("col2", System.Data.DbType.Int32).Value = 15;
                sqlite_cmd.Parameters.Add("name", System.Data.DbType.String).Value = "Test Text ";
                sqlite_cmd.ExecuteNonQuery();

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

        }*/
       public SQLiteCommand prepare()
        {
            reset();
            return conn.CreateCommand();
        }
       public void ExecuteNonQuery(string query)
        {
            SQLiteCommand cmd = prepare();
            cmd.Prepare();
            cmd.CommandText = query;
            cmd.ExecuteNonQuery ();
        }
       /*public void ExecuteNonQuery(SQLiteCommand cmd)
        {
            cmd.ExecuteNonQuery();
        }*/
        public void reset()
        {
            conn.Close();
            conn.Open();
        }

    }
}
