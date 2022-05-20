
using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class UserService
    {

        /*        UserControl userControl = UserControl.Instance;
        */        /// <summary>
                  /// this method will register a new user to our system
                  /// </summary>
                  /// <param name="email">The user email must be unique.</param>
                  /// <param name="password">the user password, A valid password is in the length of 6 to 20 characters and must include at least one
                  ///uppercase letter, one small character and a number.</param>
                  /// <returns>response - string with the message "successfuul registration" unless an error occures, then "failure, please try again"
                  /// might be for two reasons problem with user's email or invalid password</returns> 
                  /// 
        /* private static UserService instance = null;

         public static UserService Instance { get { return instance ?? (instance = new UserService()); } }*/
        UserControl userControl;
        public UserService()
        {
            userControl = new UserControl();
        }

        public string Register(string email, string password)
        {
            string newEmail = email.ToLower();
            Response response = userControl.Register(newEmail, password);  
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return "{}";
        }
        /// <summary>
        /// this mehod will allow a rgistered user to log in to his account
        /// </summary>
        /// <param name="email"> existing user email address.</param>
        /// <param name="password"> the user password as saved in the system. </param>
        /// <returns>response- string with the message "successfully logged in" unlees an error occures. </returns> 
        public string Login(string email, string password)
        {
            email= email.ToLower();
            Response response = userControl.Login(email, password);
            if (response.ErrorOccured())
            {
                return response.Serialize(); 
            }
            return response.Serialize();
        }
        /// <summary>
        /// this method will log out a log in user
        /// </summary>
        /// <param name="email">The user email address</param>
        /// <returns>response - the string "successfully logout" unless an error occures. </returns>
        public string Logout(string email)
        {
            email = email.ToLower();
            Response response = userControl.Logout(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return "{}";

        }

        public bool Logged(string email)
        {
            if (userControl.Logged(email))
            {
                return true;
            }
            return false;
        }
    }
}
