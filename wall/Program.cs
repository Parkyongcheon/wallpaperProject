using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wall
{
    public class Program : ApplicationContext
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Program());
        }

        /// <summary>
        /// Form 변수 선언
        /// </summary>
        public Form1 m_Form1 = new Form1();
        //public Form2 m_Form2 = new Form2();

        /// <summary>
        /// 선언자
        /// </summary>
        Program()
        {
            m_Form1.Show();
            //m_Form2.Show();
        }
    }
}
