namespace eWallet.ViewModels
{
    public class Notification
    {
        public string Type { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string AutoHide { get; set; }

        public static Notification GetNotification(string type,string title, string message, bool autohide) {
            return new Notification {
                Type = type,
                Title = title,
                Message = message,
                AutoHide = autohide.ToString()
            };
        }

        public static Notification GetError(string title, string message, bool autohide = true) {
            return GetNotification("error", title, message, autohide);
        }

        public static Notification GetSuccess(string title, string message, bool autohide = true)
        {
            return GetNotification("success", title, message, autohide);
        }
        public static Notification GetInfo(string title, string message, bool autohide = true)
        {
            return GetNotification("info", title, message, autohide);
        }
        public static Notification GetNotif(string title, string message, bool autohide = true)
        {
            return GetNotification("", title, message, autohide);
        }
    }
}