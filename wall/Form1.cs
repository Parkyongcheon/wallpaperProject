using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace wall
{
    public partial class Form1 : Form
    {
        List<string> pathlist = new List<string>();
        string filename = "";
        public static string pathvalue = "";


        public static bool m_bFixed = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void input_button_Click(object sender, EventArgs e)
        {
            //string filepath = "C:\\movie\\1.mp4";
            //axWindowsMediaPlayer1.URL = filepath;
            //axWindowsMediaPlayer1.settings.autoStart = true;

            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                    filename = openFileDialog.SafeFileName;
                    pathlist.Add(filePath);
                    pathvalue = filePath;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();
                    
                    

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                    listView1.Items.Add(filename);
                }
               

            }
            
            
            
            
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            try { 
            ListView iv = sender as ListView;
            iv.FullRowSelect = true;
            int num = iv.SelectedItems[0].Index;
            //name value
            ListView.SelectedListViewItemCollection items = listView1.SelectedItems;
            ListViewItem name = items[0];
            string filepath = pathlist[num];
            axWindowsMediaPlayer1.URL = filepath;
            axWindowsMediaPlayer1.settings.autoStart = true;
            axWindowsMediaPlayer1.settings.setMode("loop", true);
            
            }
            catch
            {
                
            }
            


        }
        [DllImport("User32", CharSet = CharSet.Auto)]
        //public static extern int SystemParametersInfo(int uiAction, int uiParam,
        //string pvParam, uint fWinIni);
        private static extern Int32 SystemParametersInfo(
           UInt32 uiAction, UInt32 uiParam, String pvParam, UInt32 fWinIni);

        private static UInt32 SPI_SETDESKWALLPAPER = 20;

        private static UInt32 SPIF_UPDATEINIFILE = 0x1;
        private void button2_Click(object sender, EventArgs e)
        {
            //SystemParametersInfo(0x0014, 0, pathlist[0], 0x0001);
            //SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, pathlist[0], SPIF_UPDATEINIFILE);
            axWindowsMediaPlayer1.close();
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose();
            Application.Exit();
        }

        private void NotifyTest_Resize(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notify_doubleClk(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }
    }
}
