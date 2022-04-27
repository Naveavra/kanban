using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    internal class UserService
    {
        /// <summary>
        /// this method will register a new user to our system
        /// </summary>
        /// <param name="email">The user email must be unique.</param>
        /// <param name="password">the user password, A valid password is in the length of 6 to 20 characters and must include at least one
        ///uppercase letter, one small character and a number.</param>
        /// <returns>response - string with the message "successfuul registration" unless an error occures, then "failure, please try again"
        /// might be for two reasons problem with user's email or invalid password</returns> 

        public string Register(string email, string password)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// this mehod will allow a rgistered user to log in to his account
        /// </summary>
        /// <param name="email"> existing user email address.</param>
        /// <param name="password"> the user password as saved in the system. </param>
        /// <returns>response- string with the message "successfully logged in" unlees an error occures. </returns> 
        public string Login(string email, string password)
        {
            throw new NotImplementedException();

        }
        /// <summary>
        /// this method will log out a log in user
        /// </summary>
        /// <param name="email">The user email address</param>
        /// <returns>response - the string "successfully logout" unless an error occures. </returns>
        public string Logout(string email)
        {
            throw new NotImplementedException();
        }      
    }
}
