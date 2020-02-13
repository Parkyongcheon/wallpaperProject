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
        //유튜브 링크를 받기위한 프롬포트을 위한 변수
        public static string input = "";

        public static bool m_bFixed = false;

        public Form1()
        {   
            
            //MessageBox.Show(Convert.ToString(System.Windows.Forms.Screen.AllScreens.Length));
            InitializeComponent();

            // webBrowser1.Navigate(new Uri("https://www.youtube.com/embed/yqtCGojXEpM?autoplay=1"));
        }

        //파일 오프너
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
        //리스트을 클릭할 경우 파일을 불러왔을 때 저장한 것의 경로를 불러와서 윈도우 미디어에 미리보기 형식으로 띄움
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
        //트레이 버튼을 만들기 위한 함수 
        //Hide가 되면 form화면은 사라지고 트레이 화면에 표시
        private void NotifyTest_Resize(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }
        //트레이로 간 버튼을 더블클릭할 경우 다시 윈폼이 나타난다.
        private void notify_doubleClk(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }
        
        //youtube로 백그라운드 재생을 위해서 만든 버튼
        private void button1_Click(object sender, EventArgs e)
        {
            string str = "https://www.youtube.com/embed/";
            input = Microsoft.VisualBasic.Interaction.InputBox("유튜브 링크 입력", "YouTube Link input prompt", "", 0, 0);
            if(input.Length == 0)
            {
                MessageBox.Show("유튜브 링크를 제대로 입력해주세요");
            }
            else if(input.Contains(str) == false)
            {
                MessageBox.Show("유튜브 링크를 제대로 입력해주세요");
            }
            else
            {
                Form3 form3 = new Form3();
                form3.Show();

            }

        }

        
    }
}
