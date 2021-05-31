using ChatApp.Common;
using Google.Protobuf.WellKnownTypes;
using GrpcWpfSample.Client.Wpf.Commands;
using GrpcWpfSample.Client.Wpf.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace GrpcWpfSample.Client.Wpf.ViewModel
{
    class ChatViewModel : BasePropertyChanged
    {
        public ChatViewModel(string username)
        {
            FontSize = ApplicationData.ApplicationData.Default_FontSize;
            LocalUsername = username;
            MainChatVisibility = "Visible";
            BindingOperations.EnableCollectionSynchronization(ChatHistory, m_chatHistoryLockObject);
            BindingOperations.EnableCollectionSynchronization(UserList, m_chatHistoryUsersLockObject);
            StartReadingChatServer();
            StartReadingChatUsers();
        }

        #region BindingElements

        private readonly ChatServiceClient m_chatService = new ChatServiceClient();
        public static ObservableCollection<string> UserList { get; set; } = new ObservableCollection<string>();
        private readonly object m_chatHistoryUsersLockObject = new object();
        public static ObservableCollection<Message> ChatHistory { get; } = new ObservableCollection<Message>();
        private readonly object m_chatHistoryLockObject = new object();

        private int fontSize;

        public int FontSize
        {
            get { return fontSize; }
            set { fontSize = value; NotifyPropertyChanged("FontSize"); }
        }

        private string localUsername;

        public string LocalUsername
        {
            get { return localUsername; }
            set { localUsername = value; NotifyPropertyChanged("LocalUsername"); }
        }

        private String mainChatVisibility;
        public String MainChatVisibility
        {
            get { return mainChatVisibility; }
            set { mainChatVisibility = value; NotifyPropertyChanged("MainChatVisibility"); }
        }

        private String messageTxt;

        public String MessageTxt
        {
            get { return messageTxt; }
            set { messageTxt = value; NotifyPropertyChanged("MessageTxt"); }
        }

        private string newLocalUsername;

        public string NewLocalUsername
        {
            get { return newLocalUsername; }
            set { newLocalUsername = value; NotifyPropertyChanged("NewLocalUsername"); TextIsValid(); }
        }

        private string warningLogin;

        public string WarningLogin
        {
            get { return warningLogin; }
            set { warningLogin = value; NotifyPropertyChanged("WarningLogin"); }
        }

        #endregion BindingElements

        #region Commands

        private ICommand disconnectCommand;

        public ICommand DisconnectCommand
        {
            get
            {
                if (disconnectCommand == null)
                {
                    disconnectCommand = new RelayCommand(DisconnectAndLeaveMeeting);
                }
                return disconnectCommand;
            }
        }

        private ICommand sendCommand;

        public ICommand SendCommand
        {
            get
            {
                if (sendCommand == null)
                {
                    sendCommand = new RelayCommand(SendMessage);
                }
                return sendCommand;
            }
        }


        #endregion Commands


        #region Methods

        private void DisconnectAndLeaveMeeting(object obj)
        {
            App.Current.MainWindow.DataContext = new LoginViewModel();
        }

        private void SendMessage(object obj)
        {
            if (string.IsNullOrEmpty(MessageTxt))
                return;

            WriteCommandExecute(MessageTxt);
            MessageTxt = String.Empty;

        }

        private bool TextIsValid()
        {
            if (string.IsNullOrEmpty(NewLocalUsername))
            {
                WarningLogin = "Username is empty!";
                return false;
            }
            Regex regex = new Regex("^[a-zA-Z ]+$");
            if (!regex.IsMatch(NewLocalUsername))
            {
                WarningLogin = "Username can contain only letters!";
                return false;
            }
            if (NewLocalUsername[NewLocalUsername.Length - 1] == ' ')
            {
                WarningLogin = "Last character can't be a whitespace!";
                return false;
            }
            WarningLogin = String.Empty;
            return true;
        }

        private void StartReadingChatServer()
        {
            var cts = new CancellationTokenSource();
            _ = m_chatService.ChatLogs()
                .ForEachAsync((x) => ChatHistory.Add(new Message(x.Name, x.Content, x.At.ToDateTime().ToLocalTime().ToString("HH:mm:ss"))), cts.Token);
            App.Current.Exit += (_, __) => cts.Cancel();
        }

        private void StartReadingChatUsers()
        {
            var cts = new CancellationTokenSource();
            _ = m_chatService.ChatUsers()
                .ForEachAsync((x) => {
                    AddUser(x.Name);
                }, cts.Token);
            App.Current.Exit += (_, __) => cts.Cancel();
        }

        private async void WriteCommandExecute(string content)
        {
            await m_chatService.Write(new ChatLog
            {
                Name = LocalUsername,
                Content = content,
                At = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
            });
        }

        public void AddUser(string user)
        {
            if (!UserList.Contains(user))
                UserList.Add(user);
        }

        #endregion Methods
    }
}
