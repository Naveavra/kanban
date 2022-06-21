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
        log4net.ILog logger = Log.GetLogger();
        private string password;

        /*        public UserDTO? DTO { get; private set; }*/


        public User(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
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
                    throw new Exception("Password is incorrect");
                    //return new Response(email);
                }
                logger.Info("The user " + email + " Logged In Successfully");
                logged = true;
            }
            logger.Warn("Password is incorrect. email: "+email+" has failed to login");
            
        }

        public bool Logged()
        {
            return logged;
        }
        public void Logout()
        {
            if (!Logged())
            {
                logger.Warn($"Error: Cannot logout, User is not logged in. Email: {email}");
                throw new Exception($"Error: Cannot logout, User is not logged in. Email: {email}");
            }
            logged = false;
            logger.Info("User successfully logged out.");
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
