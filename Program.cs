using System;
using System.Windows.Forms;

namespace Pikachu
{
    internal static class Program
    {
        private static string? currentPlayName;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var loginForm = new LoginFormcs();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                currentPlayName = loginForm.PlayerName;
                Application.Run(new Form1(currentPlayName));
            }
        }
    }
}