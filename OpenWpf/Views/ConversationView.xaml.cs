namespace OpenWpf.Views
{
    using OpenWpf.ViewModels;
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for SendMessageView.xaml
    /// </summary>
    public partial class ConversationView : UserControl
    {
        public ConversationView()
        {
            InitializeComponent();
            DataContext = ConversationsViewModel.Instance.SelectedConversation;

            ConversationsViewModel.Instance.PropertyChanged += Instance_PropertyChanged; ;
            ConversationsViewModel.Instance.SelectedConversation.Messages.CollectionChanged += ItemsSource_CollectionChanged;
        }

        public static Visual? GetDescendantByType(Visual element, Type type)
        {
            if (element == null) return null;
            if (element.GetType() == type) return element;
            Visual? foundElement = null;
            if (element is FrameworkElement frameworkElement)
                _ = frameworkElement.ApplyTemplate();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual? visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendantByType(visual, type);
                if (foundElement != null)
                    break;
            }

            return foundElement;
        }

       
        private void ConversationView_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Content" || (e.PropertyName == "FinishReason" && (sender as MessageViewModel) == ConversationsViewModel.Instance.SelectedConversation.Messages.Last()))
            {
                ScrollViewer? scrollViewer = GetDescendantByType(messageList, typeof(ScrollViewer)) as ScrollViewer;

                scrollViewer?.ScrollToEnd();
            }
        }

        private void Instance_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedConversation")
            {
                DataContext = ConversationsViewModel.Instance.SelectedConversation;
                ScrollViewer? scrollViewer = GetDescendantByType(messageList, typeof(ScrollViewer)) as ScrollViewer;

                scrollViewer?.ScrollToEnd();
            }
        }

        private void ItemsSource_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (object? item in e.NewItems)
                {
                    (item as MessageViewModel).PropertyChanged += ConversationView_PropertyChanged;
                }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                foreach (object? item in e.OldItems)
                {
                    (item as MessageViewModel).PropertyChanged -= ConversationView_PropertyChanged;
                }
            }
        }

    
    }
}