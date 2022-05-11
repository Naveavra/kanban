using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    internal class BoardControl
    {
        int counter; //this counter will give every new board his id
        Dictionary<string, Dictionary<string, Board>> boards; //borads pool <user email, <boardname, board>>

        public BoardControl(){
            boards = new Dictionary<string, Dictionary<string, Board>>();
            }
        public void AddBoard(string email, string boardName) //TODO miki
        {

        
            
           

        }
    }
}
