using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Security.Principal;
using Microsoft.Win32;

namespace wall
{
    public partial class Form2 : Form
    {

        // private bool m_bFixed = false;
        private int m_iMonitor = 0;
        public Form2()
        {
            Background();
            InitializeComponent();
            Monitor = m_iMonitor;
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            axWindowsMediaPlayer1.URL = Form1.pathvalue;
            axWindowsMediaPlayer1.settings.autoStart = true;
            axWindowsMediaPlayer1.settings.setMode("loop", true);

            //Background();
        }

        public int Monitor
        {
            get
            {
                return m_iMonitor;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                else if (value >= Screen.AllScreens.Length)
                {
                    value = 0;
                }

                if (m_iMonitor != value)
                {
                    m_iMonitor = value;

                    Background();
                }
            }
        }

        public bool Background()
        {
            Form1.m_bFixed = wall.Wallpaper.Background(this.Handle);

            if (Form1.m_bFixed)
            {
                Utility.FillMonitor(this, MonitorInfo);
            }

            return Form1.m_bFixed;
        }

        public WinApi.MONITORINFO MonitorInfo
        {
            get
            {
                if (Monitor < Utility.g_staticMONITORINFO.Length)
                    return Utility.g_staticMONITORINFO[Monitor];

                return new WinApi.MONITORINFO()
                {
                    rcMonitor = Screen.PrimaryScreen.Bounds,
                    rcWork = Screen.PrimaryScreen.WorkingArea,
                };
            }
        }

        public bool Fixed
        {
            get
            {
                return Form1.m_bFixed;
            }
        }

        //public bool Fixed
        //{
        //    get
        //    {
        //        return m_bFixed;
        //    }
        //}


    }
}
