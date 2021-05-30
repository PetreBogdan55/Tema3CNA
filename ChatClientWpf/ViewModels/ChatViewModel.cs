using ChatClientWpf.Commands;
using ChatClientWpf.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatClientWpf.ViewModels
{
    class ChatViewModel : BasePropertyChanged
    {
        public ChatViewModel(string username)
        {
            FontSize = ApplicationData.ApplicationData.Default_FontSize;
            LocalUsername = username;
            MainChatVisibility = "Visible";
        }

        #region BindingElements

        private ObservableCollection<Message> messages;

        public ObservableCollection<Message> Messages
        {
            get { return messages; }
            set { messages = value; NotifyPropertyChanged("Messages"); }
        }


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
            if (Messages == null)
            {
                Messages = new ObservableCollection<Message>();
            }
            Messages.Add(new Message(LocalUsername, MessageTxt, DateTime.Now));
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

        #endregion Methods
    }
}
