using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    internal sealed class UserControl
    {
        private Dictionary<string, User> users;

        private Validator validator;
        private UserControl() {
            users = new Dictionary<string, User>();
            validator = Validator.Instance;
        }

        private static UserControl instance = null;
        public static UserControl Instance { get { return instance ?? (instance = new UserControl()); } }
        

        public string Login(string email,string pw)
        {
            //checks if a user is already logged in from the users that are logged in and not from all the users that we have on the db
            if (users.ContainsKey(email)){
                if(users[email].getPassword() == pw)
                {
                    users[email].Login();
                    return "Success";
                }
                return "Password is incorrect";
            }
            return "Email doesn't exist.";
        }
        
        public string Register(string email, string pw) // TODO
        {
            if(validator.validateEmail(email).Equals("Great Success")){

 /*               if(Validator.EmailIsUnique(email, list <string> AllEmails)){
                    if(Validator.validatePassword(getPassword)){
                        User user = new User(email, pw)
                    }
                }*/
            }
        }

    }
}
