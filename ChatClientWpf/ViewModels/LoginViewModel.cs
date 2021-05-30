using ChatClientWpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatClientWpf.ViewModels
{
    class LoginViewModel : BasePropertyChanged
    {
        public LoginViewModel()
        {

        }

        #region BindingElements

        private string userLogin;

        public string UserLogin
        {
            get { return userLogin; }
            set { userLogin = value; NotifyPropertyChanged("UserLogin"); TextIsValid(); }
        }

        private string warningLogin;

        public string WarningLogin
        {
            get { return warningLogin; }
            set { warningLogin = value; NotifyPropertyChanged("WarningLogin"); }
        }

        #endregion BindingElements

        #region Commands

        private ICommand loginCommand;

        public ICommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                {
                    loginCommand = new RelayCommand(LoginAndConnectToChat);
                }
                return loginCommand;
            }
        }

        #endregion Commands

        #region Methods

        private void LoginAndConnectToChat(object obj)
        {
            if (!TextIsValid())
            {
                return;
            }
            //App.Current.MainWindow.DataContext = new ChatViewModel(UserLogin);
        }

        private bool TextIsValid()
        {
            if (string.IsNullOrEmpty(UserLogin))
            {
                WarningLogin = "Username is empty!";
                return false;
            }
            Regex regex = new Regex("^[a-zA-Z ]+$");
            if (!regex.IsMatch(UserLogin))
            {
                WarningLogin = "Username can contain only letters!";
                return false;
            }
            if (UserLogin[UserLogin.Length - 1] == ' ')
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
