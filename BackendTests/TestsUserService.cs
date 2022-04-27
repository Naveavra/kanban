using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using IntroSE.Kanban.Backend.ServiceLayer;

namespace BackendTests
{
    internal class TestsUserService
    {
        /*
        private IntroSE.Kanban.Backend.ServiceLayer.UserService use;
        public TestsUserService(IntroSE.Kanban.Backend.ServiceLayer.UserService use){this.use = use;}
        public void testRegister()
        {
            string resultRegister1 = use.register("naveAndMikiForever@gmail.com", "danhakatan23");

            if (resultRegister1.Equals("Great success"))
                Console.WriteLine("created user successfully very nice");
            else
                Console.WriteLine(resultRegister1);

            string resultRegister2 = use.register("naveAndMikiForever@gmail.com", "danhakatan23");

            if (resultRegister2.Equals("Great success"))
                Console.WriteLine("error : success to register same username");
            else
                Console.WriteLine(resultRegister2);
        }

        public void testLogin()
        {

            string resultTest = use.login("naveAndMikiForever@gmail.com", "danhakatan23");
            if (resultTest.Equals("Great success"))
                Console.WriteLine("OK");
            else
                Console.WriteLine(resultTest);

            string resultTest2 = use.login("naveAndMikiForever@gmail.com", "danhakatan23");
            if (resultTest.Equals("Great success"))
                Console.WriteLine("this let to login to a loggedin user");
            else
                Console.WriteLine("OK");

            string resultTest3 = use.login("danTheGreat@gmail.com", "114453");
            if (resultTest3.Equals("Great success"))
                Console.WriteLine("Error : login work on not exist email");
            else
                Console.WriteLine("OK");


        }
        public void testLogout()
        {
            string resultTest = use.logout("naveAndMikiForever@gmail.com");
            if (resultTest.Equals("Great success"))
                Console.WriteLine("OK");
            else
                Console.WriteLine(resultTest);

            string resultTest2 = use.logout("naveAndMikiForever@gmail.com");
            if (resultTest.Equals("Great success"))
                Console.WriteLine("this let to logout from logged out user");
            else
                Console.WriteLine("OK");

            string resultTest3 = use.logout("danTheGreat@gmail.com");
            if (resultTest3.Equals("Great success"))
                Console.WriteLine("Error : logout work on not exist email");
            else
                Console.WriteLine("OK");

        }
        public void testGetUser()
        {
            string resultTest = use.login("naveAndMikiForever@gmail.com", "danhakatan23");
            string borat = use.getUser("naveAndMikiForever@gmail.com");
            if (!borat.Equals("Great success"))
            {
                Console.WriteLine("borat exist but return null");
            }
            else
            {
                Console.WriteLine("OK");
            }
            use.logout("naveAndMikiForever@gmail.com");
            borat = use.getUser("naveAndMikiForever@gmail.com");
            if (borat.Equals("Great success"))
            {
                Console.WriteLine("borat not exist but return success");
            }
            else
            {
                Console.WriteLine("OK");
            }

        }
        public void runTests()
        {
            testRegister();
            testLogin();
            testLogout();
            testGetUser();
        }
        */
    }
}

