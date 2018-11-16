using System.Windows;
using System.Windows.Controls;
using Registration.ViewModels;

namespace Registration.Views
{
    /// <summary>
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : UserControl
    {
        
        public UserProfile()
        {
            this.DataContext = new UserProfileViewModel();
            InitializeComponent();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.MainWindow;
            var presenter = window.FindName("RegistrationPresent") as ContentPresenter;
            presenter.ContentTemplate = window.FindResource("Registration") as DataTemplate;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var window = Application.Current.MainWindow;
            var presenter = window.FindName("RegistrationPresent") as ContentPresenter;
            presenter.ContentTemplate = window.FindResource("Login") as DataTemplate;
        }
    }
}
