using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal class BoardUserDTO
    {
        public int boardID { get; set; }
        public string user { get; set; }
        public BoardUserDTO(int BID,string user)
        {
            boardID = BID;
            this.user = user;
        }
    }
}
