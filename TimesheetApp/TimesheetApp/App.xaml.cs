﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TimesheetApp.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TimesheetApp
{
    public partial class App : Application
    {
        public static string urlAPI = "https://ehbpmagroup6.azurewebsites.net";
        public App()
        {
            InitializeComponent();
            SetProperties("Auth_Token", "");
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        public async static void SetProperties(string property, object value)
        {
            var app = (App)Application.Current;
            app.Properties[property] = value;
            await app.SavePropertiesAsync();
        }
    }
}
