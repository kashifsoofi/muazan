using System;
namespace Muazun
{
    public interface INotificationManager
    {
        event EventHandler NotificationReceived;
        void Initialize();
        void SendNotification(string title, string message, bool isFajr, DateTime? notifyTime = null);
        void ReceiveNotification(string title, string message, bool isFajr);
    }
}
