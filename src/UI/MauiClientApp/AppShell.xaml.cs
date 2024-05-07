using MauiClientApp.Email.EmailList.Pages;

namespace MauiClientApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(EmailListPage), typeof(EmailListPage));
        }
    }
}
