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
    internal class UserMapper
    {
        DBConnector conn = DBConnector.Instance; 
        public Dictionary<string,UserDTO> idMap { get; }

        private bool loaded = false;
        public UserMapper()
        {
            idMap = new Dictionary<string, UserDTO>();
        }
        public Dictionary<string, UserDTO> getUsers() { return idMap; }
        public void addData(UserDTO user)
        {
            SQLiteCommand cmd = conn.prepare();

            cmd.CommandText = $"INSERT INTO Users (email,password) " +
                    $"VALUES (@Email,@PW);";

            SQLiteParameter email = new SQLiteParameter(@"Email", user.email);
            SQLiteParameter PW = new SQLiteParameter(@"PW", user.password);
            cmd.Parameters.Add(email);
            cmd.Parameters.Add(PW);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            idMap.Add(user.email, user);
            /*string InsertOrIgnore = "INSERT INTO Users(email,password) ";
            InsertOrIgnore += string.Format("VALUES(@{email},@{password})", user.email, user.password);
            conn.ExecuteNonQuery(InsertOrIgnore);*/
        }
        public void loadData() {
            if (getUsers().Count!=0)
            {
                return;
            }
            idMap.Clear();
            SQLiteDataReader result = conn.readData("Users");
            while (result.Read())
            {
                string email = result.GetString(0);
                string password = result.GetString(1);
                idMap.Add(email, new UserDTO(email,password,false));
            }
            conn.reset();
        }
        public Response deleteData()
        {
            string UpdateQuery = "UPDATE Users SET email =: email,password =: password WHERE email =: email";
            try
            {
                /*foreach (UserDTO user in users.Values)
                {
                    string InsertOrIgnore = "INSERT OR IGNORE INTO Users(email,password) ";
                    string format = "VALUES(@{email},@{password})";
                    InsertOrIgnore+= string.Format(format,user.email,user.password);
                    conn.ExecuteNonQuery(InsertOrIgnore);
                    //cmd=conn.prepare();
                    SQLiteCommand cmd = conn.prepare();
                    cmd.CommandText = UpdateQuery;
                    cmd.Parameters.Add("email", System.Data.DbType.String).Value = user.email;
                    cmd.Parameters.Add("password", System.Data.DbType.String).Value = user.password;
                    cmd.ExecuteNonQuery();
                    
                }*/
                idMap.Clear();
                return new Response("");
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response("An error occured in DeleteData - userMapper",true);
            }

        }

        public bool Contains(string email)
        {
            SQLiteDataReader result = conn.readData("Users");
            while (result.Read())
            {
                if (result.GetString(0) == email)
                    return true;
            }
            return false;
        }
        public bool Loaded()
        {
            return Loaded();
        }

        internal void setLoaded(bool v)
        {
            loaded = v;
        }
    }
}
