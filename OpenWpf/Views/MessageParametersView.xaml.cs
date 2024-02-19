namespace OpenWpf.Views
{
    using OpenWpf.ViewModels;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for RequestParametersView.xaml
    /// </summary>
    public partial class MessageParametersView : UserControl
    {
        public MessageParametersView()
        {
            InitializeComponent();

            DataContext = MessageParametersViewModel.Instance;
        }
    }
}