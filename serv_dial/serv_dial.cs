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
        {//
            string my_ip = "172.16.137.1";
            string ip1 = "172.16.137.11";
            string ip2 = "172.16.137.12";
            string ip3 = "172.16.137.13";
            string wip1 = "154.41.5.12";
            string wip2 = "91.240.97.145";
            string wip3 = "193.160.225.58";
            string nam1 = "pro1";
            string nam2 = "pro2";
            string nam3 = "pro3";

            //biblioteka.methodName();
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            IPAddress address = IPAddress.Parse(ip1);
            PingReply reply = pingSender.Send(address, timeout, buffer, options);
            if (reply.Status != IPStatus.Success)
            {
            command_(@"cmd.exe", @" /c c:\windows\system32\rasdial.exe global kil_rdp_servSM1 112267qwz >> C:\dial\in.txt");

                // System.Diagnostics.Process.Start("rasdial",@" global kil_rdp_servSM1 112267qwz >> C:\dial\in.txt");
                //              System.Diagnostics.Process.Start("c:/dial/dial.cmd");
                //            System.Diagnostics.Process.Start("c:/dial/dial_.cmd");
            }

        }
            private string command_(string FileName, string Arguments)
            {
                string output;
                using (var p = new System.Diagnostics.Process())
                {//узнаем PID сервиса AnyDesk
                    p.StartInfo.FileName = FileName;
                    p.StartInfo.Arguments = Arguments;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.StandardOutputEncoding = Encoding.GetEncoding("CP866");
                    p.Start();
                    output = p.StandardOutput.ReadToEnd();
                }
                 return output;
            }
        protected override void OnStop()
        {
            System.Diagnostics.Process.Start("c:/dial/dial_.cmd");
        }
    }
}
