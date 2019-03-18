﻿using TimesheetApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimesheetApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("Auth_Token"))
            {
                if (Application.Current.Properties["Auth_Token"] != null)
                {
                    MasterBehavior = MasterBehavior.Popover;
                    MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
                }
            }
            else
            {
                PushLogin();
            }
        }

        private async void PushLogin()
        {
            await Navigation.PushModalAsync(new LoginPage());
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Browse:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                    case (int)MenuItemType.Timeline:
                        MenuPages.Add(id, new NavigationPage(new Timeline()));
                        break;
                    case (int)MenuItemType.Logout:
                        Application.Current.Properties.Clear();
                        MenuPages.Add(id, new NavigationPage(new MainPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}