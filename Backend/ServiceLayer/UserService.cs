
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
/*            userControl.LoadData();
*/        }

        public string Register(string email, string password)
        {
            string newEmail = email.ToLower();
            try
            {
                userControl.Register(newEmail, password);
                return new Response().Serialize();

            }catch (Exception ex)
            {
                return new Response(ex.Message,true).Serialize();
            }
            
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
            try
            {
                userControl.Login(email, password);
                return new Response(email).Serialize();
            }catch (Exception ex)
            {
                return new Response(ex.Message,true).Serialize();
            }
            
        }
        /// <summary>
        /// this method will log out a log in user
        /// </summary>
        /// <param name="email">The user email address</param>
        /// <returns>response - the string "successfully logout" unless an error occures. </returns>
        public string Logout(string email)
        {
            email = email.ToLower();
            try
            {
                userControl.Logout(email);
                return new Response().Serialize();
            }
            catch (Exception ex)
            {
                return new Response(ex.Message,true).Serialize();
            }
            

        }
        
        public bool Logged(string email)
        {
            return userControl.Logged(email);
        }
        public Response LoadData()
        {
            return userControl.LoadData();
        }

        public string DeleteData()
        {
            return userControl.DeleteData().Serialize();
        }

        internal bool Registered(string email)
        {
            return userControl.Registered(email);
        }
    }
}
