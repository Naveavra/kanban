using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    internal class Kaki_Pipi
    {
        private static Kaki_Pipi instance = null;

        public static Kaki_Pipi Instance { get { return instance ?? (instance = new Kaki_Pipi()); } }

        UserService userService;
        BoardService boardService;

        public Kaki_Pipi()
        {
            userService = new UserService();
            boardService = new BoardService();
        }
        public string Register(string email, string password)
        {
            if (email == null)
            {
                return new Response("Email can't be null").Serialize();
            }
            return userService.Register(email, password);
        }


        public string Login(string email, string password)
        {
            if (email == null)
            {
                return new Response("Email can't be null").Serialize();
            }
            return userService.Login(email, password);  
        }


        public string Logout(string email)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return userService.Logout(email);
        }

       
        public string LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.LimitColumn(email, boardName, columnOrdinal, limit);
        }

       
        public string GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.GetColumnLimit(email, boardName, columnOrdinal);
        }



        public string GetColumnName(string email, string boardName, int columnOrdinal)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.GetColumnName(email, boardName, columnOrdinal);
        }


        public string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.AddTask(email, boardName, title, description, dueDate);
        }


        public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.UpdateTaskDueDate(email, boardName,taskId, dueDate);
        }


        public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.UpdateTaskTitle(email, boardName, taskId, title);
        }


   
        public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.UpdateTaskDesc(email, boardName, taskId, description);
        }


        public string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.AdvanceTask(email, boardName, taskId);
        }


        public string GetColumn(string email, string boardName, int columnOrdinal)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.GetColumn(email, boardName, columnOrdinal);
        }



        public string AddBoard(string email, string name)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.AddBoard(email, name);
        }


        public string RemoveBoard(string email, string name)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.RemoveBoard(email, name);
        }


        /// <summary>
        /// This method returns all the In progress tasks of the user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <returns>Response with  a list of the in progress tasks, unless an error occurs (see <see cref="GradingService"/>)</returns>
        public string InProgressTasks(string email)
        {            
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
                
            return boardService.InProgressTasks(email);
        }
        
        internal Response makeError(string msg)
        {
            return new Response(msg, true);
/*            return JsonConvert.SerializeObject(r, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
*/        }
        internal Response makeOk(object val)
        {
            return new Response(val);
        }

        internal Response Validate(String email)
        {
            email = email.ToLower();
            if(email == null)
            {
                return makeError("Email can't be null.");
            }
            else if (!userService.Logged(email))
            {
                return makeError("User doesn't exist or not logged in");
            }
           return makeOk(true);
        }
    }
    
}
