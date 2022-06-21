using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal class ColumnDTO
    {
        public string ColumnName { get; set; }
        public int limit  {get;set;}
        public int ordinal { get;set;}
        public int boardID { get; set; }

        public ColumnDTO(string columnName,int limit, int ordinal, int boardName)
        {
            this.ColumnName = columnName;
            this.limit = limit;
            this.ordinal = ordinal;
            this.boardID = boardName;
        }
        public ColumnDTO(Column c,int ordinal,int boardID)
        {
            this.ColumnName=c.getName();
            this.limit= c.getlimit();
            this.ordinal=ordinal;
            this.boardID=boardID;

        }
    }
}
