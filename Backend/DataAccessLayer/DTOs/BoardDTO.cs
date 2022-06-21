using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal class BoardDTO
    {
        public int boardID { get; set; }
        public string name { get; set; }
        public string boardowner { get; set; }
        public int taskID { get; set; }
        public string boarduser { get; set; }

        public BoardDTO(int boardID, string name, string boardowner, int taskID)
        {
            this.boardID = boardID;
            this.name = name;
            this.boardowner = boardowner;
            this.taskID = taskID;
            
        }
        public BoardDTO(Board b)
        {
            this.boardID = b.getID();
            this.name = b.name;
            this.boardowner = b.boardowner;
            this.taskID = b.taskID;
        }
    }
}
