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
        private Validator validator;
        public UserControl() {
            users = new Dictionary<string, User>();
            validator = Validator.Instance;
            validator.reset();
        }

 /*       private static UserControl instance = null;
        public static UserControl Instance { get { return instance ?? (instance = new UserControl()); } }*/
        

        public Response Login(string email,string pw)
        {
            //checks if a user is already logged in from the users that are logged in and not from all the users that we have on the db
            if (users.ContainsKey(email)){
                if (users[email].Logged())
                {

                    return new Response("User is allready logged in", true);
                }
                else
                {
                    if (users[email].getPassword() == pw)
                    {
                        users[email].Login();
                        logger.Info("The user " + email + " Logged In Successfully");
                        return new Response(email);
                    }
                }
                logger.Warn("Password is incorrect. email: "+email+" has failed to login");
                return new Response("Password is incorrect", true);
            }
            logger.Warn("Error. email: " + email + " doesn't exist");
            return new Response("Email doesn't exist.", true);
        }
        
        public Response Register(string email, string pw) 
        {
            if(validator.ValidteRegistraion(email, pw))
            {
                User user = new User(email, pw);
                users.Add(email, user);
                logger.Info("User: " + email + ", Registered Successfully");
                return new Response("Successfully registred new user");

            }
            return new Response("Registration Failed", true);    
            
        }

        public Response Logout(string email)
        {
            if (!users.ContainsKey(email))
            {
                logger.Warn("User doesn't exist. Email: "+email);
                return new Response("User dosen't exist", true);
            }
            else
            {
                if (users[email].Logged()) {
                    users[email].Logout();
                    logger.Info("User successfully logged out.");
                    return new Response("Succefully logged out");
                }
                logger.Warn("Error: Cannot logout, User is not logged in. Email: " + email);
                return new Response("User isn't logged in", true);
            }
        }

        internal bool Logged(string email)
        {
            if (users.ContainsKey(email))
            {
                return users[email].Logged();
            }
            else
            {
                return false;
            }
        }
    }
}
