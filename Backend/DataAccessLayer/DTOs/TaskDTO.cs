using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = IntroSE.Kanban.Backend.BusinessLayer.Task;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal class TaskDTO
    {
        private int _Id;
        public int Id { get => _Id; set { _Id = value; TaskMapper.Instance.Update(Id, owner, "id", _Id); } }

        private string _owner;
        public string owner { get => _owner; set { _owner = value; TaskMapper.Instance.Update(Id, owner, "owner", _owner); } }

        private DateTime _CreationTime;
        public DateTime CreationTime { get => _CreationTime; set { _CreationTime = value; TaskMapper.Instance.Update(Id, owner, "created", CreationTime); } }

        private string _Title;
        public string Title { get => _Title; set { _Title = value; TaskMapper.Instance.Update(Id, owner, "title", Title); } }

        private string _Description;
        public string Description { get => _Description; set { _Description = value; TaskMapper.Instance.Update(Id, owner, "desc", Description); } }

        private int _ordinal;
        public int ordinal { get => _ordinal; set { _ordinal = value; TaskMapper.Instance.Update(Id, owner, "ordinal", ordinal); } }

        private int _boardID;
        public int boardID { get => _boardID; set { _boardID = value; TaskMapper.Instance.Update(Id, owner, "boardID", boardID); } }

        private DateTime _DueDate;
        public DateTime DueDate { get => _DueDate; set { _DueDate = value; TaskMapper.Instance.Update(Id, owner, "dueDate", DueDate); } }

        private string _Assignee;
        public string Assignee { get => _Assignee; set { _Assignee = value; TaskMapper.Instance.Update(Id, owner, "assigned", Assignee); } }



        public TaskDTO(int id, DateTime creationTime, string title, string description, int ordinal, int boardID, DateTime dueDate, string assignee,string owner)

        {
            _Id = id;
            _CreationTime = creationTime;
            _Title = title;
            _Description = description;
            _ordinal = ordinal;
            _boardID = boardID;
            _DueDate = dueDate;
            _Assignee = assignee;
            _owner = owner;
        }

        public TaskDTO(Task t,int boardID,int ordinal,string owner)

        {
            _Id = t.Id;
            _CreationTime = t.CreationTime;
            _Title = t.Title;
            _Description = t.Description;
            _ordinal = ordinal;
            _boardID = boardID;
            _DueDate = t.DueDate;
            _Assignee = t.getAssignee();
            _owner = owner;
        }
        public void save()
        {//TODO CHECK IF NEEDED
            Id = _Id;
            CreationTime = _CreationTime;
            Title = _Title;
            Description = _Description;
            ordinal = _ordinal;
            boardID = _boardID;
            DueDate = _DueDate;
            Assignee = _Assignee;
            owner = _owner;
        }

    }
}
