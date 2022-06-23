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
    internal class User
    {
        private bool logged;
        public string email { get; set; }
        private string password;

        /*        public UserDTO? DTO { get; private set; }*/


        public User(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        /// <summary>
        /// login
        /// </summary>
        /// <param name="pw"></param>
        /// <exception cref="Exception"></exception> user is already logged or doesnt exist
        public void Login(string pw) 
        {
            if (Logged())
            {
                throw new Exception("User is already logged in");
            }
            else
            {
                if (!(password == pw))
                {
                    throw new Exception("Password is incorrect. email: " + email + " has failed to login");
                }
                logged = true;
            }
            
        }

        ///is the user logged or not
        public bool Logged()
        {
            return logged;
        }
        public void Logout()
        {
            if (!Logged())
            {
                throw new Exception($"Error: Cannot logout, User is not logged in. Email: {email}");
            }
            logged = false;
        }
        // checks if a new password is different from previous one.
        public string CheckNewPW(string newPW)
        {
            if (newPW == password)
            {
                return "passwords are the same";
            }
            password = newPW;
/*            DTO.password = newPW;
*/            return "Success";

        }

        /// <summary>
        /// returns the user password
        /// </summary>
        /// <returns></returns>
        public string getPassword()
        {
            return password;
        }

        /*        public void setDTO(UserDTO Userdto)
                {
                    DTO = Userdto;
                }
            }*/
    }
}
