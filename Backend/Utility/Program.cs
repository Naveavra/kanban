using IntroSE.Kanban.Backend.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.Utility
{
    internal class Program
    {
        public static void Main(String[] args)
        {
            DBConnector conn = DBConnector.Instance; 
        }
    }
}
