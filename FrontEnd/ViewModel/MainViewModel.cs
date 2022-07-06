using FrontEnd.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FrontEnd.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private BackEndControl Control;
        private string? _userName;
        public string Username
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged();
                }
            }
        }
        private string? _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged();
                }
            }
        }
        private string? msg;
        public string Message
        {
            get { return msg; }
            set
            {
                if (msg != value)
                {
                    msg = value;
                    OnPropertyChanged();
                }
            }
        }
        public MainViewModel()
        {
            Control = new BackEndControl();
        }
        internal UserModel Register()
        {
            try
            {
                UserModel m = Control.Register(Username, Password);
                return m;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return null;
            }
        }
        internal UserModel Login()
        {
            Message = "";
            try
            {
                return Control.Login(Username, Password);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return null;
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
