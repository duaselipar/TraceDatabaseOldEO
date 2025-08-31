using System;
using System.Windows.Forms;
using TraceDatababeOld4;

namespace TraceDatababeOld4
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
