using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using FindMe.Data;
using Plugin.Connectivity;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FindMe.ViewModels
{
    public class HomeViewModel:BaseViewModel
    {
        public string ALabel { get; set; }
        public string AButton { get; set; }

        UserDatabase db;
        private bool _isrefreshing = false;
        public HomeViewModel()
        {
            db = App.UserDatabase;
            Users=new ObservableCollection<User>();
            users = new List<User>();
            setItems();
            //coordadd();
        }

        public ObservableCollection<User> Users { get; set; }
        private ObservableCollection<Coordinates> mycoordinate=new ObservableCollection<Coordinates>();

        public ObservableCollection<Coordinates> MyCoordinate
        {
            get { return mycoordinate; }
            set
            {
                
                mycoordinate = value;
                OnPropertyChanged();
            }
        }

        public List<User> users {get; set; }

        async void setItems()
        {
            users =await db.GetItemsAsync();
            foreach (var user in users)
            {
                Users.Add(user);
            }
            ALabel = $"My Registrations ({users.Count})";
            AButton = $"Upload {mycoordinate.Count} Records";
            OnPropertyChanged(nameof(AButton));
            OnPropertyChanged(nameof(ALabel));
        }

        private async void refresh()
        {
            isrefreshing = true;
            users = await db.GetItemsAsync();
            Users.Clear();
            if (users.Count != Users.Count)
            {
                foreach (var user in users)
                {
                    Users.Add(user);
                }
            }
           
            ALabel = $"My Registrations ({users.Count})";
            OnPropertyChanged(nameof(ALabel));
            AButton =users.Count==1? $"Upload {users.Count} Record": $"Upload {users.Count} Records";
            OnPropertyChanged(nameof(AButton));
            isrefreshing = false;
        }

        //List<string> categories = new List<string>
        //{
        //    "Artisan","Educationist",
        //};
        //List<string> gender = new List<string>
        //{
        //    "Male","Female"
        //};
        //List<string> subcategories = new List<string>
        //{
        //    "Lecturer","Speaker"
        //};

        //public List<string> Categories
        //{
        //    get { return categories; }
        //}
        //public List<string> Gender
        //{
        //    get { return gender; }
        //}
        //public List<string> SubCategories
        //{
        //    get { return subcategories; }
        //}

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }  
        public string CompanyTitle { get; set; }
        public string CompanyAddress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool isrefreshing
        {
            get { return _isrefreshing;}
            set
            {
                _isrefreshing = value;
                OnPropertyChanged(nameof(isrefreshing));
            }
        }

        public ICommand RegisterCommand => new Command(Register);
        public ICommand RefreshCommand => new Command(refresh);
        public ICommand CoordinateAddCommand => new Command(coordadd);
        public ICommand UploadCommand => new Command(upload);

        private async void upload()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                UserDialogs.Instance.ShowLoading("Uploading Records");
                await Task.Delay(TimeSpan.FromSeconds(5));
                await db.TruncateDbAsync();
                refresh();
                UserDialogs.Instance.HideLoading();
            }
            else
            {
                await UserDialogs.Instance.AlertAsync("You are not connected to the internet");
                UserDialogs.Instance.HideLoading();
            }
        }

        private async void coordadd()
        {
            //if (CrossConnectivity.Current.IsConnected)
            //{
                try
                {
                    UserDialogs.Instance.ShowLoading("Getting Coordinates");
                    //var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    //var location = await Geolocation.GetLocationAsync(request);
                    var location = await Geolocation.GetLastKnownLocationAsync();

                    if (location != null)
                    {
                        if (mycoordinate.Any(a =>
                            a.Latittude == location.Latitude.ToString() &&
                            a.Longitude == location.Longitude.ToString()))
                        {
                            await UserDialogs.Instance.AlertAsync("Coordinates already added");
                            UserDialogs.Instance.HideLoading();
                            return;
                        }
                       
                        mycoordinate.Add(new Coordinates{Latittude = location.Latitude.ToString(),Longitude = location.Longitude.ToString()});
                        OnPropertyChanged(nameof(MyCoordinate));
                        UserDialogs.Instance.HideLoading();
                    }
                    else
                    {
                        await UserDialogs.Instance.AlertAsync("Could not get coordinates");
                        UserDialogs.Instance.HideLoading();
                    }
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    await UserDialogs.Instance.AlertAsync("Geolocation is not supported on your device");
                    UserDialogs.Instance.HideLoading();
                }
                catch (PermissionException pEx)
                {
                    await UserDialogs.Instance.AlertAsync("Could not obtain necessary permissions on your device");
                    UserDialogs.Instance.HideLoading();
                }
                catch (Exception ex)
                {
                    await UserDialogs.Instance.AlertAsync("There was an error getting your coordinates");
                    UserDialogs.Instance.HideLoading();
                }
            //}
            //else
            //{
            //    await UserDialogs.Instance.AlertAsync("You are not connected to the internet");
            //    UserDialogs.Instance.HideLoading();
            //}

        }

        private async void Register()
        {
            //await db.TruncateDbAsync();
            if (String.IsNullOrEmpty(Phone) || String.IsNullOrEmpty(Email) ||
                String.IsNullOrEmpty(FirstName) || String.IsNullOrEmpty(LastName) || String.IsNullOrEmpty(CompanyTitle) || (mycoordinate.Count==0))
            {
                await UserDialogs.Instance.AlertAsync("There are one or more empty fields");
            }
            else if (Phone.Length < 10 || Phone.Length > 10)
            {
                await UserDialogs.Instance.AlertAsync("Phone Number must be 10 digits exactly");
            }
            else
            {

                var model = new User {
                    Coordinate1 = mycoordinate.Count>0? "Longitude:"+ mycoordinate[0].Longitude + "," + "Latitude"+mycoordinate[0].Longitude : null,
                    Coordinate2 = mycoordinate.Count>1 ? "Longitude:" + mycoordinate[1].Longitude + "," + "Latitude" + mycoordinate[1].Longitude : null,
                    Coordinate3 = mycoordinate.Count > 2  ? "Longitude:" + mycoordinate[2].Longitude + "," + "Latitude" + mycoordinate[2].Longitude : null,
                    Coordinate4 = mycoordinate.Count > 3? "Longitude:" + mycoordinate[3].Longitude + "," + "Latitude" + mycoordinate[3].Longitude : null,
                    Coordinate5 = mycoordinate.Count > 4? "Longitude:" + mycoordinate[4].Longitude + "," + "Latitude" + mycoordinate[4].Longitude : null,
                    FullName = FirstName + " " + LastName, RegistrationDate = DateTime.Now, FirstName = FirstName, LastName = LastName, Phone = Phone, Email = Email, CompanyTitle = CompanyTitle };
                int result = await db.SaveItemAsync(model);
                if (result == 1) { UserDialogs.Instance.Toast("Registration was successful"); refresh(); }
                else { UserDialogs.Instance.Toast("Registration failed"); }
            }
        }
    }
}
