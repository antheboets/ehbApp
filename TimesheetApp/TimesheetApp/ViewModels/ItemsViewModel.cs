using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using TimesheetApp.Models;
using TimesheetApp.Views;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

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
                var hasLoggedIn = false;
                while (!hasLoggedIn)
                {
                    if ((string)Application.Current.Properties["Auth_Token"] == "")
                    {
                        await Task.Delay(100);
                    }
                    else
                    {
                        
                        var testy = (string)Application.Current.Properties["Auth_Token"];
                        hasLoggedIn = true;
                        Items.Clear();

                        DTO.LogArray logs = new DTO.LogArray();

                        var httpWebRequest = (HttpWebRequest)WebRequest.Create(App.urlAPI + "/Log/GetAll"); //url
                        httpWebRequest.ContentType = "application/json"; //ContentType
                        httpWebRequest.Method = "GET"; //Methode
                        httpWebRequest.Headers.Add("Authorization", "Bearer " + Application.Current.Properties["Auth_Token"]);

                        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse(); //sending request
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            
                            String result = streamReader.ReadToEnd(); //get result Json string From response

                            if (httpResponse.StatusCode.ToString() == "OK")
                            {
                                JsonConvert.PopulateObject(result, logs); //converting json string to Obj
                                foreach (var log in logs.Logs)
                                {
                                    Items.Add(new Item { Id = Guid.NewGuid().ToString(), Text = log.Description, Description = log.Start.ToLongTimeString() + " - " + log.Stop.ToLongTimeString() });
                                }
                            }
                            else
                            {
                                Items.Add(new Item { Id = "Something went wrong", Text = "Something went wrong", Description = "Something went wrong" });
                            }
                        }
                        /*
                        foreach (var log in User.LoggedInUser.Logs)
                        {
                            Items.Add(new Item { Id = Guid.NewGuid().ToString(), Text = log.Description, Description = log.Start.ToLongTimeString() + " - " + log.Stop.ToLongTimeString() });
                        }
                        */
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