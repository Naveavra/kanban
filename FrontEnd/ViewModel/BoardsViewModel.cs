using FrontEnd.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.ViewModel
{
    internal class BoardsViewModel
    {
        BackEndControl Control;
        UserModel User;
        public string Title { get; set; }
        public ObservableCollection<BoardModel> Boards { get; set; }
        /*        public ObservableCollection<string> Boards { get; set; }
        */
        public ObservableCollection<ColumnModel> Columns { get; set; }

        public BoardsViewModel(UserModel user)
        {
            this.User = user;
            this.Control = user.Control;
            
            Boards = new ObservableCollection<BoardModel>();
/*            Boards = new ObservableCollection<string>();
*/            foreach (string name in Control.GetAllBoardNames(user.Email))
            {
                Boards.Add(new BoardModel(user,name));
            }
        }
        public void getColumns(string boardName)
        {
            Columns = new ObservableCollection<ColumnModel>();
            string response1 = Control.getColumns(User.Email,boardName,0);
            string response2 = Control.getColumns(User.Email,boardName,1);
            string response3 = Control.getColumns(User.Email,boardName,2);
            ColumnModel c1 = new ColumnModel("Backlog", response1);
            ColumnModel c2 = new ColumnModel("InProgress", response2);
            ColumnModel c3 = new ColumnModel("Done", response3);
            Columns.Add(c1);
            Columns.Add(c2);
            Columns.Add(c3);
        }

        internal void Logout(string email)
        {
            Control.Logout(email);
        }
    }
}
