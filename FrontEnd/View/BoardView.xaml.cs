using FrontEnd.Model;
using FrontEnd.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class BoardView : Window
    {
        private UserModel User;
        private BoardsViewModel viewModel;
        private string currBoard;
        

        public BoardView(UserModel user)
        {
            viewModel = new BoardsViewModel(user);
            User = user;
            DataContext = viewModel;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string boardName = BoardList.SelectedItem.ToString();
            viewModel.getColumns(boardName);
            Column1.ItemsSource = viewModel.Columns[0].Tasks;
            Column2.ItemsSource = viewModel.Columns[1].Tasks;
            Column3.ItemsSource = viewModel.Columns[2].Tasks;

        }

        private void Boards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string boardName = BoardList.SelectedItem.ToString();
            currBoard = boardName;
            viewModel.getColumns(boardName);
            Column1.ItemsSource = viewModel.Columns[0].Tasks;
            Column2.ItemsSource = viewModel.Columns[1].Tasks;
            Column3.ItemsSource = viewModel.Columns[2].Tasks;
        }

        private void Column1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string temp = Column1.SelectedItem.ToString();
            string taskID = temp.Substring(temp.IndexOf(':') + 1);
            TaskModel model = viewModel.Columns[0].GetTask(taskID);
            if (model != null)
            {
                TaskView v = new TaskView(model,User);
                v.TaskV.Text = v.TaskViewModel.TaskDescription;  
                v.Show();
                this.Close();
            }
        }

        private void Column2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string temp = Column2.SelectedItem.ToString();
            string taskID = temp.Substring(temp.IndexOf(':') + 1);
            TaskModel model = viewModel.Columns[1].GetTask(taskID);
            if (model != null)
            {
                TaskView v = new TaskView(model,User);
                v.TaskV.Text = v.TaskViewModel.TaskDescription;
                v.Show();
                this.Close();
            }
        }

        private void Column3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string temp = Column3.SelectedItem.ToString();
            string taskID = temp.Substring(temp.IndexOf(':') + 1);
            TaskModel model = viewModel.Columns[2].GetTask(taskID);
            if (model != null)
            {
                TaskView v = new TaskView(model,User);
                v.TaskV.Text = v.TaskViewModel.TaskDescription;
                v.Show();
                this.Close();
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Logout(User.Email);
            new MainWindow().Show();
            this.Close();
        }
    }
}
