using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Acr.UserDialogs;
using FindMe.Views;
using Xamarin.Forms;

namespace FindMe.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;

        public LoginViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }


        public string Username { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand => new Command(Login);

        private async void Login()
        {
            Application.Current.MainPage = new NavigationPage(new HomePage());
            //if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            //{

            //    await UserDialogs.Instance.AlertAsync("Please fill out all fields");
            //}
            //else
            //{
            //    if (Username != "agent" && Password != "agent")
            //    {
            //        await UserDialogs.Instance.AlertAsync("Incorrect username or password");
            //    }
            //    else
            //    {
            //        Application.Current.MainPage = new NavigationPage(new HomePage());
            //    }
            //}
        }
    }

}
