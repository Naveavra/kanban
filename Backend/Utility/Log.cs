using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace IntroSE.Kanban.Backend.Utility
{
    internal class Log
    {
        private static readonly ILog logger = LogManager.GetLogger("KanbanLogger");

        public static ILog GetLogger()
        {
            return logger;
        }

    }
}
