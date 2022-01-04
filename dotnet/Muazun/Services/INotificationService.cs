using System;
namespace Muazun.Services
{
    public interface INotificationService
    {
        event EventHandler NotificationReceived;
        void Initialize();
        void SendNotification(string title, string message, bool isFajr, DateTime? notifyTime = null);
        void ReceiveNotification(string title, string message, bool isFajr);
    }
}
