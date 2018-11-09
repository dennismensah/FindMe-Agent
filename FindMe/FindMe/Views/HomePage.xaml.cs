using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using FindMe.Data;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindMe.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HomePage : TabbedPage
	{
		public HomePage ()
		{
			InitializeComponent ();
		}


	    void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        var item = e.SelectedItem as User;
	    }

	    private async void TakePhoto_Clicked(object sender, EventArgs e)
	    {

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Feature Unavailable", "No camera was detected", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            photo.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });
        }

	    private async void PickPhoto_Clicked(object sender, EventArgs e)
	    {
	        await CrossMedia.Current.Initialize();

	        if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
	        {
	            await DisplayAlert("Feature Unavailable", "No camera was detected", "OK");
	            return;
	        }

	        var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
	            return;

	        await DisplayAlert("File Location", file.Path, "OK");

	        photo.Source = ImageSource.FromStream(() =>
	        {
	            var stream = file.GetStream();
	            return stream;
	        });
        }
    }
}