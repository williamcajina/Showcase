namespace OpenWpf.Views
{
    using OpenWpf.ViewModels;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for ConversationListYtemView.xaml
    /// </summary>
    public partial class ConversationListItemView : UserControl
    {
        public ConversationListItemView()
        {
            InitializeComponent();
            DeleteButton.Command = ConversationsViewModel.Instance.DeleteChatCommand;
        }

        private void CheckMarkClick(object sender, RoutedEventArgs e)
        {
            CheckMarkButton.Visibility = Visibility.Collapsed;
            EditButton.Visibility = Visibility.Visible;
            conversationName.IsReadOnly = true;
            conversationName.Focusable = false;
            conversationName.BorderBrush = null;
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            EditButton.Visibility = Visibility.Collapsed;
            CheckMarkButton.Visibility = Visibility.Visible;
            conversationName.IsReadOnly = false;
            conversationName.Focusable = true;
            conversationName.BorderBrush = Brushes.White;
        }
    }
}