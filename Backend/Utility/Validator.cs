using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
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
            string s = email.Substring(0,email.IndexOf("@"));
            string s1 = email.Substring(email.IndexOf("@") + 1);
            Regex validateEmailRegex = new Regex(@"^[\w!#$%&'+\-/=?\^_`{|}~]+(\.[\w!#$%&'+\-/=?\^_`{|}~]+)*@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
            Regex validateEmailRegex2 = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            Regex validateEmailRegex3 = new Regex(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$");
            Regex validateEmailRegex4 = new Regex(@"[^\x00-\x80]+");
            string[] lst = s1.Split(".");
            bool test_regex2 = true;
            for (int i = 0; i < lst.Length; i++)
            {
                if (!Regex.IsMatch(lst[i], @"^[a-zA-Z]+$"))
                {
                    test_regex2 = false; 
                }
            }
            
            bool test_regex = validateEmailRegex4.IsMatch(s);
            if (!(validateEmailRegex.IsMatch(email) || validateEmailRegex2.IsMatch(email) || validateEmailRegex3.IsMatch(email)) || s.Count() > 64 || s1.Contains("_") || !NotContainsHebrew(email) ||test_regex|| !test_regex2 ||email.Count()==0)
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
            if (Users_Emailes.ContainsKey(email)/* || mapper.Contains(email)*/)
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
            if(string.IsNullOrWhiteSpace(password)||string.IsNullOrEmpty(password) || !(password.Any(char.IsUpper))|| !(password.Any(char.IsLower))|| !(password.Any(char.IsDigit)) || password.Contains(" ") || !NotContainsHebrew(password))
            {
                throw new Exception("Invalid Password input");
            }
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
                if (char.IsLower(c))
                    lowerLetter = true;
                else if (char.IsUpper(c))
                    upperLetter = true;
                else if (char.IsDigit(c)) { number = true; }
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
        public bool NotContainsHebrew(string email)
        {
            char[] chars = "אבגדהוזחטיכלמנסעפצקרשת".ToCharArray();
            foreach (char c in email.ToCharArray())
            {
                if (chars.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }
        public bool test1(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public void ValidateRegistration(string email, string password)
        {
            //validateEmail(email).Equals("Great Success")

            if (validatePassword(password).Equals("Great Success") && ValidateEmailUsingRegex(email) && UniqueEmail(email) && !email.Contains(" "))
            {
                User user = new User(email, password);
                Users_Emailes.Add(email, user);
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
