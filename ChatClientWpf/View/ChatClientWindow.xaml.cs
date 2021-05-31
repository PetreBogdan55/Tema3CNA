using GrpcWpfSample.Client.Wpf.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace GrpcWpfSample.Client.Wpf.View
{
    public partial class ChatClientWindow : Window
    {
        public ChatClientWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }
    }
}
