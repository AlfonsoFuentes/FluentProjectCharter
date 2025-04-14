using FluentWeb.Infrastructures.Identity.Account;

namespace FluentWeb.Infrastructures.NotificacionService
{
    public interface ISnackBar : IManagetAuth
    {
        void ShowError(string message);
        void ShowError(List<string> message);
        void ShowSuccess(string message);
        void ShowSuccess(List<string> message);
    }
    public class SnackBar : ISnackBar
    {
        //IToastService ToastService;

        public SnackBar(/*IToastService toastService*/)
        {
            //ToastService = toastService;
        }

        public void ShowSuccess(string message)
        {
            //ToastService.ShowSuccess(message, 4000);
        }

        public void ShowSuccess(List<string> message)
        {
            foreach (var item in message)
            {
                ShowSuccess(item);
            }
        }
        public void ShowError(string message)
        {
            //ToastService.ShowError(message,4000);
        }

        public void ShowError(List<string> message)
        {
            foreach (var item in message)
            {
                ShowError(item);
            }
        }
    }
}
