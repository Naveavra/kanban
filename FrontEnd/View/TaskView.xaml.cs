using FrontEnd.Model;
using FrontEnd.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FrontEnd.View
{
    /// <summary>
    /// Interaction logic for TaskView.xaml
    /// </summary>
    public partial class TaskView : Window
    {
        public UserModel User { get; set; }
        public TaskViewModel TaskViewModel;
        public TaskView(TaskModel task, UserModel user)
        {
            User = user;
            TaskViewModel = new TaskViewModel(task);
            DataContext = TaskViewModel;
            InitializeComponent();
        }

        private void Closing_Event(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new BoardView(User).Show();
            
        }
    }
}
