using System;
using System.IO;
using Acr.UserDialogs;
using FindMe.Data;
using FindMe.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace FindMe
{
    public partial class App : Application
    {
        public static UserDatabase Database;
        public static UserDatabase UserDatabase
        {
            get
            {
                if (Database == null)
                {
                    Database = new UserDatabase(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            "FindMedb.db3"));
                }
                return Database;
            }
        }


        public App()
        {
            InitializeComponent();
            //MainPage=new HomePage();
                MainPage = new NavigationPage(new LoginPage());
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
    }
}
