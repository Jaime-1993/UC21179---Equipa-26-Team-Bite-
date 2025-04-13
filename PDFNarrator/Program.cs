using System;
using System.Windows.Forms;

namespace PDFNarrator
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Controller controller = new Controller();
            controller.LaunchApp();
        }
    }
}