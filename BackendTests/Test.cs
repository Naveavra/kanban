using System;
using System.Collections.Generic;
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

        public void RunTests()
        {
            UserService userService = new UserService();
            BoardService boardService = new BoardService();
            int counter = 0;
            Console.WriteLine("-----------------------\n");
            Console.WriteLine(counter.ToString()+" Valid registration Should Succeed");
            Console.WriteLine(counter.ToString() + "." + userService.Register("naveav95@gmail.com", "Aa123456"));//0 Valid registraion
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " Valid Login Should Succeed");
            Console.WriteLine(counter.ToString() + "." + userService.Login("naveav95@gmail.com", "Aa123456"));//1 valid login
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "  invalid login because the user is already logged in");
            Console.WriteLine(counter.ToString() + "." + userService.Login("naveav95@gmail.com", "Aa123456"));//2 invalid login because the user is already logged in
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "  Valid logout");
            Console.WriteLine(counter.ToString() + "." + userService.Logout("naveav95@gmail.com"));//3  valid logout
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid login incrrect password");
            Console.WriteLine(counter.ToString() + "." + userService.Login("naveav95@gmail.com", "124"));//4 not valid login incrrect password
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "  invalid logout user isnt logged in");
            Console.WriteLine(counter.ToString() + "." + userService.Logout("naveav95@gmail.com"));//5 invalid logout user isnt logged in
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid logout user doesnt exist");
            Console.WriteLine(counter.ToString() + "." + userService.Logout("na95@gmail.com"));//6 not valid logout user doesnt exist
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "  not valid registration email isnt unique");
            Console.WriteLine(counter.ToString() + "." + userService.Register("naveav95@gmail.com", "SFDGF"));//7 not valid registration email isnt unique
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid registration ");
            Console.WriteLine(counter.ToString() + "." + userService.Register("danhakatan@gmail.com", "Bb123456"));//8  valid registration 
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid login ");
            Console.WriteLine(counter.ToString() + "." + userService.Login("danhakatan@gmail.com", "Bb123456"));//9  valid login 
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "valid addboard ");
            Console.WriteLine(counter.ToString() + "." + boardService.AddBoard("naveav95@gmail.com", "nitutz")); //10 valid addboard
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid addboard ");
            Console.WriteLine(counter.ToString() + "." + boardService.AddBoard("naveav95@gmail.com", "nitutz")); //11 not valid addboard
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "  valid BoardRemove");
            Console.WriteLine(counter.ToString() + "." + boardService.RemoveBoard("naveav95@gmail.com", "nitutz"));//12 valid BoardRemove
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "  not valid BoardRemove board doesnt exist");
            Console.WriteLine(counter.ToString() + "." + boardService.RemoveBoard("naveav95@gmail.com", "nitutz"));//13 not valid BoardRemove board doesnt exist
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "  valid addboard");
            Console.WriteLine(counter.ToString() + "." + boardService.AddBoard("naveav95@gmail.com", "nitutz")); //14 valid addboard
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "  valid addboard");
            Console.WriteLine(counter.ToString() + "." + boardService.AddBoard("danhakatan@gmail.com", "nitutz")); //15 valid addboard
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "valid addtask");
            Console.WriteLine(counter.ToString() + "." + boardService.AddTask("naveav95@gmail.com", "nitutz", "Task0", "lets get this shit over with", new DateTime())); //16 valid addtask
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid addtask because there is no title");
            Console.WriteLine(counter.ToString() + "." + boardService.AddTask("naveav95@gmail.com", "nitutz", "", "lets get this shit over with2", new DateTime())); //17 not valid addtask because there is no title
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid addtask because the title is only white spaces");
            Console.WriteLine(counter.ToString() + "." + boardService.AddTask("naveav95@gmail.com", "nitutz", "    ", "lets get this shit over with2", new DateTime())); //18 not valid addtask because the title is only white spaces
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "valid update task due");
            Console.WriteLine(counter.ToString() + "." + boardService.UpdateTaskDueDate("naveav95@gmail.com", "nitutz", 0, new DateTime())); //19 valid update task due
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid update task due because taskid isnt correct");
            Console.WriteLine(counter.ToString() + "." + boardService.UpdateTaskDueDate("naveav95@gmail.com", "nitutz", 5, new DateTime())); //20 not valid update task due because taskid isnt correct
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "valid update task title");
            Console.WriteLine(counter.ToString() + "." + boardService.UpdateTaskTitle("naveav95@gmail.com", "nitutz", 0, "NewTitleNitutz")); //21 valid update task title
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid update task title id doesnt exist");
            Console.WriteLine(counter.ToString() + "." + boardService.UpdateTaskTitle("naveav95@gmail.com", "nitutz", 5, "NewTitleNitutz")); //22 not valid update task title id doesnt exist
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "valid update task description");
            Console.WriteLine(counter.ToString() + "." + boardService.UpdateTaskDesc("naveav95@gmail.com", "nitutz", 0, "new description")); //23 valid update task description
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid update task description task id doesnt exist");
            Console.WriteLine(counter.ToString() + "." + boardService.UpdateTaskDesc("naveav95@gmail.com", "nitutz", 5, "new description")); //24 not valid update task description task id doesnt exist
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid advancetask");
            Console.WriteLine(counter.ToString() + "." + boardService.AdvanceTask("naveav95@gmail.com", "nitutz", 0)); //25 valid advancetask
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid advancetask");
            Console.WriteLine(counter.ToString() + "." + boardService.AdvanceTask("naveav95@gmail.com", "nitutz", 0)); //26 valid advancetask
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid advancetask beacaue its already done");
            Console.WriteLine(counter.ToString() + "." + boardService.AdvanceTask("naveav95@gmail.com", "nitutz", 0)); //27 not valid advancetask beacaue its allready done
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid advancetask beacaue it doesnt exist");
            Console.WriteLine(counter.ToString() + "." + boardService.AdvanceTask("naveav95@gmail.com", "nitutz", 5)); //28 not valid advancetask beacaue it doesnt exist
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid updating beacaue its already done");
            Console.WriteLine(counter.ToString() + "." + boardService.UpdateTaskDesc("naveav95@gmail.com", "nitutz", 0, "lala")); //29 not valid updating beacaue its allready done
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid updating beacaue its already done");
            Console.WriteLine(counter.ToString() + "." + boardService.UpdateTaskTitle("naveav95@gmail.com", "nitutz", 0, "lala")); //30 not valid updating beacaue its allready done
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid updating beacaue its already done");
            Console.WriteLine(counter.ToString() + "." + boardService.UpdateTaskDueDate("naveav95@gmail.com", "nitutz", 0, new DateTime())); //31 not valid updating beacaue its allready done
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid addtask");
            Console.WriteLine(counter.ToString() + "." + boardService.AddTask("naveav95@gmail.com", "nitutz", "Task1", "lets get this shit over with2", new DateTime()));//32
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid addtask");
            Console.WriteLine(counter.ToString() + "." + boardService.AddTask("naveav95@gmail.com", "nitutz", "Task2", "lets get this shit over with2", new DateTime()));//33
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid addtask");
            Console.WriteLine(counter.ToString() + "." + boardService.AddTask("naveav95@gmail.com", "nitutz", "Task3", "lets get this shit over with2", new DateTime()));//34
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid GetColumn");
            Console.WriteLine(counter.ToString() + "." + boardService.GetColumn("naveav95@gmail.com", "nitutz", 0)); //35 Get column
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "not valid registraion email isnt valid");
            Console.WriteLine(counter.ToString() + "." + userService.Register("nave.gmail.com", "Cc123456"));//36 not valid registraion email isnt valid
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " invalid because there is more tasks than the limit");
            Console.WriteLine(counter.ToString() + "." + boardService.LimitColumn("naveav95@gmail.com", "nitutz", 0, 1));//37 invalid because there is more tasks than the limit
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid because there is less tasks than the limit");
            Console.WriteLine(counter.ToString() + "." + boardService.LimitColumn("naveav95@gmail.com", "nitutz", 0, 5));//38 valid because there is more tasks than the limit
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " valid ,5");
            Console.WriteLine(counter.ToString() + "." + boardService.GetColumnLimit("naveav95@gmail.com", "nitutz", 0));//39 valid 5
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid columnID doesnt exist");
            Console.WriteLine(counter.ToString() + "." + boardService.GetColumnLimit("naveav95@gmail.com", "nitutz", 20));//40 not valid columnID doesnt exist 
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "valid, -1");
            Console.WriteLine(counter.ToString() + "." + boardService.GetColumnLimit("naveav95@gmail.com", "nitutz", 1));//41 valid -1 
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + "valid, Done");
            Console.WriteLine(counter.ToString() + "." + boardService.GetColumnName("naveav95@gmail.com", "nitutz", 2));//42 valid done
            Console.WriteLine("-----------------------\n");
            counter++;
            Console.WriteLine(counter.ToString() + " not valid column id doesnt exist");
            Console.WriteLine(counter.ToString() + "." + boardService.GetColumnName("naveav95@gmail.com", "nitutz", 8));//43 not valid column id doesnt exist
            Console.WriteLine("-----------------------\n");
            counter++;

        }
    }
}
