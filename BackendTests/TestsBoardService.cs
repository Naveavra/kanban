using IntroSE.Kanban.Backend.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTests
{
    public class TestsBoardService
    {
        private BoardService boardService = new BoardService();
        private UserService userSer = new UserService();
      
        public void testAddTask()
        {
            string resultTest = boardService.AddTask("miki@gmail.com", "b", "work 1", " build search engine in binary", "20.4.2022");
            if (resultTest.Equals("success"))
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("fail, all arguments are correct but didnt success");
            }
            string resultTest1 = boardService.AddTask("miki@gmail.", "b", "work 1", " build search engine in binary", "20.4.2022");
            if (resultTest.Equals("success"))
            {
                Console.WriteLine("fail, there is no such email");
            }
            else
            {
                Console.WriteLine("fail successefuly");
            }
            string resultTest2 = boardService.AddTask("miki@gmail.com", "c", "work 1", " build search engine in binary", "20.4.2022");
            if (resultTest.Equals("success"))
            {
                Console.WriteLine("fail, there is no such board");
            }
            else
            {
                Console.WriteLine("fail successefuly");
            }
            string resultTest3 = boardService.AddTask("miki@gmail.com", "b", "work 1", " build search engine in binary", "20.4.2022");
            if (resultTest.Equals("success"))
            {
                Console.WriteLine("fail, dueDate cannot be in the past");
            }
            else
            {
                Console.WriteLine("fail successfuly");
            }
        }
        public void testAddBoard()
        {
            userSer.login("miki@gmail.com", "dan3456");
            string resultTest = boardService.AddBoard("miki@gmail.com", "boardboard");
            if (resultTest.Equals("success"))
                Console.WriteLine("OK");
            else
                Console.WriteLine(resultTest);
            if (boardService.GetBoard("miki@gmail.com", "boardboard").Equals("success"))
            {
                Console.WriteLine("OK");
            }
            else
                Console.WriteLine(resultTest);
            string resultTest2 = boardService.AddBoard("miki@gmail.com", "boardboard");
            if (resultTest2.Equals("success"))
            {
                Console.WriteLine("board name should be uninqe");
            }
            else
                Console.WriteLine("OK");

        }
        public void testGetBoard()
        {
            userSer.login("miki@gmail.com", "dan3456");
            string resultTest = boardService.AddBoard("miki@gmail.com", "boardboard");
            if (resultTest.Equals("success"))
                Console.WriteLine("OK");
            else
                Console.WriteLine(resultTest);
            if (boardService.GetBoard("miki@gmail.com", "boardboard").Equals("success"))
            {
                Console.WriteLine("OK");
            }
            else
                Console.WriteLine(resultTest);
            string resultTest2 = boardService.AddBoard("miki@gmail.com", "boardboard");
            if (resultTest2.Equals("success"))
            {
                Console.WriteLine("board name should be uninqe");
            }
            else
                Console.WriteLine("OK");

        }
        public void testToInProgress()
        {
            boardService.AddTask("miki@gmail.com", "b", "work 1", " build search engine in binary", "20.4.2022");
            string resultTest = boardService.ToInProgress("miki@gmail.com");
            if (resultTest.Equals("success"))
            {
                Console.WriteLine("Ok");
            }
            else
            {
                Console.WriteLine("fail,all ");
            }
            boardService.RemoveTask("miki@gmail.com", "b", 0, 1);
            string resultTest1 = boardService.ToInProgress("miki@gmail.com");
            if (resultTest.Equals("success"))
            {
                Console.WriteLine("fail,there is no task to show");
            }
            else
            {
                Console.WriteLine("failed successfuly");
            }
            string resultTest2 = boardService.ToInProgress("miki@gmail");
            if (resultTest.Equals("success"))
            {
                Console.WriteLine("fail,there is no such name");
            }
            else
            {
                Console.WriteLine("failed successfuly");
            }
        }
        public void testLimitColumn()
        {
            string resultTest = boardService.limitColumn("miki@gmail.com", "b", 0, 5);
            if (resultTest.Equals("success"))
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("fail, all arguments are correct but didnt success");
            }
            string resultTest1 = boardService.limitColumn("miki@gmail.", "b", 0, 5);
            if (resultTest.Equals("success"))
            {
                Console.WriteLine("fail, email does not exist");
            }
            else
            {
                Console.WriteLine("fail successfuly!!!");
            }
            string resultTest2 = boardService.limitColumn("miki@gmail.com", "c", 0, 5);
            if (resultTest.Equals("success"))
            {
                Console.WriteLine("fail, board name does not exist");
            }
            else
            {
                Console.WriteLine("fail successfuly!!!");
            }
            string resultTest3 = boardService.limitColumn("miki@gmail.", "b", 0, -5);
            if (resultTest.Equals("success"))
            {
                Console.WriteLine("fail, limit cannot be negative");
            }
            else
            {
                Console.WriteLine("fail successfuly!!!");
            }
        }
       
        public void testUpdateTaskDue()
        {
            string resultTest = boardService.UpdateTaskDue("miki@gmail.com", "b", 1, 0, "23/6/2043");
            if (resultTest.Equals("success"))
            {
                Console.WriteLine("Ok");
            }
            else
            {
                System.Console.WriteLine("fail,all arguments were correct but failed");
            }
            string resultTes2 = boardService.UpdateTaskDue("miki@gmail", "b", 1, 0, "23/6/2043");
            if (resultTest.Equals("success"))
            {
                Console.WriteLine("fail, there is no such email");
            }
            else
            {
                Console.WriteLine("fail successefuly");
            }
        }
        public void testUpdateTaskDesc()
        {
            string resultTest = boardService.UpdateTaskDesc("miki@gmail.com", "b", 1, 0, "new des");
            if (resultTest.Equals("success"))
            {
                Console.WriteLine("Ok");
            }
            else
            {
                System.Console.WriteLine("fail,all arguments were correct but failed");
            }
            string resultTes2 = boardService.UpdateTaskDesc("miki@gmail", "b", 1, 0, "new des");
            if (resultTest.Equals("success"))
            {
                Console.WriteLine("fail, there is no such email");
            }
            else
            {
                Console.WriteLine("fail successefuly");
            }
        }
        public void testUpdateTaskTitle()
        {
            string resultTest = boardService.UpdateTaskTitle("miki@gmail.com", "b", 1, 0, "new title");
            if (resultTest.Equals("success"))
            {
                Console.WriteLine("Ok");
            }
            else
            {
                System.Console.WriteLine("fail,all arguments were correct but failed");
            }
            string resultTes2 = boardService.UpdateTaskTitle("miki@gmail", "b", 1, 0, "new title");
            if (resultTest.Equals("success"))
            {
                Console.WriteLine("fail, there is no such email");
            }
            else
            {
                Console.WriteLine("fail successefuly");
            }
        }
        public void testRemoveBoard()
        {
            string resultTest = boardService.RemoveBoard("miki@gmail.com", "b");
            if (resultTest.Equals("success"))
            {
                System.Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("fail,all arguments were correct but didnt found");
            }
            string resultTest1 = boardService.RemoveBoard("miki@gmail.com", "b");
            if (resultTest.Equals("success"))
            {
                System.Console.WriteLine("fail,board allready been removed");
            }
            else
            {
                Console.WriteLine("fail succsessfuly");
            }
            string resultTest2 = boardService.RemoveBoard("miki@gmail", "b");
            if (resultTest.Equals("success"))
            {
                System.Console.WriteLine("fail,there is no such mail");
            }
            else
            {
                Console.WriteLine("fail succsessfuly");
            }
        }
        public void testRemoveTask()
        {
            string resultTest = boardService.RemoveTask("miki@gmail.com", "b", 1, 0);
            if (resultTest.Equals("success"))
            {
                Console.WriteLine("Ok");
            }
            else
            {
                Console.WriteLine("fail,all arguments were correct but failed");
            }
            string resultTest1 = boardService.RemoveTask("miki@gmail.com", "b", 1, 0);
            if (resultTest.Equals("success"))
            {
                Console.WriteLine("fail,task allready been removed");
            }
            else
            {
                Console.WriteLine("fail successfuly");
            }
        }
        public void runTests()
        {
            testRemoveTask();
            testAddTask();
            testAddBoard();
            testGetBoard();
            testToInProgress();   
            testLimitColumn(); 
            testUpdateTaskDue();
            testUpdateTaskDesc();
            testUpdateTaskTitle();
            testRemoveBoard();
        }
    }
}

