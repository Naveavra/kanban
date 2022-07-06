using FrontEnd.Model;
using FrontEnd.View;
using FrontEnd.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;
        public MainWindow()
        {
            DataContext = new MainViewModel();
            viewModel = (MainViewModel)DataContext;
            InitializeComponent();
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Password = passwordBox.Password;
            UserModel user = viewModel.Register();
            if (user != null)
            {
                BoardView bView = new BoardView(user);
                bView.Show();
                this.Close();
            }
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Password = passwordBox.Password;
            UserModel user = viewModel.Login();
            if(user != null)
            {
                BoardView bView = new BoardView(user);
                bView.Show();
                this.Close();
            }
        }

    }
}
