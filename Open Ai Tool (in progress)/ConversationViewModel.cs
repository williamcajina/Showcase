using AiDos.Messages;
using AiDos.Models;
using AiDos.Services.Interfaces;
using AiDos.Utilities.Extensions;
using CommunityToolkit.Maui.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace AiDos.ViewModels
{
    public class ConversationViewModel : ObservableRecipient
    {
        private readonly IAiService _aiService;
        public Conversation Convesation { get; set; }

        private readonly IConversationService _conversationService;
        private readonly IFileStorageService _fileStorageService;
        private readonly Settings _settings;
     
        private CancellationTokenSource _sendMessageCancellationToken;
        private IDispatcher _dispatcher;
        private ImageSource _imageSource;
        private bool _isSending;
        private string _messageText;
        public ICommand CancelSendMessageCommand { get; private set; }
        public CollectionView CollectionView { get; set; }
        private string _conversationName;
        public string ConversationName { get => _conversationName; set => SetProperty(ref _conversationName, value); }
        public bool IsLoading { get; private set; }
        public bool IsSending { get => _isSending; set => SetProperty(ref _isSending, value); }
        public IAsyncRelayCommand ListenCommand { get; private set; }
        public ObservableCollection<MessageViewModel> Messages { get; private set; }

        public string MessageText
        {
            get => _messageText; set
            {
                SetProperty(ref _messageText, value);

                var newSource = !string.IsNullOrEmpty(value) ? "messagefilled.png" : "messageempty.png";
                SendMessageCommand.NotifyCanExecuteChanged();
                if (!SendButtonImageSource.ToString().Contains(newSource))
                {
                    SendButtonImageSource = newSource;
                }
            }
        }

        public ICommand NewConversationCommand { get; private set; }
        public ICommand PickImageCommand { get; private set; }
        public ImageSource SendButtonImageSource { get => _imageSource; set { SetProperty(ref _imageSource, value); } }
        public IAsyncRelayCommand SendMessageCommand { get; private set; }

        public ConversationViewModel(IAiService aiService, IConversationService conversationService, IFileStorageService fileStorageService, IDispatcher dispacher, Conversation conversation, Settings settings)
        {
            NewButtonIsVisible = false;
            ConversationName = GlobalConstants.DefaultConversationName;
            _dispatcher = dispacher;
            _settings = settings;
            Convesation = conversation;
            _fileStorageService = fileStorageService;
            _imageSource = ImageSource.FromFile("messageempty.png");
            Messages = new ObservableCollection<MessageViewModel>(Convesation.Messages.Select(m => new MessageViewModel(m, _fileStorageService, true)));

            _aiService = aiService;
            _conversationService = conversationService;
            ListenCommand = new AsyncRelayCommand(Listen);
            PickImageCommand = new RelayCommand(PickImage);
            SendMessageCommand = new AsyncRelayCommand(SendMessageAsync, CanSendMessage);
            CancelSendMessageCommand = new RelayCommand(CancelSendMessage);
            NewConversationCommand = new RelayCommand(() => { StartNewConversation(); WeakReferenceMessenger.Default.Send(new NewConversationMessage()); });
            WeakReferenceMessenger.Default.Register<ConversationViewModel, ConversationDeletedMessage>(this, (r, m) => {if(m.ConversationId == conversation.ConversationId) StartNewConversation(); });
            WeakReferenceMessenger.Default.Register<ConversationViewModel, NewConversationMessage>(this, (r, m) => { StartNewConversation(); });
            WeakReferenceMessenger.Default.Register<ConversationViewModel, ConversationNameChangedMessage>(this, (r, m) => { OnConversationNameChanged(m); });
            WeakReferenceMessenger.Default.Register(this, (MessageHandler<ConversationViewModel, ConversationSelectedMessage>)(async (r, m) => { await OnConversationSelected(fileStorageService, m); }));

        }

        private bool _newButtonIsVisible;
        public bool NewButtonIsVisible { get => _newButtonIsVisible; set => SetProperty(ref _newButtonIsVisible, value); }
        private async Task OnConversationSelected(IFileStorageService fileStorageService, ConversationSelectedMessage m)
        {
            if (IsSending)
                CancelSendMessage();

            Messages.RemoveAll(p => true);
            WeakReferenceMessenger.Default.Send(new DisposeMessageSubscriptionsMessage());
            Convesation.Messages.Clear();
            Convesation.ConversationId = m.ConversationId;

            var currentConversationId = m.ConversationId;
            OnConversationNameChanged(new ConversationNameChangedMessage(m.ConversationId));
            await foreach (var message in _conversationService.LoadConversationMessageAsStreamAsync(m.ConversationId))
            {
                if (currentConversationId != Convesation.ConversationId)
                    break;

                Messages.Add(new MessageViewModel(message, fileStorageService, true));
                await Task.Delay(15);
            }

            NewButtonIsVisible = true;
        }

        private CancellationTokenSource _changingConversationNameCancellationToken;

        private async void OnConversationNameChanged(ConversationNameChangedMessage conversationNameChangedMessage)
        {
            // Cancel the previous task if it's still running
            _changingConversationNameCancellationToken?.Cancel();

            // Create a new cancellation token
            _changingConversationNameCancellationToken = new CancellationTokenSource();
            var cancellationToken = _changingConversationNameCancellationToken.Token;

            ConversationName = string.Empty;

            try
            {
                foreach (var letter in conversationNameChangedMessage.ConversationId.Name)
                {
                    // Throw an exception if cancellation is requested
                    cancellationToken.ThrowIfCancellationRequested();

                    ConversationName += letter;
                    await Task.Delay(15, cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
         
                Console.WriteLine("Operation was cancelled");
            }

            if (ConversationName != GlobalConstants.DefaultConversationName)
                NewButtonIsVisible = true;
        }


        public void CancelSendMessage()
        {
            _sendMessageCancellationToken?.Cancel();
            IsSending = false;
        }

        public bool CanSendMessage() => !string.IsNullOrWhiteSpace(MessageText);

        public void SaveConversation()
        {
            if (string.IsNullOrEmpty(Messages.Last().Content))
                return;
            Convesation.Messages = Messages.Select(mvm => mvm.Message).ToList();
            _conversationService.SaveConversationAsync(Convesation);
            WeakReferenceMessenger.Default.Send(new ConversationSavedMessage(Convesation));
            NewButtonIsVisible = true;
        }

        public async Task SendMessageAsync()
        {
            if (!CanSendMessage())
                return;

            var newMessage = new Message { Sender = "User", Content = MessageText, IsAi = false };
            MessageViewModel ourMessage = new(newMessage, _fileStorageService, true);
            Messages.Add(ourMessage);

            MessageText = string.Empty;
            IsSending = true;
            var aiMessage = new MessageViewModel(new Message { Sender = "Ai", Content = "", IsAi = true }, _fileStorageService, false);

            _sendMessageCancellationToken?.Cancel();
            _sendMessageCancellationToken = new CancellationTokenSource();
            var token = _sendMessageCancellationToken.Token;
            try
            {
                Messages.Add(aiMessage);
                aiMessage.IsProcessing = true;
                IEnumerable<Message> messagesForService = Messages.Take(Messages.Count - 1).Select(x => x.Message);
                await Task.Run(async () =>
                {
                    await foreach (DTOs.ChatResponseDTO dto in _aiService.ChatRequestStream(messagesForService, _settings, token))
                    {
                        if (dto.Content != null)
                        {
                            Helper.UpdateLoadingMessage("Adding letter");
                            _dispatcher.Dispatch(() =>
                            {
                                aiMessage.AddContent(dto.Content);
                            });
                        }
                    }
                });

                SaveConversation();
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"An error occurred: {e.Message}", "OK");
            }
            finally
            {
                IsSending = false;
                aiMessage.FinalizeMessage();

            }
        }

        private async Task Listen()
        {
            var isGranted = await SpeechToText.Default.RequestPermissions(CancellationToken.None);
            if (!isGranted)
                throw new Exception("fuckl you");

            var recognitionResult = await SpeechToText.Default.ListenAsync(
                CultureInfo.GetCultureInfo("en-US"),
                new Progress<string>(partialText =>
                {
                    MessageText += partialText;
                }),
                CancellationToken.None
            );
            if (recognitionResult.IsSuccessful)
                MessageText = recognitionResult.Text;
            else
                throw new Exception("fuckl you");

        }

        private async void PickImage()
        {
            var result = await MediaPicker.PickPhotoAsync();
            if (result != null)
                MessageText += result.FullPath;
        }

        private void StartNewConversation()
        {
            CancelSendMessage();
            Messages.RemoveAll(p => true);
            ConversationName = GlobalConstants.DefaultConversationName;
            Convesation.ConversationId = new ConversationId() { Id = Guid.NewGuid(), Name = GlobalConstants.DefaultConversationName };
            Convesation.Messages.Clear();
            WeakReferenceMessenger.Default.Send(new DisposeMessageSubscriptionsMessage());
            NewButtonIsVisible = false;
        }
    }

}