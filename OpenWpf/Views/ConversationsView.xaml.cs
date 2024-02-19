namespace OpenWpf.Views
{
    using OpenWpf.ViewModels;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for AllConversationsView.xaml
    /// </summary>
    public partial class ConversationsView : UserControl
    {
        public ConversationsView()
        {
            InitializeComponent();

            DataContext = ConversationsViewModel.Instance;
        }
    }
}