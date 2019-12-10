using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winformcsharo
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, "winformcsharo"))
            {
                bool Running = !mutex.WaitOne(0, false);
                if (!Running)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                }
                else
                {
                    // App already launched.
                    MessageBox.Show("Программа уже запущена, проверьте трэй");
                }
            }
            
        }
    }
}
