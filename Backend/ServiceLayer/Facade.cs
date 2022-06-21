using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class Facade
    {
        private static Facade instance = null;

        public static Facade Instance { get { return instance ?? (instance = new Facade()); } }

        UserService userService;
        BoardService boardService;

        public Facade()
        {
            userService = new UserService();
            boardService = new BoardService();
        }
        public string Register(string email, string password)
        {
            email = email.ToLower();
            if (email == null)
            {
                return new Response("Email can't be null").Serialize();
            }
            return userService.Register(email, password);
        }


        public string Login(string email, string password)
        {
            email = email.ToLower();
            if (email == null)
            {
                return new Response("Email can't be null").Serialize();
            }
            return userService.Login(email, password);  
        }


        public string Logout(string email)
        {
            email = email.ToLower();
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return userService.Logout(email);
        }

       
        public string LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            email = email.ToLower();
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.LimitColumn(email, boardName, columnOrdinal, limit);
        }

       
        public string GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            email = email.ToLower();
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.GetColumnLimit(email, boardName, columnOrdinal);
        }



        public string GetColumnName(string email, string boardName, int columnOrdinal)
        {
            email = email.ToLower();
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.GetColumnName(email, boardName, columnOrdinal);
        }

        public void getReady()
        {
            boardService.getReady();
        }

        public string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            email = email.ToLower();
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.AddTask(email, boardName, title, description, dueDate);
        }


        public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        { 
            email = email.ToLower();
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.UpdateTaskDueDate(email, boardName,taskId,columnOrdinal, dueDate);
        }


        public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            email = email.ToLower();
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.UpdateTaskTitle(email, boardName,columnOrdinal, taskId, title);
        }


   
        public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            email = email.ToLower();
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.UpdateTaskDesc(email, boardName,columnOrdinal, taskId, description);
        }


        public string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            email = email.ToLower();
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.AdvanceTask(email, boardName, taskId);
        }


        public string GetColumn(string email, string boardName, int columnOrdinal)
        {
            email = email.ToLower();
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.GetColumn(email, boardName, columnOrdinal);
        }



        public string AddBoard(string email, string name)
        {
            email = email.ToLower();
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            return boardService.AddBoard(email, name);
        }


        public string RemoveBoard(string email, string name)
        {
            email = email.ToLower();
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
            email = email.ToLower();
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
        internal bool Registered(string email)
        {
            return userService.Registered(email);
        }
        public string LoadData()
        {
            try
            {
                boardService.LoadData();
                userService.LoadData();
                return new Response("{}").Serialize();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Failed loadingData in facade");
                return new Response("Some Error occured while loading the data", true).Serialize();
            }
        }

        public string JoinBoard(string email, int boardID)
        {
            email = email.ToLower();
            Response r = Validate(email);
            if (r.ErrorOccured())
            {
                return r.Serialize();
            }
            return boardService.JoinBoard(email, boardID);
        }

        public string LeaveBoard(string email, int boardID)
        {
            email = email.ToLower();
            Response r = Validate(email);
            if (r.ErrorOccured())
            {
                return r.Serialize();
            }
            return boardService.LeaveBoard(email, boardID);
        }
        public string AssignTask(string email, string boardName, int columnOrdinal, int taskID, string emailAssignee)
        {
            email = email.ToLower();
            emailAssignee = emailAssignee.ToLower();
            Response r = Validate(email);
            if (r.ErrorOccured()||!Registered(email) ||! Registered(emailAssignee))
            {
                return r.Serialize();
            }
            return boardService.AssignTask(email, boardName, columnOrdinal, taskID, emailAssignee);
        }

        public string DeleteData()
        {
            boardService.DeleteData();
            userService.DeleteData();
            return new Response("").Serialize();
        }
        public string GetUserBoards(string email)
        {
            email = email.ToLower();
            Response r = Validate(email);
            if (r.ErrorOccured())
                return r.Serialize();
            return boardService.GetBoardIDs(email);

        }
        public string TransferOwnership(string currentOwnerEmail, string newOwnerEmail, string boardName)
        {
            currentOwnerEmail = currentOwnerEmail.ToLower();
            newOwnerEmail = newOwnerEmail.ToLower();
            Response r = Validate(currentOwnerEmail);
            if (r.ErrorOccured())
                return r.Serialize();
            return boardService.TransferOwnership(currentOwnerEmail,newOwnerEmail,boardName);
        }
    }
    
}
