using System;
namespace Muazan
{
    public class NotificationEventArgs : EventArgs
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsFajr { get; set; }
    }
}
