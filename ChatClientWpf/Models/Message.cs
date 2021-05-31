using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatClientWpf.ApplicationData;

namespace ChatClientWpf.Models
{
    class Message
    {
        public Message(String user, String messageTxt, DateTime date)
        {
            User = user;
            MessageTxt = messageTxt;
            Date = date;
            FontSize = ApplicationData.ApplicationData.FontSize;
        }

        private String user;

        public String User
        {
            get { return user; }
            set { user = value; }
        }

        private String messageTxt;

        public String MessageTxt
        {
            get { return messageTxt; }
            set { messageTxt = value; }
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private int fontSize;

        public int FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
        }
    }
}
