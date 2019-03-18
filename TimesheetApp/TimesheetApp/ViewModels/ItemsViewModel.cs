using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using TimesheetApp.Models;
using TimesheetApp.Views;

namespace TimesheetApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var hasLogedIn = false;
                while (!hasLogedIn)
                {
                    if (User.LoggedInUser == null)
                    {
                        await Task.Delay(100);
                    }
                    else
                    {
                        hasLogedIn = true;
                        Items.Clear();
                        foreach (var log in User.LoggedInUser.Logs)
                        {
                            Items.Add(new Item { Id = Guid.NewGuid().ToString(), Text = log.Description, Description = log.Start.ToLongTimeString() + " - " + log.Stop.ToLongTimeString() });
                        }
                    }
                }

                //var items = await DataStore.GetItemsAsync(true);
                /*
                foreach (var item in items)
                {
                    Items.Add(item);
                }
                */
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}