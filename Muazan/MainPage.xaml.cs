using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Muazan
{
    public partial class MainPage : ContentPage
    {
        INotificationManager notificationManager;
        int notificationNumber = 0;

        public MainPage()
        {
            InitializeComponent();

            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, eventArgs) =>
            {
                var evtData = (NotificationEventArgs)eventArgs;
                ShowNotification(evtData.Title, evtData.Message);
            };
        }

        public void OnPlayFajrAdhanClicked(System.Object sender, System.EventArgs e)
        {
            PlayAdhan(true);
        }

        void OnPlayAdhanClicked(System.Object sender, System.EventArgs e)
        {
            PlayAdhan(false);
        }

        void OnSendClick(System.Object sender, System.EventArgs e)
        {
            notificationNumber++;
            string title = $"Local Notification #{notificationNumber}";
            string message = $"You have now received {notificationNumber} notifications!";
            notificationManager.SendNotification(title, message, true);
        }

        void OnScheduleClick(System.Object sender, System.EventArgs e)
        {
            notificationNumber++;
            string title = $"Local Notification #{notificationNumber}";
            string message = $"You have now received {notificationNumber} notifications!";
            notificationManager.SendNotification(title, message, false, DateTime.Now.AddSeconds(10));
        }

        private void PlayAdhan(bool isFajr)
        {
            var prefix = isFajr ? "Fajr-" : "";
            var fileName = $"{prefix}Adhan-Makkah.mp3";
            DependencyService.Get<IAudioPlayer>().PlayAudioFile(fileName);
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
    }
}
