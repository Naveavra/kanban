using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using IntroSE.Kanban.Backend.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    public class UserControl
    {
        private Dictionary<string, User> users; //<email, User>
        log4net.ILog logger = Log.GetLogger();
        private UserMapper userMapper { get; set; }
        private Validator validator;
        public UserControl() {
            users = new Dictionary<string, User>();
            validator = Validator.Instance;
            userMapper = new UserMapper();
/*            UserMapper.loadData();
*/            validator.reset();
        }

 /*       private static UserControl instance = null;
        public static UserControl Instance { get { return instance ?? (instance = new UserControl()); } }*/
        
        public Response LoadData()
        {
            if (users.Count != 0)
            {
                return new Response("data is loaded already", true);
                
            }
            users.Clear();
            userMapper.loadData();
            foreach(UserDTO dto in userMapper.idMap.Values) 
            {
                users.Add(dto.email, new User(dto.email, dto.password));
            }
            /*List<string> list = new List<string>();
            foreach (string key in users.Keys)
            {
                list.Add(key);
            }*/
            validator.insertUsersFromDB(users);
            return new Response("{}");
        }
        public void Login(string email, string pw)
        {
            LoadData();
            //checks if a user is already logged in from the users that are logged in and not from all the users that we have on the db
            if (users.ContainsKey(email))
            {
                users[email].Login(pw);
            }
            else 
            { 
                throw new Exception($"Email: {email} Doesn't exist.");
                logger.Warn("Error. email: " + email + " doesn't exist"); 
            }
        }




        public void Register(string email, string pw)
        {
            
            LoadData();
            validator.ValidateRegistraion(email, pw);
            User user = new User(email, pw);
            //users.Add(email, user);
            userMapper.addData(new UserDTO(email, pw, false));
            logger.Info("User: " + email + ", Registered Successfully");
            
            
        }

        public void Logout(string email)
        {
            if (!users.ContainsKey(email))
            {
                logger.Warn("User doesn't exist. Email: "+email);
                throw new Exception("User doesn't exist");
            }
            users[email].Logout();
                        
        }

        internal Response DeleteData()
        {
            /*Dictionary<string, UserDTO> usrs= new Dictionary<string, UserDTO>();
            foreach (User user in users.Values)
            {
                usrs.Add(user.email,new UserDTO(user.email,user.getPassword(),user.Logged()));
            }
            return userMapper.deleteData(usrs);*/
            users.Clear();
            userMapper.deleteData();
//            userMapper.setLoaded(false);
            return new Response("{}");
        }

        internal bool Logged(string email)
        {
            userMapper.loadData();
            if (users.ContainsKey(email))
            {
                return users[email].Logged();
            }
            else
            {
                return false;
            }
        }

        internal bool Registered(string email)
        {
            userMapper.loadData();
            if (!userMapper.getUsers().ContainsKey(email))
            {
                return false;
                throw new Exception($"User isn't registered, Email: {email}");
            }
            return true;
        }
    }
}
