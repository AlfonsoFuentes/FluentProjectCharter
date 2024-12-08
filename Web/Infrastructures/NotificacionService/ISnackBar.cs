using Radzen;
using Web.Infrastructures.Identity.Account;

namespace Web.Infrastructures.NotificacionService
{
    public interface ISnackBar : IManagetAuth
    {
        void Add(string message, NotificationSeverity severity);

        void Add(List<string> message, NotificationSeverity severity);
    }
    public class SnackBar : ISnackBar
    {
        NotificationService NotificationService;

        public SnackBar(NotificationService notificationService)
        {
            NotificationService = notificationService;
        }

        public void Add(string message, NotificationSeverity severity)
        {
            NotificationService.Notify(new NotificationMessage
            {
                Severity = severity,
                Summary = $"{severity} Summary",
                Detail = message,
                Duration = 4000
            });
        }

        public void Add(List<string> message, NotificationSeverity severity)
        {
            foreach (var item in message)
            {
                Add(item, severity);
            }
        }
    }
}
