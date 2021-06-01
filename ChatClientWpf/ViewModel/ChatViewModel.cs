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
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;

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
               .ForEachAsync((x) => { Application.Current.Dispatcher.Invoke(() => { ChatHistory.Add(new Message(x.Name, DivideContentInRuns(x.Content), x.At.ToDateTime().ToLocalTime().ToString())); }); }, cts.Token);

            App.Current.Exit += (_, __) => cts.Cancel();
        }

        private List<Run> DivideContentInRuns(string content)
        {
            var runList = new List<Run>();
            var signStack = new Stack<char>();
            char[] specialSigns = { '*', '_', '~', '`' };
            var index = 0;
            var splitContent = content.Split(' ');
            var fontWeight = Enums.FontWeight.NONE;
            var fontStyle = Enums.FontStyle.NONE;
            var textDecoration = Enums.TextDecoration.NONE;
            String newSplit;

            foreach (var split in splitContent)
            {
                fontWeight = Enums.FontWeight.NONE;
                fontStyle = Enums.FontStyle.NONE;
                textDecoration = Enums.TextDecoration.NONE;
                newSplit = split;
                index = 0;
                signStack.Clear();
                foreach (var splitCharacter in split)
                {
                    if (specialSigns.Contains(splitCharacter))
                    {
                        signStack.Push(splitCharacter);
                    }
                    ++index;
                }
                var initialCount = signStack.Count;
                String order = String.Empty;
                foreach (var sign in signStack)
                {
                    order += sign;
                }
                while (signStack.Count != initialCount / 2)
                {
                    var top = signStack.Peek();
                    signStack.Pop();
                    var firstIndex = order.IndexOf(top);
                    if (order.ElementAt(order.Length - (firstIndex + 1)).Equals(top))
                    {
                        switch (top)
                        {
                            case '*':
                                {
                                    if (fontWeight == Enums.FontWeight.NONE)
                                    {
                                        fontWeight = GetTextFontWeight(newSplit);
                                        newSplit = newSplit.Trim('*');
                                    }
                                    break;
                                }
                            case '~':
                                {
                                    if (textDecoration == Enums.TextDecoration.NONE)
                                    {
                                        textDecoration = GetTextDecoration(newSplit);
                                        newSplit = newSplit.Trim('~');
                                    }
                                    break;
                                }
                            case '`':
                                {
                                    if (textDecoration == Enums.TextDecoration.NONE)
                                    {
                                        textDecoration = GetTextDecoration(newSplit);
                                        newSplit = newSplit.Trim('`');
                                    }
                                    break;
                                }
                            case '_':
                                {
                                    if (fontStyle == Enums.FontStyle.NONE)
                                    {
                                        fontStyle = GetTextFontStyle(newSplit);
                                        newSplit = newSplit.Trim('_');
                                    }
                                    break;
                                }
                        }
                    }
                }
                runList.Add(GetFormattedText(newSplit, fontWeight, fontStyle, textDecoration));
            }

            /*var splitList = content.Split(' ').ToList();
            foreach (var split in splitList)
            {
                if (split.ElementAt(0).Equals('*') && split.ElementAt(split.Length - 1).Equals('*'))
                {
                    var newSplit = split.Trim('*');
                    var newRun = new Run(newSplit);
                    newRun.FontWeight = FontWeights.Bold;
                    runList.Add(newRun);
                }
                else if (split.ElementAt(0).Equals('_') || split.ElementAt(split.Length - 1).Equals('_'))
                {
                    var newSplit = split.Trim('_');
                    var newRun = new Run(newSplit);
                    newRun.FontWeight = FontWeights.;
                    runList.Add(newRun);
                }
                else
                {
                    var newRun = new Run(split);
                    runList.Add(newRun);
                }
            }
            */
            return runList;
        }


        private Run GetFormattedText(String text, Enums.FontWeight fontWeight, Enums.FontStyle fontStyle, Enums.TextDecoration textDecoration)
        {
            var newRun = new Run(text);
            switch (textDecoration)
            {
                case Enums.TextDecoration.STRIKE:
                    newRun.TextDecorations = TextDecorations.Strikethrough;
                    break;
                case Enums.TextDecoration.UNDERLINE:
                    newRun.TextDecorations = TextDecorations.Underline;
                    break;
            }
            switch (fontWeight)
            {
                case Enums.FontWeight.BOLD:
                    newRun.FontWeight = FontWeights.Bold;
                    break;
            }
            switch (fontStyle)
            {
                case Enums.FontStyle.ITALIC:
                    newRun.FontStyle = FontStyles.Italic;
                    break;
            }
            return newRun;
        }

        private Enums.FontStyle GetTextFontStyle(string text)
        {
            if (text.ElementAt(0).Equals('_') && text.ElementAt(text.Length - 1).Equals('_'))
            {
                text = text.Trim('_');
                return Enums.FontStyle.ITALIC;
            }
            return Enums.FontStyle.NONE;
        }

        private Enums.FontWeight GetTextFontWeight(string text)
        {
            if (text.ElementAt(0).Equals('*') && text.ElementAt(text.Length - 1).Equals('*'))
            {
                text = text.Trim('*');
                return Enums.FontWeight.BOLD;
            }
            return Enums.FontWeight.NONE;
        }

        private Enums.TextDecoration GetTextDecoration(string text)
        {
            if (text.ElementAt(0).Equals('~') && text.ElementAt(text.Length - 1).Equals('~'))
            {
                text = text.Trim('~');
                return Enums.TextDecoration.STRIKE;
            }
            else if (text.ElementAt(0).Equals('`') && text.ElementAt(text.Length - 1).Equals('`'))
            {
                text = text.Trim('`');
                return Enums.TextDecoration.UNDERLINE;
            }
            return Enums.TextDecoration.NONE;
        }



        private void StartReadingChatUsers()
        {
            var cts = new CancellationTokenSource();
            _ = m_chatService.ChatUsers()
                .ForEachAsync((x) =>
                {
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
