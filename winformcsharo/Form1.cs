using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winformcsharo
{
    public partial class Form1 : Form
    {
        HotKey _hotKeyToRU = new HotKey();
        HotKey _hotKeyToJP = new HotKey();
        HotKey _hotKeyToEUW = new HotKey();
        HotKey _hotKillProc = new HotKey();
        HotKey _hotKillProcInGame = new HotKey();
        public Form1()
        {

            InitializeComponent();
            this.Text = "Region changer";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
            try
            {
                _hotKeyToRU.Key = Keys.NumPad1;
                //_hotKeyToRU.KeyModifier = HotKey.KeyModifiers.Alt;
                //_hotKeyToRU.HotKeyPressed += NotifyIcon1_Click;

                _hotKeyToJP.Key = Keys.NumPad2;
                //_hotKeyToJP.KeyModifier = HotKey.KeyModifiers.Alt;
                //_hotKeyToJP.HotKeyPressed += NotifyIcon1_Click;
                _hotKeyToEUW.Key = Keys.NumPad4;
                _hotKillProc.Key = Keys.NumPad3;
                _hotKillProc.KeyModifier = HotKey.KeyModifiers.Alt;

                _hotKillProcInGame.Key = Keys.NumPad6;
                _hotKillProcInGame.KeyModifier = HotKey.KeyModifiers.Alt;

            }
            catch (ApplicationException)
            {

            }
        }

        public static String changeFile(String toRegion)
        {

            if (Process.GetProcessesByName("LeagueClient").Length == 0 && Process.GetProcessesByName("League of Legends").Length == 0)
            {
                StreamReader streamReader = new StreamReader(@"C:\Riot Games\League of Legends\Config\LeagueClientSettings.yaml");
                string read = streamReader.ReadToEnd();
                streamReader.Close();

                string pattern = "region: \".+\"";
                Regex reg = new Regex(pattern);

                StreamWriter streamWriter = new StreamWriter(@"C:\Riot Games\League of Legends\Config\LeagueClientSettings.yaml");
                switch (toRegion)
                {
                    case "RU":
                            streamWriter.WriteLine(reg.Replace(read, "region: \"RU\""));
                        break;
                    case "JP":
                        streamWriter.WriteLine(reg.Replace(read, "region: \"JP\""));
                        break;
                    case "EUW":
                        streamWriter.WriteLine(reg.Replace(read, "region: \"EUW\""));
                        break;
                }
                streamWriter.Flush();
                streamWriter.Close();
            }
            else
            {
                plSound(false);
                return "Клиент открыт, изменить регион не удалось";
            }

            plSound(true);
            return "Регион изменён на " + toRegion;
        }


        public void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                switch(((Button)sender).Name)
                {
                    case "Russia":
                        MessageBox.Show(changeFile("RU"));
                        break;
                    case "Japan":
                        MessageBox.Show(changeFile("JP"));
                        break;
                }
            }
            catch
            {
                    MessageBox.Show("Произошла ошибка, изменить регион не удалось");
            }
            
        }

        

       

        private void NotifyIcon1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                notifyIcon1.Visible = false;
            }
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

       
        

        private static void plSound(Boolean b)
        {
            if (b)
            {
                SoundPlayer player = new SoundPlayer(winformcsharo.Properties.Resources.sucess_change);//В скобках имя файла в формате wav
                
                player.Play();
            }
            else
            {
                SoundPlayer player = new SoundPlayer(winformcsharo.Properties.Resources.press_hotkey);//В скобках имя файла в формате wav
                player.Play();
            }
                
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Win32.RegistryKey myKey =
                Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                myKey.SetValue("winformcsharo.exe", Application.ExecutablePath.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
            Microsoft.Win32.RegistryKey myKey =
            Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            myKey.DeleteValue("winformcsharo.exe");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
}
    }


}
