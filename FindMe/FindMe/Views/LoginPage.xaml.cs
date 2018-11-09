using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FindMe.Models;
using FindMe.Services;
using FindMe.ViewModels;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindMe.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
		    BindingContext = new LoginViewModel(Navigation);
        }

	    private async void Register(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new Views.Register());
	    }


        private async void Login(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new HomePage());
            //if (string.IsNullOrEmpty(Username.Text) || string.IsNullOrEmpty(Password.Text))
            //{

            //    await UserDialogs.Instance.AlertAsync("Please fill out all fields");
            //}
            //else
            //{
            //    if (CrossConnectivity.Current.IsConnected)
            //    {
            //        try
            //        {
            //            this.InputTransparent = true;
            //            UserDialogs.Instance.ShowLoading("Logging In", MaskType.None);
            //            var model = new LoginModel { Username = Username.Text, Password = Password.Text };
            //            var loginService = new Authenticate();
            //            Response response = await loginService.Login(model);

            //            if (!response.IsSuccessStatusCode)
            //            {
            //                this.InputTransparent = false;
            //                await UserDialogs.Instance.AlertAsync("Incorrect username or password");
            //                UserDialogs.Instance.HideLoading();
            //            }
            //            else
            //            {
            //                this.InputTransparent = false;
            //                LoginReturn ret = JsonConvert.DeserializeObject<LoginReturn>(response.Content);
            //                Application.Current.Properties["Id"] = ret.Id;
            //                Application.Current.MainPage = new NavigationPage(new HomePage());
            //                UserDialogs.Instance.HideLoading();
            //            }
            //        }
            //        catch (Exception ee)
            //        {
            //            Debug.Write(ee.InnerException);
            //            this.InputTransparent = false;
            //            UserDialogs.Instance.HideLoading();
            //            await UserDialogs.Instance.AlertAsync("There was an error executing your request");
            //        }
            //    }
            //    else
            //    {
            //        UserDialogs.Instance.HideLoading();
            //        await UserDialogs.Instance.AlertAsync("You are not connected to the internet");
            //    }
            //}
        }

    }
}