using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            if(String.IsNullOrWhiteSpace(LoginEmail) || String.IsNullOrWhiteSpace(LoginWw))
            {
                LoginError.IsVisible = true;
            }
            else
            {
                Application.Current.Properties["Auth_Token"] = Boolean.TrueString;
                LoginError.IsVisible = false;
                await Navigation.PopModalAsync();
            }
        }
    }
}