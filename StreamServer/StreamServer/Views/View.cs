using StreamServer.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using View = StreamServer.Views.View;

namespace StreamServer.Views
{
    public partial class View : Form
    {
        private Controller myController = new Controller();

        public View()
        {
            InitializeComponent();
            listen.Click += myController.Listen;
        }

        //return the port number
        public string ServerPort => port.Text;

        public void DisableListenButton()
        {
            this.listen.Enabled = false;
        }

        //return the server IP
        public IPAddress ServerIP => IPAddress.Parse(this.ServerIPAddress.Text);

        //delegate to use function as a parameter
        delegate void SetInfoCallback(String info);

        //append text to the server box
        public void SetServerBox(string msg)
        {
            var text = msg;
            SetInfoCallback callback = new SetInfoCallback(AddServerText);
            this.Invoke(callback, new object[] { text });
        }

        public void AddServerText(string msg)
        {
            this.serverStatus.Text += msg;
        }

        //append text to the client box
        public void SetClientBox(string msg)
        {
            var text = msg;
            SetInfoCallback callback = new SetInfoCallback(AddClientText);
            this.Invoke(callback, new object[] { text });
        }

        public void AddClientText(string msg)
        {
            this.clientRequests.Text += msg;
        }

        //if we want to change the server IP
        public void SetServerIP(string msg)
        {
            var text = msg;
            this.ServerIPAddress.Text = text;
        }
    }
}
