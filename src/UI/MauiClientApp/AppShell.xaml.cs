using MauiClientApp.Email.EmailDetail.Pages;
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
            Routing.RegisterRoute(nameof(EmailDetailPage), typeof(EmailDetailPage));
        }
    }
}
