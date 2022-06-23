using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IntroSE.Kanban.Backend.ServiceLayer
{
    internal class Test
    {

        /*static void Main(string[] args)
        {
            *//*UserService userService = new UserService();
            BoardService boardService = new BoardService();*//*
            RunTests();

        }*/
        int counter = 0;
        /*        UserService facade = new UserService();
        *//*        BoardService fc = new BoardService();
        */
        Facade facade = Facade.Instance;
        public void RunTests()
        {
            GetReady();
            Console.WriteLine("-------- Login/Logout/Registration Tests ---------\n");
            UserOpTests();
            Console.WriteLine("-------- Board Tests ---------\n");
            BoardTests();
            Console.WriteLine("-------- Task Tests ---------\n");
            taskTests();
            Console.WriteLine("-------- Column Tests ---------\n");
            ColumnTests();
            Console.WriteLine("-------- Delete/Load Data Tests ---------\n");
            DeleteLoadTest();
            Console.WriteLine("-------- Assignment Tests ---------\n");
            AssignTest();
        }
        private void DeleteLoadTest()
        {
            facade.DeleteData();
            //facade.DeleteData();//clearing all lists in ram
            facade.DeleteData();
            facade.DeleteData();
            facade.LoadData();
            facade.LoadData();
            facade.Register("dan96@gmail.com", "Aa123456");
            facade.Register("miki96@gmail.com", "Aa123456");
            facade.Login("miki96@gmail.com", "Aa123456");
            facade.Login("dan96@gmail.com", "Aa123456");
            facade.AddBoard("miki96@gmail.com", "nitutz");
            facade.AddBoard("miki96@gmail.com", "nituz2");
            facade.Register("nadia97@gmail.com", "Aa123456");
            facade.Register("nave95@gmail.com", "Aa123456");
            facade.Login("nadia97@gmail.com", "Aa123456");
            facade.AddBoard("nadia97@gmail.com", "cola");
            facade.JoinBoard("nadia97@gmail.com", 0);
            facade.AddTask("miki96@gmail.com", "nitutz", "AssignedTask", "lets get this shit over with", new DateTime());
            facade.AddTask("nadia97@gmail.com", "cola", "newTask", "lets get this shit over with", new DateTime());
            facade.LoadData();
        }
        private void GetReady()
        {
            Console.WriteLine("lets go");
            facade.getReady();
        }
        private void ColumnTests()
        {
            Console.WriteLine(counter.ToString() + " valid GetColumn");
            Console.WriteLine(counter.ToString() + "." + facade.GetColumn("naveav95@gmail.com", "nitutz", 2)); //35 Get column
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " invalid because there is more tasks than the limit");
            Console.WriteLine(counter.ToString() + "." + facade.LimitColumn("naveav95@gmail.com", "nitutz", 2, 1));//37 invalid because there is more tasks than the limit
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid because there is less tasks than the limit");
            Console.WriteLine(counter.ToString() + "." + facade.LimitColumn("naveav95@gmail.com", "nitutz", 0, 5));//38 valid because there is more tasks than the limit
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid ,5");
            Console.WriteLine(counter.ToString() + "." + facade.GetColumnLimit("naveav95@gmail.com", "nitutz", 0));//39 valid 5
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid columnID doesnt exist");
            Console.WriteLine(counter.ToString() + "." + facade.GetColumnLimit("naveav95@gmail.com", "nitutz", 20));//40 not valid columnID doesnt exist 
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "valid, -1");
            Console.WriteLine(counter.ToString() + "." + facade.GetColumnLimit("naveav95@gmail.com", "nitutz", 1));//41 valid -1 
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "valid, Done");
            Console.WriteLine(counter.ToString() + "." + facade.GetColumnName("naveav95@gmail.com", "nitutz", 2));//42 valid done
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid column id doesnt exist");
            Console.WriteLine(counter.ToString() + "." + facade.GetColumnName("naveav95@gmail.com", "nitutz", 8));//43 not valid column id doesnt exist
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid join board");
            Console.WriteLine(counter.ToString() + "." + facade.JoinBoard("naveav96@gmail.com", 1));//43 not valid column id doesnt exist
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid get column limit");
            Console.WriteLine(counter.ToString() + "." + facade.GetColumnLimit("naveav96@gmail.com","nitutz", 0));//43 not valid column id doesnt exist
            Console.WriteLine("-----------------------\n");
            counter++;
            facade.Login("danhakatan@gmail.com", "Bb123456");
            Console.WriteLine(counter.ToString() + " Invalid get column limit");
            Console.WriteLine(counter.ToString() + "." + facade.GetColumnLimit("danhakatan@gmail.com", "nitutz", 0));//43 not valid column id doesnt exist
            Console.WriteLine("-----------------------\n");
            counter++;
            facade.LeaveBoard("naveav96@gmail.com", 1);
            Console.WriteLine(counter.ToString() + " Invalid get column limit");
            Console.WriteLine(counter.ToString() + "." + facade.GetColumnLimit("naveav96@gmail.com", "nitutz", 0));//43 not valid column id doesnt exist
            Console.WriteLine("-----------------------\n");
            AssignTest();


        }
        private void AssignTest()
        {
            facade.DeleteData();
            facade.Register("dan96@gmail.com", "Aa123456");
            facade.Register("miki96@gmail.com", "Aa123456");
            facade.AddBoard("miki96@gmail.com", "nitutz");
            facade.Register("naveav95@gmail.com", "Aa1234");
            facade.Register("nadia97@gmail.com", "Aa123456");
            facade.AddBoard("nadia97@gmail.com", "cola");
            facade.JoinBoard("nadia97@gmail.com", 0);
            facade.AddTask("miki96@gmail.com", "nitutz", "AssignedTask", "lets get this shit over with", new DateTime());
            facade.AddTask("nadia97@gmail.com", "cola", "newTask", "lets get this shit over with", new DateTime());
            facade.Logout("miki96@gmail.com");
            Console.WriteLine(counter.ToString() + "not valid AssignTask, miki isnt logged in");
            Console.WriteLine(counter.ToString() + "." + facade.AssignTask("miki96@gmail.com", "nitutz", 0, 0, "nadia97@gmail.com"));
            Console.WriteLine("-----------------------\n");
            counter++;
            facade.Login("miki96@gmail.com", "Aa123456");
            Console.WriteLine(counter.ToString() + " valid AssignTask");
            Console.WriteLine(counter.ToString() + "." + facade.AssignTask("miki96@gmail.com", "nitutz", 0, 0, "nadia97@gmail.com"));
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " Not valid AssignTask, user doesnt exist");
            Console.WriteLine(counter.ToString() + "." + facade.AssignTask("naveav95@gmail.com", "nitutz", 0, 0, "danhakatan@gmail.com"));
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid, get user boards");
            Console.WriteLine(counter.ToString() + "." + facade.GetUserBoards("nadia97@gmail.com"));
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " Not valid AssignTask, same same");
            Console.WriteLine(counter.ToString() + "." + facade.AssignTask("nadia97@gmail.com", "nitutz", 0, 0, "nadia97@gmail.com"));
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " Not valid AssignTask, user hasn't join the board");
            Console.WriteLine(counter.ToString() + "." + facade.AssignTask("nadia97@gmail.com", "nitutz", 0, 0, "dan96@gmail.com"));
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " Not valid AssignTask, this user isnt reaponsible for the task");
            Console.WriteLine(counter.ToString() + "." + facade.AssignTask("miki96@gmail.com", "nitutz", 0, 0, "nadia97@gmail.com"));
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid join board");
            Console.WriteLine(counter.ToString() + "." + facade.JoinBoard("naveav95@gmail.com", 0));
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid join board, board doesnt exist");
            Console.WriteLine(counter.ToString() + "." + facade.JoinBoard("naveav95@gmail.com", 1000000));
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid leave board");
            Console.WriteLine(counter.ToString() + "." + facade.LeaveBoard("naveav95@gmail.com", 0));
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " invalid transfer ownership, nave isnt part of the board");
            Console.WriteLine(counter.ToString() + "." + facade.TransferOwnership("miki96@gmail.com", "naveav95@gmail.com", "nitutz"));
            Console.WriteLine("-----------------------\n");
            counter++;
            facade.JoinBoard("naveav95@gmail.com", 0);
            Console.WriteLine(counter.ToString() + " valid transfer ownership");
            Console.WriteLine(counter.ToString() + "." + facade.TransferOwnership("miki96@gmail.com", "naveav95@gmail.com", "nitutz"));
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid transfer ownership, miki isnt the owner");
            Console.WriteLine(counter.ToString() + "." + facade.TransferOwnership("miki96@gmail.com", "nadia97@gmail.com", "nitutz"));
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "valid add board");
            Console.WriteLine(counter.ToString() + "." + facade.AddBoard("naveav95@gmail.com", "board2"));
            Console.WriteLine("-----------------------\n");
            counter++;
            facade.AddTask("naveav95@gmail.com", "board2", "task2", "im tired", new DateTime());
            facade.JoinBoard("miki96@gmail.com", 2);
            facade.JoinBoard("nadia97@gmail.com", 2);
            facade.AddTask("nadia97@gmail.com", "board2", "task3", "im also tired", new DateTime());
            Console.WriteLine(counter.ToString() + "valid assign task");
            Console.WriteLine(counter.ToString() + "." + facade.AssignTask("nadia97@gmail.com", "board2", 0, 0, "miki96@gmail.com"));
            Console.WriteLine("-----------------------\n");
            Console.WriteLine(counter.ToString() + "Invalid assign task");
            Console.WriteLine(counter.ToString() + "." + facade.AssignTask("naveav95@gmail.com", "board2", 0, 0, "miki96@gmail.com"));
            Console.WriteLine("-----------------------\n");
            Console.WriteLine(facade.AddTask("nadia97@gmail.com", "board2", "task1", "dd", new DateTime()));
            Console.WriteLine(facade.AdvanceTask("nadia97@gmail.com", "board2", 0, 1));
            Console.WriteLine(facade.AdvanceTask("naveav95@gmail.com", "board2", 0, 1));
            Console.WriteLine(facade.AssignTask("naveav95@gmail.com", "board2", 1, 1, "nadia97@gmail.com"));
            Console.WriteLine(facade.AdvanceTask("miki96@gmail.com", "board2", 0, 1));
            Console.WriteLine(facade.AssignTask("miki96@gmai.com", "board2", 1, 0, "nadia97@gmail.com"));
            Console.WriteLine(facade.InProgressTasks("nadia97@gmail.com"));


            facade.DeleteData(); 
            //loading data to the tables again
            facade.Register("dan96@gmail.com", "Aa123456");
            facade.Register("miki96@gmail.com", "Aa123456");
            facade.Login("miki96@gmail.com", "Aa123456");
            facade.Login("dan96@gmail.com", "Aa123456");
            facade.AddBoard("miki96@gmail.com", "nitutz");
            facade.AddBoard("miki96@gmail.com", "nituz2");
            facade.Register("nadia97@gmail.com", "Aa123456");
            facade.Register("nave95@gmail.com", "Aa123456");
            facade.Login("nadia97@gmail.com", "Aa123456");
            facade.AddBoard("nadia97@gmail.com", "cola");
            facade.JoinBoard("nadia97@gmail.com", 0);
            facade.AddTask("miki96@gmail.com", "nitutz", "AssignedTask", "lets get this shit over with", new DateTime());
            facade.AddTask("nadia97@gmail.com", "cola", "newTask", "lets get this shit over with", new DateTime());
            facade.AssignTask("miki96@gmail.com", "nitutz", 0, 0, "Nadia97@gmail.com");
            Console.WriteLine(counter.ToString() + " not valid transfer ownership");
            Console.WriteLine(counter.ToString() + "." + facade.TransferOwnership("nave95@gmail.com", "miki96@gmail.com", "nitutz"));
            Console.WriteLine("-----------------------\n");

            /*    facade.JoinBoard("nave95@gmail.com", 0);//nave joining the nitutz board
                facade.AddTask("nave95@gmail.com", "nitutz", "mikiyahu", "halas tzahi", new DateTime());*/
            //Update Task DueDate
            Console.WriteLine(counter.ToString() + " valid update task due");
            Console.WriteLine(counter.ToString() + "." + facade.UpdateTaskDueDate("nadia97@gmail.com", "nitutz", 0, 0, DateTime.Today.AddDays(1)));
            Console.WriteLine("-----------------------\n");
            counter++;
            facade.JoinBoard("dan96@gmail.com", 0);
            Console.WriteLine(counter.ToString() + " not valid update task due because user is not assigned");
            Console.WriteLine(counter.ToString() + "." + facade.UpdateTaskDueDate("dan96@gmail.com", "nitutz", 0, 0, DateTime.Today.AddDays(2.5)));
            Console.WriteLine("-----------------------\n");
            counter++;
            //Update Task Title
            Console.WriteLine(counter.ToString() + " valid update task title");
            Console.WriteLine(counter.ToString() + "." + facade.UpdateTaskTitle("nadia97@gmail.com", "nitutz", 0, 0, "NewTitleNitutz"));
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid update task title, user not assigned");
            Console.WriteLine(counter.ToString() + "." + facade.UpdateTaskTitle("dan96@gmail.com", "nitutz", 0, 0, "NewTitleNitutz2"));
            Console.WriteLine("-----------------------\n");
            counter++;
            //Update Task Description
            Console.WriteLine(counter.ToString() + " valid update task description");
            Console.WriteLine(counter.ToString() + "." + facade.UpdateTaskDescription("nadia97@gmail.com", "nitutz", 0, 0, "new description"));
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid update task description, user not assigned");
            Console.WriteLine(counter.ToString() + "." + facade.UpdateTaskDescription("dan96@gmail.com", "nitutz", 0, 0, "new description2"));
            Console.WriteLine("-----------------------\n");
            counter++;
            //AdvanceTask
            Console.WriteLine(counter.ToString() + " valid AdvanceTask");
            Console.WriteLine(counter.ToString() + "." + facade.AdvanceTask("nadia97@gmail.com", "nitutz", 0, 0));
            counter++;
            Console.WriteLine("-----------------------\n");
            Console.WriteLine(counter.ToString() + " Not valid AdvanceTask, user not assigned");
            Console.WriteLine(counter.ToString() + "." + facade.AdvanceTask("dan96@gmail.com", "nitutz", 0, 0));
            counter++;
            Console.WriteLine("-----------------------\n");
            //re assign and check if something works (if one update works all other should)
            facade.AssignTask("nadia97@gmail.com", "nitutz", 1, 0, "dan96@gmail.com");
            facade.LeaveBoard("nadia97@gmail.com", 0);
            Console.WriteLine(counter.ToString() + " valid update task due");
            Console.WriteLine(counter.ToString() + "." + facade.UpdateTaskDueDate("dan96@gmail.com", "nitutz", 1, 0, DateTime.Today.AddDays(3)));
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " Not valid update task due, user isnt assigned anymore");
            Console.WriteLine(counter.ToString() + "." + facade.UpdateTaskDueDate("nadia97@gmail.com", "nitutz", 1, 0, DateTime.Today.AddDays(2)));
            Console.WriteLine("-----------------------\n");
            //check persistent id's //can only be checked through DB view, not for testing purposes
            facade.AddBoard("nadia97@gmail.com", "cola2");
            facade.AddTask("nadia97@gmail.com", "cola2", "newTask2", "shit", new DateTime());
            facade.AddTask("nadia97@gmail.com", "cola", "newTask2", "shit", new DateTime());

            //try to add tests from owner and not owner
            facade.AddTask("miki96@gmail.com", "nitutz", "task-0", "noDescription", new DateTime());
            facade.AddTask("dan96@gmail.com", "nitutz", "task-1", "noDescription", new DateTime());


        }
        private void taskTests()
        {
            Console.WriteLine(counter.ToString() + " Invalid addtask, user isnt logged in");
            Console.WriteLine(counter.ToString() + "." + facade.AddTask("danhakatan@gmail.com", "nitutz", "Task0", "lets get this shit over with", new DateTime())); //16 valid addtask
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid addtask");
            Console.WriteLine(counter.ToString() + "." + facade.AddTask("naveav95@gmail.com", "nitutz", "Task0", "lets get this shit over with", new DateTime())); //16 valid addtask
            Console.WriteLine("-----------------------\n");
            counter++;
            facade.AssignTask("naveav95@gmail.com", "nitutz", 0, 0, "naveav95@gmail.com"); //task assignment
            Console.WriteLine(counter.ToString() + " not valid addtask because there is no title");
            Console.WriteLine(counter.ToString() + "." + facade.AddTask("naveav95@gmail.com", "nitutz", "", "lets get this shit over with2", new DateTime())); //17 not valid addtask because there is no title
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid addtask because the title is only white spaces");
            Console.WriteLine(counter.ToString() + "." + facade.AddTask("naveav95@gmail.com", "nitutz", "    ", "lets get this shit over with2", new DateTime())); //18 not valid addtask because the title is only white spaces
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid update task due");
            Console.WriteLine(counter.ToString() + "." + facade.UpdateTaskDueDate("naveav95@gmail.com", "nitutz", 0, 0, DateTime.Today.AddDays(2.5))); //19 valid update task due
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid update task due because taskid isnt correct");
            Console.WriteLine(counter.ToString() + "." + facade.UpdateTaskDueDate("naveav95@gmail.com", "nitutz", 5, 5, DateTime.Today.AddDays(2.5))); //20 not valid update task due because taskid isnt correct
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid update task title");
            Console.WriteLine(counter.ToString() + "." + facade.UpdateTaskTitle("naveav95@gmail.com", "nitutz", 0, 0, "NewTitleNitutz")); //21 valid update task title
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid update task title id doesnt exist");
            Console.WriteLine(counter.ToString() + "." + facade.UpdateTaskTitle("naveav95@gmail.com", "nitutz", 5, 5, "NewTitleNitutz")); //22 not valid update task title id doesnt exist
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid update task description");
            Console.WriteLine(counter.ToString() + "." + facade.UpdateTaskDescription("naveav95@gmail.com", "nitutz", 0, 0, "new description")); //23 valid update task description
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid update task description task id doesnt exist");
            Console.WriteLine(counter.ToString() + "." + facade.UpdateTaskDescription("naveav95@gmail.com", "nitutz", 5, 5, "new description")); //24 not valid update task description task id doesnt exist
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid advancetask");
            Console.WriteLine(counter.ToString() + "." + facade.AdvanceTask("naveav95@gmail.com", "nitutz", 0, 0)); //25 valid advancetask
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid advancetask");
            Console.WriteLine(counter.ToString() + "." + facade.AdvanceTask("naveav95@gmail.com", "nitutz", 0, 0)); //26 valid advancetask
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid advancetask beacaue its already done");
            Console.WriteLine(counter.ToString() + "." + facade.AdvanceTask("naveav95@gmail.com", "nitutz", 0, 0)); //27 not valid advancetask beacaue its allready done
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid advancetask beacaue it doesnt exist");
            Console.WriteLine(counter.ToString() + "." + facade.AdvanceTask("naveav95@gmail.com", "nitutz", 5, 5)); //28 not valid advancetask beacaue it doesnt exist
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid updating beacaue its already done");
            Console.WriteLine(counter.ToString() + "." + facade.UpdateTaskDescription("naveav95@gmail.com", "nitutz", 2, 0, "lala")); //29 not valid updating beacaue its allready done
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid updating beacaue its already done");
            Console.WriteLine(counter.ToString() + "." + facade.UpdateTaskTitle("naveav95@gmail.com", "nitutz", 2, 0, "lala")); //30 not valid updating beacaue its allready done
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid updating beacaue its already done");
            Console.WriteLine(counter.ToString() + "." + facade.UpdateTaskDueDate("naveav95@gmail.com", "nitutz", 2, 0, DateTime.Today.AddDays(5))); //31 not valid updating beacaue its allready done
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid addtask (only for testing purposes)");
            Console.WriteLine(counter.ToString() + "." + facade.AddTask("naveav95@gmail.com", "nitutz", "Task1", "lets get this shit over with2", new DateTime()));//32
            Console.WriteLine("-----------------------\n");
            counter++;
            facade.AssignTask("naveav95@gmail.com", "nitutz", 0, 1, "naveav95@gmail.com"); //task assignment
            facade.AdvanceTask("naveav95@gmail.com", "nitutz", 0, 1);
            facade.AdvanceTask("naveav95@gmail.com", "nitutz", 0, 1);
            /*Console.WriteLine(counter.ToString() + " valid addtask");
            Console.WriteLine(counter.ToString() + "." + boardService.AddTask("naveav95@gmail.com", "nitutz", "Task2", "lets get this shit over with2", new DateTime()));//33
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid addtask");
            Console.WriteLine(counter.ToString() + "." + boardService.AddTask("naveav95@gmail.com", "nitutz", "Task3", "lets get this shit over with2", new DateTime()));//34
            Console.WriteLine("-----------------------\n");*/
        }
        private void BoardTests()
        {
            facade.Login("naveav95@gmail.com", "Aa123456");
            Console.WriteLine(counter.ToString() + "valid addboard ");
            Console.WriteLine(counter.ToString() + "." + facade.AddBoard("naveav95@gmail.com", "nitutz")); //10 valid addboard
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid addboard ");
            Console.WriteLine(counter.ToString() + "." + facade.AddBoard("naveav95@gmail.com", "nitutz")); //11 not valid addboard
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "  valid BoardRemove");
            Console.WriteLine(counter.ToString() + "." + facade.RemoveBoard("naveav95@gmail.com", "nitutz"));//12 valid BoardRemove
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "  not valid BoardRemove board doesnt exist");
            Console.WriteLine(counter.ToString() + "." + facade.RemoveBoard("naveav95@gmail.com", "nitutz"));//13 not valid BoardRemove board doesnt exist
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "  valid addboard");
            Console.WriteLine(counter.ToString() + "." + facade.AddBoard("naveav95@gmail.com", "nitutz")); //14 valid addboard
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " Invalid addboard,user isnt logged in");
            Console.WriteLine(counter.ToString() + "." + facade.AddBoard("danhakatan@gmail.com", "nitutz")); //15 valid addboard
            Console.WriteLine("-----------------------\n");
            counter++;
            facade.AddBoard("naveav95@gmail.com", "nitutz5");
        }
        private void UserOpTests()
        {
            Console.WriteLine(counter.ToString() + " Valid registration Should Succeed");
            Console.WriteLine(counter.ToString() + "." + facade.Register("naveav95@gmail.com", "Aa123456"));//1 Valid registraion
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " Invalid registration, email isnt unique");
            Console.WriteLine(counter.ToString() + "." + facade.Register("naveav95@gmail.com", "Aa123456"));//2 Invalid registraion
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid registration");
            Console.WriteLine(counter.ToString() + "." + facade.Register("navv95@gmail.com", "Aa123456"));//2 Invalid registraion
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " Invalid Login Should Fail");
            Console.WriteLine(counter.ToString() + "." + facade.Login("navev95@gmail.com", "Aa123456"));//2 Invalid login
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " Invalid Login Should Fail");
            Console.WriteLine(counter.ToString() + "." + facade.Login("naveav95@gmail.com", "Aa123456"));//2 Invalid login
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "  Valid logout");
            Console.WriteLine(counter.ToString() + "." + facade.Logout("naveav95@gmail.com"));//3  valid logout
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid login incorrect password");
            Console.WriteLine(counter.ToString() + "." + facade.Login("naveav95@gmail.com", "Aa12345678"));//4 not valid login incorrect password
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid registration");
            Console.WriteLine(counter.ToString() + "." + facade.Register("naveav95.@gmail.com", "Aa12345678999"));//4 not valid login incorrect password
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "  invalid logout user isnt logged in");
            Console.WriteLine(counter.ToString() + "." + facade.Logout("naveav95@gmail.com"));//5 invalid logout user isnt logged in
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid logout user doesnt exist");
            Console.WriteLine(counter.ToString() + "." + facade.Logout("na95@gmail.com"));//6 not valid logout user doesnt exist
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid registration ");
            Console.WriteLine(counter.ToString() + "." + facade.Register("danhakatan@gmail.com", "Bb123456"));//8  valid registration 
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " Invalid login, user is already logged ");
            Console.WriteLine(counter.ToString() + "." + facade.Login("danhakatan@gmail.com", "Bb123456"));//9  Invalid login 
            Console.WriteLine("-----------------------\n");
            facade.Logout("danhakatan@gmail.com");
            counter++;
            Console.WriteLine("Invalid email adress tests");
            Console.WriteLine(counter.ToString() + "not valid registraion email isnt valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("nave.gmail.com", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid registraion email isnt valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("#@%^%#$@#$@#.com", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid registraion email isnt valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("@example.com", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid registraion email isnt valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("Joe Smith <email@example.com>", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid registraion email isnt valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("email@example@example.com", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid registraion email isnt valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register(".email@example.com", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid registraion email isnt valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("email.@example.com", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid registraion email isnt valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("あいうえお@example.com", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid registraion email isnt valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("email@example.com (Joe Smith)", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid registraion email isnt valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("email @example", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid registraion email isnt valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("email@-example.com", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid registraion email isnt valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("email @example.web", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid registraion email isnt valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("email@111.222.333.44444", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid registraion email isnt valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("email @example..com", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid registraion email isnt valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("Abc..123@example.com", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid registraion email is null");
            Console.WriteLine(counter.ToString() + "." + facade.Register(null, "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid registraion password is null");
            Console.WriteLine(counter.ToString() + "." + facade.Register("Abc123@example.com", null));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine("Valid email tests");
            Console.WriteLine(counter.ToString() + ". Valid registraion email is valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("email@example.com", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + ". Valid registraion email is valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("firstname.lastname@example.com", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + ". Valid registraion email is valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("email@subdomain.example.com", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + ". Valid registraion email is valid2");
            Console.WriteLine(counter.ToString() + "." + facade.Register("firstname+lastname@example.com", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            /*Console.WriteLine(counter.ToString() + ". Valid registraion email is valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("email@123.123.123.123", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;*/
            /*            Console.WriteLine(counter.ToString() + ". Valid registraion email is valid");
                        Console.WriteLine(counter.ToString() + "." + facade.Register("\"email\"@example.com", "Cc123456"));//10 not valid registraion email isnt valid
                        Console.WriteLine("-----------------------\n");
                        counter++;*/
            Console.WriteLine(counter.ToString() + ". Valid registraion email is valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("_______@example.com", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + ". Valid registraion email is valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("email@example.name", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + ". Valid registraion email is valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("email@example.co.jp", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + ". Valid registraion email is valid");
            Console.WriteLine(counter.ToString() + "." + facade.Register("firstname-lastname@example.com", "Cc123456"));//10 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
        }
    }
}
