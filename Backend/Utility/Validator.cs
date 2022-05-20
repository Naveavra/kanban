using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    internal sealed class Validator
    {
        List<string> Users_Emailes = new List<string>();
        public Validator() { }

        private static Validator instance = null;

        public static Validator Instance { get { return instance ?? (instance = new Validator()); } }

        //title cant be over 50
        public bool ValidateTaskTitle(string title)
        {
           if(title.Length > 50 || title.Length == 0 || String.IsNullOrWhiteSpace(title))
            {
                return false;
            }
            return true;
        }

        internal void reset()
        {
            Users_Emailes.Clear();
        }

        //description can be empty and cant be over 300
        public bool ValidateTaskDesc(string desc)
        {
            if(desc.Length > 300)
            {
                return false;
            }
            return true;
        }

        //checks wether a board name is already taken
/*        private string isBoardNameAvailable(string newBoardName, List<string> boardNames)
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
        }*/

        //checks wether an email has a valid form and unique

        public bool ValidateEmailUsingRegex(string email)
        {
            Regex validateEmailRegex = new Regex(@"^[\w!#$%&'+\-/=?\^_`{|}~]+(\.[\w!#$%&'+\-/=?\^_`{|}~]+)*@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            return validateEmailRegex.IsMatch(email) && UniqueEmail(email);
        }
       
        public string validateEmail(string email)
        {
            if (ContainsAt(email) && ValidEnding(email) && UniqueEmail(email)) 
            {
                return "Great Success";
            }
            return "Email isnt valid";
    
        }
        public bool ContainsAt( string email)
        {
            return email.Contains("@");
        }

        public bool ValidEnding(string email)
        {
            return (email.Contains(".com") || email.Contains(".co.il"));
        }

        public bool UniqueEmail(string email)
        {
            return !Users_Emailes.Contains(email);
        }
/*
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
        }*/

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

        public bool ValidteRegistraion(string email, string password)
        {
            bool valid = false;
            //validateEmail(email).Equals("Great Success")
            if (validatePassword(password).Equals("Great Success") && ValidateEmailUsingRegex(email))
            {
                Users_Emailes.Add(email);
                valid = true;
            }
            return valid;
        }
        
    }
}
