using IntroSE.Kanban.Backend.DataAccessLayer;
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
        private Dictionary<string,User> Users_Emailes = new Dictionary<string, User>();
        //Magic Numbers
        private int maxTitleLen = 50;
        private int minTitleLen = 0;
        private int maxDescLen = 300;
        private int maxPwLen = 20;
        private int minPwLen = 6;
        public Validator() { }

        private static Validator instance = null;

        public static Validator Instance { get { return instance ?? (instance = new Validator()); } }

        //title cant be over 50
        public void ValidateTaskTitle(string title)
        {
           if(title.Length > maxTitleLen || title.Length == minTitleLen|| String.IsNullOrWhiteSpace(title))
            {
                throw new Exception("Title input invalid.");
            }
        }

        internal void reset()
        {
            Users_Emailes.Clear();
        }

        //description can be empty and cant be over 300
        public void ValidateTaskDesc(string desc)
        {
            if(desc.Length > maxDescLen)
            {
                throw new Exception("Description is too long.");
            }
            
        }
        public void insertUsersFromDB(Dictionary<string,User> lst)
        {
            Users_Emailes = lst;
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
            if (!validateEmailRegex.IsMatch(email))
                throw new Exception("Email isn't valid");
            return validateEmailRegex.IsMatch(email);
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
            UserMapper mapper = new UserMapper();
            if (Users_Emailes.ContainsKey(email) || mapper.Contains(email))
                throw new Exception("Email isn't unique.");
            return !Users_Emailes.ContainsKey(email);
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
            if(password.Length > maxPwLen || password.Length < minPwLen)
            {
                throw new Exception("Password's length is incorrect, it must be between 6 and 20 letters.");
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
                throw new Exception("Password missing a lower letter/upper letter/number");
            }
            else
            {
                return "Great Success";
            }
        } 

        public void ValidateRegistraion(string email, string password)
        {
            //validateEmail(email).Equals("Great Success")
            if (validatePassword(password).Equals("Great Success") && ValidateEmailUsingRegex(email) && UniqueEmail(email))
            {
                User user =  new User(email, password);
                Users_Emailes.Add(email,user);
                return;
            }
            throw new Exception("invalid registraion");
            
        }
        /*internal bool Logged(string email)
        {
            if (Users_Emailes.ContainsKey(email))
            {
                return Users_Emailes[email].Logged();
            }
            else
            {
                return false;
            }
        }*/
    }
}
