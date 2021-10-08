using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using AndroidX.Core.App;
using Muazun.Droid;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;

[assembly: Dependency(typeof(AndroidNotificationManager))]
namespace Muazun.Droid
{
    public class AndroidNotificationManager : INotificationManager
    {
        const string FajrAdhanChannelId = "fajradhan";
        const string FajrAdhanChannelName = "FajrAdhan";
        const string AdhanChannelId = "adhan";
        const string AdhanChannelName = "Adhan";
        const string ChannelDescription = "Channel for adhan notifications.";

        public const string TitleKey = "title";
        public const string MessageKey = "message";

        bool channelsInitialized = false;
        int messageId = 0;
        int pendingIntentId = 0;

        NotificationManager manager;

        public event EventHandler NotificationReceived;

        public static AndroidNotificationManager Instance { get; private set; }

        public AndroidNotificationManager() => Initialize();

        public void Initialize()
        {
            if (Instance == null)
            {
                CreateNotificationChannels();
                Instance = this;
            }
        }

        public void SendNotification(string title, string message, bool isFajr, DateTime? notifyTime = null)
        {
            if (!channelsInitialized)
            {
                CreateNotificationChannels();
            }

            if (notifyTime != null)
            {
                Intent intent = new Intent(AndroidApp.Context, typeof(AlarmHandler));
                intent.PutExtra(TitleKey, title);
                intent.PutExtra(MessageKey, message);

                PendingIntent pendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, pendingIntentId++, intent, PendingIntentFlags.CancelCurrent);
                long triggerTime = GetNotifyTime(notifyTime.Value);
                AlarmManager alarmManager = AndroidApp.Context.GetSystemService(Context.AlarmService) as AlarmManager;
                alarmManager.Set(AlarmType.RtcWakeup, triggerTime, pendingIntent);
            }
            else
            {
                Show(title, message, isFajr);
            }
        }

        public void ReceiveNotification(string title, string message, bool isFajr)
        {
            var args = new NotificationEventArgs()
            {
                Title = title,
                Message = message,
                IsFajr = isFajr,
            };
            NotificationReceived?.Invoke(null, args);
        }

        public void Show(string title, string message, bool isFajr)
        {
            Intent intent = new Intent(AndroidApp.Context, typeof(MainActivity));
            intent.PutExtra(TitleKey, title);
            intent.PutExtra(MessageKey, message);

            PendingIntent pendingIntent = PendingIntent.GetActivity(AndroidApp.Context, pendingIntentId++, intent, PendingIntentFlags.UpdateCurrent);

            var channelId = isFajr ? FajrAdhanChannelId : AdhanChannelId;

            // Set custom push notification sound.
            NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, channelId)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetLargeIcon(BitmapFactory.DecodeResource(AndroidApp.Context.Resources, Resource.Drawable.xamagonBlue))
                .SetSmallIcon(Resource.Drawable.xamagonBlue)
                .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);

            Notification notification = builder.Build();
            manager.Notify(messageId++, notification);
        }

        void CreateNotificationChannels()
        {

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                CreateNotificationChannel(
                    FajrAdhanChannelId,
                    FajrAdhanChannelName,
                    true);
                CreateNotificationChannel(
                    AdhanChannelId,
                    AdhanChannelName,
                    false);
            }

            channelsInitialized = true;
        }

        void CreateNotificationChannel(
            string channelId,
            string channelName,
            bool isFajr)
        {
            var channelNameJava = new Java.Lang.String(channelName);
            var channel = new NotificationChannel(channelId, channelNameJava, NotificationImportance.High)
            {
                Description = ChannelDescription
            };

            var audioattributes = new AudioAttributes.Builder()
                .SetContentType(AudioContentType.Sonification)
                .SetUsage(AudioUsageKind.Notification)
                .Build();

            var pathToSoundFile = isFajr
                ? $"android.resource://{AndroidApp.Context.PackageName}/{Resource.Raw.FajrAdhanMakkah}"
                : $"android.resource://{AndroidApp.Context.PackageName}/{Resource.Raw.AdhanMakkah}";
            var soundUri = Android.Net.Uri.Parse(pathToSoundFile);
            channel.SetSound(soundUri, audioattributes);

            manager = (NotificationManager)AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);
            manager.CreateNotificationChannel(channel);
        }

        long GetNotifyTime(DateTime notifyTime)
        {
            DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(notifyTime);
            double epochDiff = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;
            long utcAlarmTime = utcTime.AddSeconds(-epochDiff).Ticks / 10000;
            return utcAlarmTime; // milliseconds
        }
    }
}