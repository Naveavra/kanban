// See https://aka.ms/new-console-template for more information
using BackendTests;

Console.WriteLine("Hello, World!");
TestsBoardService tbs = new TestsBoardService();
tbs.runTests();
//TestsUserService tus = new TestsUserService(new IntroSE.Kanban.Backend.ServiceLayer.userService());
//tus.runTests();