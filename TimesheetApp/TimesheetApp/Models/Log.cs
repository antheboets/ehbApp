using System;
using System.Collections.Generic;
using System.Text;

namespace TimesheetApp.Models
{
    class Log
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public String Description { get; set; }
    }
}
