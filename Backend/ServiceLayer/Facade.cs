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

        public bool Loaded { get; private set; }

        UserService userService;
        BoardService boardService;

        public Facade()
        {
            userService = new UserService();
            boardService = new BoardService();
        }
        public string Register(string email, string password)
        {
            /*if (!Loaded)
                return new Response("Havn't loaded the data yet.",true).Serialize();*/
            if (email == null)
            {
                return new Response("Email can't be null",true).Serialize();
            }
            email = email.ToLower();
            return userService.Register(email, password);
        }


        public string Login(string email, string password)
        {
            if (email == null)
            {
                return new Response("Email can't be null",true).Serialize();
            }
            email = email.ToLower();
            return userService.Login(email, password);  
        }


        public string Logout(string email)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            email = email.ToLower();
            return userService.Logout(email);
        }

       
        public string LimitColumn(string email, string boardName, int columnOrdinal, int limit)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            email = email.ToLower();
            return boardService.LimitColumn(email, boardName, columnOrdinal, limit);
        }

       
        public string GetColumnLimit(string email, string boardName, int columnOrdinal)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            email = email.ToLower();
            return boardService.GetColumnLimit(email, boardName, columnOrdinal);
        }



        public string GetColumnName(string email, string boardName, int columnOrdinal)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            email = email.ToLower();
            return boardService.GetColumnName(email, boardName, columnOrdinal);
        }

        public void getReady()
        {
            boardService.getReady();
        }

        public string AddTask(string email, string boardName, string title, string description, DateTime dueDate)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            email = email.ToLower();
            return boardService.AddTask(email, boardName, title, description, dueDate);
        }


        public string UpdateTaskDueDate(string email, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        { 
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            email = email.ToLower();
            return boardService.UpdateTaskDueDate(email, boardName,taskId,columnOrdinal, dueDate);
        }


        public string UpdateTaskTitle(string email, string boardName, int columnOrdinal, int taskId, string title)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            email = email.ToLower();
            return boardService.UpdateTaskTitle(email, boardName,columnOrdinal, taskId, title);
        }


   
        public string UpdateTaskDescription(string email, string boardName, int columnOrdinal, int taskId, string description)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            email = email.ToLower();
            return boardService.UpdateTaskDesc(email, boardName,columnOrdinal, taskId, description);
        }


        public string AdvanceTask(string email, string boardName, int columnOrdinal, int taskId)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            email = email.ToLower();
            return boardService.AdvanceTask(email, boardName,columnOrdinal, taskId);
        }


        public string GetColumn(string email, string boardName, int columnOrdinal)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            email = email.ToLower();
            return boardService.GetColumn(email, boardName, columnOrdinal);
        }



        public string AddBoard(string email, string name)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            email = email.ToLower();
            return boardService.AddBoard(email, name);
        }


        public string RemoveBoard(string email, string name)
        {
            Response response = Validate(email);
            if (response.ErrorOccured())
            {
                return response.Serialize();
            }
            email = email.ToLower();
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
            email = email.ToLower();
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
            try
            {
                return userService.Registered(email);
            }catch (Exception e)
            {
                return false;
            }
        }
        public string LoadData()
        {
            if (!Loaded)
            {
                try
                {
                    boardService.LoadData();
                    userService.LoadData();
                    Loaded = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Failed loading Data in facade");
                    return new Response("Some Error occured while loading the data", true).Serialize();
                }
            }
            return new Response().Serialize();
        }

        public string JoinBoard(string email, int boardID)
        {
            Response r = Validate(email);
            if (r.ErrorOccured())
            {
                return r.Serialize();
            }
            email = email.ToLower();
            return boardService.JoinBoard(email, boardID);
        }

        public string LeaveBoard(string email, int boardID)
        {
            Response r = Validate(email);
            if (r.ErrorOccured())
            {
                return r.Serialize();
            }
            email = email.ToLower();
            return boardService.LeaveBoard(email, boardID);
        }
        public string AssignTask(string email, string boardName, int columnOrdinal, int taskID, string emailAssignee)
        {
            Response r = Validate(email);
            if (!(userService.Logged(email)) || !userService.Registered(emailAssignee))
            {
                Response r2 = new Response("one of the emails isnt correct", true);
                return r2.Serialize();
            }
            emailAssignee = emailAssignee.ToLower();
            email = email.ToLower();
            if (r.ErrorOccured()||!Registered(email) ||! Registered(emailAssignee))
            {
                return new Response("Something went wrong",true).Serialize();
            }
            
            return boardService.AssignTask(email, boardName, columnOrdinal, taskID, emailAssignee);
        }

        public string DeleteData()
        {
            boardService.DeleteData();
            userService.DeleteData();
            return new Response().Serialize();
        }
        public string GetUserBoards(string email)
        {
            Response r = Validate(email);
            if (r.ErrorOccured())
                return r.Serialize();
            email = email.ToLower();
            return boardService.GetBoardIDs(email);

        }
        public string TransferOwnership(string currentOwnerEmail, string newOwnerEmail, string boardName)
        {
            
            Response r = Validate(currentOwnerEmail);
            if (r.ErrorOccured())
                return r.Serialize();
            if(currentOwnerEmail==null|| newOwnerEmail==null || boardName == null)
            {
                return new Response("One of the arguments is null",true).Serialize();
            }
            currentOwnerEmail = currentOwnerEmail.ToLower();
            newOwnerEmail = newOwnerEmail.ToLower();
            return boardService.TransferOwnership(currentOwnerEmail,newOwnerEmail,boardName);
        }
    }
    
}
