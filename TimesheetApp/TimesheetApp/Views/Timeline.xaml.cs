﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TimesheetApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimesheetApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Timeline : ContentPage
	{
		public Timeline ()
		{
            InitializeComponent();
		}
	}
}