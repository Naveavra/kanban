using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    internal sealed class Validator
    {
        public Validator() { }

        private static Validator instance = null;

        public static Validator Instance { get { return instance ?? (instance = new Validator()); } }

        //title cant be over 50
        private string ValidateTaskTitle(string title)
        {
           if(title.Length > 50 && title.Length == 0)
            {
                return "Task title is not legal";
            }
            return "Success";
        }

        //description can be empty and cant be over 300
        private string ValidateTaskDesc(string desc)
        {
            if(desc.Length > 300)
            {
                return "Description is too long";
            }
            return "Success";
        }

        //checks wether a board name is already taken
        private string isBoardNameAvailable(string newBoardName, List<string> boardNames)
        {
            string res = "";
            foreach(string boardName in boardNames)
            {
                if (boardNames.Contains(newBoardName))
                {
                    res = "Board name is allready taken, please choose another name G";
                }
                else
                {
                    boardNames.Add(newBoardName);
                    res = "success";
                }
            }
            return res;
        }

        //checks wether an email has a valid form and unique

       
        public string validateEmail(string email)
        {
            if (!(email.Contains("@") && (email.Contains(".co.il") || email.Contains(".com")))) 
            {
                return "email isn't valid";
            }
            return "Great Success";
    
        }

        public string EmailIsUnique(string email, List<string> AllEmails)
        {
           if (AllEmails.Contains(email))
            {
                return "email is not unique";
            }
           else
            {
                return "success";
            }
        }

        //nust include atleast 1 upper, lower, number, length between 6-20
        private string validatePassword(string password)
        {
            if(password.Length > 20 || password.Length < 6)
            {
                return "Enter a valid password";
            }
            bool upperLetter = false;
            bool lowerLetter = false;
            bool number = false;
            for (int i = 0; i < password.Length; i++)
            {
                char c = password[i];
                if (c >= 'a' && c <= 'z')
                    lowerLetter = true;
                else if (c >= 'A' && c <= 'Z')
                    upperLetter = true;
                else if(c >= '0' && c <= '9')
                    number = true;
            }
            if(!upperLetter || !lowerLetter || !number)
            {
                return "Password missing a lower letter/upper letter\number";
            }
            else
            {
                return "Great Success";
            }
        } 
        
    }
}
