﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TimesheetApp.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Timeline,
        Logout,
        Userinfo
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
