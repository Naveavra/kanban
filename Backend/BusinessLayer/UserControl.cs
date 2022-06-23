using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
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
        
        /// <summary>
        /// this function should load the data from the db to the ram
        /// </summary>
        /// <returns></returns>empty Response
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
            return new Response();
        }

        /// <summary>
        /// user login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pw"></param>
        /// <exception cref="Exception"></exception> if the email doesnt exist or the password is incorrect
        public void Login(string email, string pw)
        {
            //LoadData();
            //checks if a user is already logged in from the users that are logged in and not from all the users that we have on the db
            if (users.ContainsKey(email))
            {
                users[email].Login(pw);
            }
            else 
            {
                throw new Exception($"Email: {email} Doesn't exist.");
            }
        }



        /// <summary>
        /// user registraion
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pw"></param>
        public void Register(string email, string pw)
        {
 //           LoadData();
            validator.ValidateRegistration(email, pw);
            User user = new User(email, pw);
            if(!users.ContainsKey(email))
                users.Add(email, user);
            userMapper.addData(new UserDTO(email, pw, false));
            Login(email, pw);
        }
        /// <summary>
        /// user logout
        /// </summary>
        /// <param name="email"></param>
        /// <exception cref="Exception"></exception>
        public void Logout(string email)
        {
            if (!users.ContainsKey(email))
            {
                throw new Exception($"User doesn't exist {email}");
            }
            users[email].Logout();
                        
        }
        /// <summary>
        /// delete all data from the ram and the DB
        /// </summary>
        /// <returns></returns> empty Response
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
            validator.reset();
//            userMapper.setLoaded(false);
            return new Response();
        }
        /// <summary>
        /// checks if the user is logged
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns boolean true if logged otherwise false
        internal bool Logged(string email)
        {
            //userMapper.loadData();
            if (users.ContainsKey(email))
            {
                return users[email].Logged();
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// checks if the user is logged
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>boolean true if registered otherwise false
        internal bool Registered(string email)
        {
            email = email.ToLower();
            if (!userMapper.getUsers().ContainsKey(email))
            {
                return false;
                throw new Exception($"User isn't registered, Email: {email}");
            }
            return true;
        }
    }
}
