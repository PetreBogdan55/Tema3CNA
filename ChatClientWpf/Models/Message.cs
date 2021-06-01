using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace GrpcWpfSample.Client.Wpf.Models
{
    class Message
    {
        public Message(String user, List<Run> runList, String date)
        {
            User = user;
            Date = date;
            RunList = new ObservableCollection<Run>();
            runList.ForEach(RunList.Add);
            FontSize = ApplicationData.ApplicationData.FontSize;
        }

        private String user;

        public String User
        {
            get { return user; }
            set { user = value; }
        }

        private ObservableCollection<Run> runList;
        public ObservableCollection<Run> RunList
        {
            get
            {
                return runList;
            }
            set
            {
                runList = value;
            }
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

