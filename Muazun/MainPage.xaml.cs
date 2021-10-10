using System;
using System.Threading.Tasks;
using Muazun.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Muazun
{
    public partial class MainPage : ContentPage
    {
        INotificationService notificationService;
        int notificationNumber = 0;

        public MainPage()
        {
            InitializeComponent();

            notificationService = DependencyService.Get<INotificationService>();
            notificationService.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var curentDateUtc = DateTime.UtcNow;
            var currentDate = DateTime.Now;
            lblCurrentDate.Text = currentDate.ToLongDateString();

            var location = Geolocation.GetLastKnownLocationAsync().GetAwaiter().GetResult();
            var namazTime = new NamazTime();
            namazTime.SetCalcMethod(2);
            namazTime.SetAsrMethod(0);

            var diff = currentDate - curentDateUtc;
            var tz = diff.Hours;
            var times = namazTime.GetDatePrayerTimes(currentDate.Year, currentDate.Month, currentDate.Day, location.Latitude, location.Longitude, tz);
            lblFajr.Text = times[0];
            lblSunrise.Text = times[1];
            lblDhuhr.Text = times[2];
            lblAsr.Text = times[3];
            lblSunset.Text = times[4];
            lblMaghrib.Text = times[5];
            lblIsha.Text = times[6];

            // Set notification for next namaz
            // notificationService.SendNotification("Namaz", "Time for Namaz", false, DateTime.Now.AddSeconds(10));
        }

        public void OnPlayFajrAdhanClicked(object sender, EventArgs e)
        {
            PlayAdhan(true);
        }

        void OnPlayAdhanClicked(object sender, EventArgs e)
        {
            PlayAdhan(false);
        }

        void OnSendClick(object sender, EventArgs e)
        {
            notificationNumber++;
            string title = $"Local Notification #{notificationNumber}";
            string message = $"You have now received {notificationNumber} notifications!";
            notificationService.SendNotification(title, message, true);
        }

        void OnScheduleClick(object sender, EventArgs e)
        {
            notificationNumber++;
            string title = $"Local Notification #{notificationNumber}";
            string message = $"You have now received {notificationNumber} notifications!";
            notificationService.SendNotification(title, message, false, DateTime.Now.AddSeconds(10));
        }

        async void OnGetLocationClick(object sender, EventArgs e)
        {
            await GetLocation();
        }

        private void PlayAdhan(bool isFajr)
        {
            var prefix = isFajr ? "Fajr-" : "";
            var fileName = $"{prefix}Adhan-Makkah.mp3";
            DependencyService.Get<IAudioService>().PlayAudioFile(fileName);
        }

        private void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
                stackLayout.Children.Add(msg);
            });
        }

        async Task GetLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var location = await Geolocation.GetLocationAsync(request);
                if (location != null)
                {
                    lblLatitude.Text = location.Latitude.ToString();
                    lblLongitude.Text = location.Longitude.ToString();
                }
            }
            catch (FeatureNotSupportedException ex)
            {
                await DisplayAlert("Failed", ex.Message, "OK");
            }
            catch (PermissionException ex)
            {
                await DisplayAlert("Failed", ex.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Failed", ex.Message, "OK");
            }
        }
    }
}
