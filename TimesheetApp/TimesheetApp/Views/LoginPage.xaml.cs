﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.LocalNotifications;

namespace TimesheetApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private String LoginEmail;
        private String LoginWw;
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void LoginButtonClicked(object sender, EventArgs e)
        {
            LoginEmail = Email.Text;
            LoginWw = Ww.Text;

            DTO.Login login = new DTO.Login();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(App.urlAPI + "/Auth/Login"); //url
            httpWebRequest.ContentType = "application/json"; //ContentType
            httpWebRequest.Method = "POST"; //Methode

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"email\":\"" + LoginEmail + "\",\"password\":\"" + LoginWw + "\"}"; //JSON body

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse(); //sending request
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                String result = streamReader.ReadToEnd(); //get result Json string From response


                if (httpResponse.StatusCode.ToString() == "OK")
                {
                    JsonConvert.PopulateObject(result, login); //converting json string to Obj
                    if (login.Token != null)
                    {
                        Application.Current.Properties["Auth_Token"] = login.Token;
                        // test notificatie
                        //CrossLocalNotifications.Current.Show(user.Name, "User " + user.Name + " is ingelogd", 1, new DateTime(2017, 1, 11, 22, 0, 0));
                        LoginError.IsVisible = false;
                        await Navigation.PopModalAsync();
                    }
                    LoginError.IsVisible = true;
                }
                else
                {
                    LoginError.IsVisible = true;
                }
            }
        }
    }
}