using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using System.Text;

namespace serv_dial
{
    public partial class serv_dial : ServiceBase
    {
        public serv_dial()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            System.Timers.Timer T2 = new System.Timers.Timer();
            T2.Interval = 60000;
            T2.AutoReset = true;
            T2.Enabled = true;
            T2.Start();
            T2.Elapsed += new System.Timers.ElapsedEventHandler(T2_Elapsed);
            // System.Diagnostics.Process.Start("c:/dial/dial.cmd" );
            //System.Windows.Forms.MessageBox.Show()ж


        }
        private void T2_Elapsed(object sender, EventArgs e)
        {
            //biblioteka.methodName();
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            IPAddress address = IPAddress.Parse("172.16.137.231");
            PingReply reply = pingSender.Send(address, timeout, buffer, options);
            if (reply.Status != IPStatus.Success)
            {
                System.Diagnostics.Process.Start("c:/dial/dial.cmd");
                System.Diagnostics.Process.Start("c:/dial/dial_.cmd");
            }
        }
        protected override void OnStop()
        {
            System.Diagnostics.Process.Start("c:/dial/dial_.cmd");
        }
    }
}
