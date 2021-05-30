using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientWpf.ViewModels
{
    class ChatViewModel
    {
        private string userLogin;

        public ChatViewModel(string userLogin)
        {
            this.userLogin = userLogin;
        }
    }
}
