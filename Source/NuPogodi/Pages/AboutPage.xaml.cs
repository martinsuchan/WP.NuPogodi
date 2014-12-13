using System.Windows.Input;
using NuPogodi.ViewModel;

namespace NuPogodi
{
    public partial class AboutPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void NameTextBoxTap(object sender, GestureEventArgs e)
        {
            ((AboutViewModel)DataContext).BuyCmd.Execute(null);
        }

        private void TwitterBoxTap(object sender, GestureEventArgs e)
        {
            ((AboutViewModel)DataContext).ShowTwitterCmd.Execute(null);
        }

        private void EmailBoxTap(object sender, GestureEventArgs e)
        {
            ((AboutViewModel)DataContext).FeedbackCmd.Execute(null);
        }
    }
}
