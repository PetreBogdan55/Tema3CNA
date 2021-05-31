using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpcWpfSample.Client.Wpf.Models
{
    class Message
    {
        public Message(String user, String messageTxt, String date)
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

        private String date;

        public String Date
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

