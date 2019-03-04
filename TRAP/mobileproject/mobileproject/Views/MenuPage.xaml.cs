using mobileproject.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobileproject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            //TODO: if user is manager/HR
            if (true)
            {
                menuItems = new List<HomeMenuItem>
                {
                    new HomeMenuItem {Id = MenuItemType.Browse, Title="Timeline" },
                    new HomeMenuItem {Id = MenuItemType.About, Title="Workweek" },
                    new HomeMenuItem {Id = MenuItemType.About, Title="Projects" },
                    new HomeMenuItem {Id = MenuItemType.About, Title="Activities" },
                    new HomeMenuItem {Id = MenuItemType.About, Title="Settings" },
                    new HomeMenuItem {Id = MenuItemType.About, Title="Logout" }
                };
            }
            else
            {
                menuItems = new List<HomeMenuItem>
                {
                    new HomeMenuItem {Id = MenuItemType.Browse, Title="Timeline" },
                    new HomeMenuItem {Id = MenuItemType.About, Title="Workweek" },
                    new HomeMenuItem {Id = MenuItemType.About, Title="Settings" },
                    new HomeMenuItem {Id = MenuItemType.About, Title="Logout" }
                };
            }


            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}