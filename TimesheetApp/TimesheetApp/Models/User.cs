using System;
using System.Collections.Generic;
using System.Text;

namespace TimesheetApp.Models
{
    class User
    {
        public static User LoggedInUser { get; set; }

        public int Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public ICollection<Log> Logs { get; set; }
    }
}
