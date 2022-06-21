using IntroSE.Kanban.Backend.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using User = IntroSE.Kanban.Backend.BusinessLayer.User;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal class UserDTO 
    {
        private bool logged;
        public string email { get; set; }
        public string password { get; }
        public UserDTO(string email, string password, bool logged)
        {
            this.email = email;
            this.password = password;
            this.logged = logged;
        }

    }
}
