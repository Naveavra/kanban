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
        private string email;
        private string password;
        
        public User(string email,string password)
        {
            this.email = email; 
            this.password = password; 
        }
        public void Login()
        {
            logged = true;
        }

        public bool Logged()
        {
            return logged;
        }
        public void Logout()
        {
            logged = false;
        }
        // checks if a new password is different from previous one.
        public string CheckNewPW(string newPW)
        {
            if(newPW == password)
            {
                return "passwords are the same";
            }
            password = newPW;
            return "Success";
         
        }
        public string getPassword()
        {
            return password;
        }   
    }
}
