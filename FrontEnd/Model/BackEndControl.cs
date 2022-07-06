using IntroSE.Kanban.Backend.BusinessLayer;
using IntroSE.Kanban.Backend.ServiceLayer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace FrontEnd.Model
{
    public class BackEndControl
    {
        private Facade facade;
        public BackEndControl()
        {
            facade = new Facade();
            facade.SetUp();
        }
        internal UserModel Register(string username, string password)
        {
            string response = facade.Register(username, password);
            if (response != "{}")
            {
                Response res = ParseJson(response);
                if (res.ErrorOccured())
                {
                    res.Exception();
                }
            }
            return new UserModel(this, username);
        }

        internal List<string> GetAllBoardNames(string email)
        {
            Response response = facade.GetAllBoardNames(email);
            if (response.ErrorOccured())
            {
                return new List<string>();
            }
            return response.ReturnValue as List<string>;            

        }

        internal UserModel Login(string username, string password)
        {
            string response = facade.Login(username, password);
            Response res = ParseJson(response);
            if (res.ErrorOccured())
            {
                /*string temp = response.Substring(response.IndexOf(":") + 1);
                throw new Exception(temp);*/
                res.Exception();
            }
            return new UserModel(this,username);
        }
        internal Response ParseJson(string Json)
        {
            JObject jObject = JObject.Parse(Json);

            try
            {
                return new Response(jObject["ReturnValue"].ToString());
            }
            catch (Exception ex) {
                return new Response(jObject["ErrorMessage"].ToString(),true);
            }
        }
        internal string getColumns(string email,string boardName,int columnOrdinal)
        {
            return facade.GetColumn(email,boardName,columnOrdinal);
        }

        internal void Logout(string email)
        {
            facade.Logout(email);
        }
    }
}
