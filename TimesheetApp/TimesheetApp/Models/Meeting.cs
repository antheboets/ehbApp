﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TimesheetApp.Models
{
    class Meeting
    {
        public string EventName { get; set; }
        public string Organizer { get; set; }
        public string ContactID { get; set; }
        public int Capacity { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Color color { get; set; }
        public bool AllDay { get; set; }
    }
}
