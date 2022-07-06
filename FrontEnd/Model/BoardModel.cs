using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Model
{
    internal class BoardModel : INotifyPropertyChanged
    {
        private UserModel user;
        private string name;

        public ObservableCollection<ColumnModel> Columns{ get; set; }
        public BoardModel(BackEndControl control, UserModel userModel)
        {
            Control = control;
            UserModel = userModel;
            /*Columns = new ObservableCollection<ColumnModel>(controller.GetAllMessagesIds(user.Email).
                Select((c, i) => new ColumnModel(controller, controller.GetMessage(user.Email, i), user)).ToList());
            Messages.CollectionChanged += HandleChange;*/
        }

        public BoardModel(UserModel user, string name)
        {
            this.user = user;
            this.name = name;
           
        }

        public BackEndControl Control { get; }
        public UserModel UserModel { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public override string ToString()
        {
            return name;
        }
    }
}
