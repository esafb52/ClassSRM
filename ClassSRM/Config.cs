using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace ClassSRM
{
    class Config
    {
        //Read Application Settings from Config File
        public static string ReadSetting(string key)
        {
            string result = string.Empty;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                result = appSettings[key] ?? "0";
            }
            catch (ConfigurationErrorsException)
            {
                result = "Error reading app settings";
            }
            return result;
        }

        //Add or Update Application Settings to Config File
        public static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        //Add Application to Startup
        public static void RegisterInStartup(bool isChecked)
        {
            var productName = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductName;
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey
                    ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (isChecked)
            {
                registryKey.SetValue(productName, Application.ExecutablePath);
            }
            else
            {
                registryKey.DeleteValue(productName);
            }
        }

#region "Sql Script Execute"

        //Script Path
        public static string path = AppDomain.CurrentDomain.BaseDirectory +
                         @"script.sql";

        //Read Script
        private static string GetScript()
        {
            var file = new FileInfo(path);

            string script = file.OpenText().ReadToEnd();
            return script;
        }

        //Delete Script
        public static void DelScript()
        {
            var file = new FileInfo(path);
            if (File.Exists(path))
                file.Delete();
        }

        //Execute Script to Sql Server
        public static void ExecuteScript()
        {
            if (File.Exists(path))
            {
                string script = GetScript();

                var splitter = new[] { "\r\nGO\r\n" };
                string[] commandTexts = script.Split(splitter,
                                                     StringSplitOptions.RemoveEmptyEntries);
                foreach (string commandText in commandTexts)
                {
                    using (var ctx = new ClassSRMDataContext())
                    {
                        if (!string.IsNullOrEmpty(commandText))
                        {
                            ctx.ExecuteCommand(commandText);
                        }
                    }
                }
            }
        }

        #endregion

#region "Persian Calendar Holiday"

        //Convert Georgian Date to Persian Date
        public static string PersianDate(DateTime DateTime1)
        {
            PersianCalendar PersianCalendar1 = new PersianCalendar();
            return string.Format(@"{0}/{1}/{2}",
                         PersianCalendar1.GetYear(DateTime1),
                         PersianCalendar1.GetMonth(DateTime1).ToString("00"),
                         PersianCalendar1.GetDayOfMonth(DateTime1).ToString("00"));
        }

        //Persian Calendar Holiday Date for 1396
        public static bool IsHoliday(DateTime dt)
        {
            int[] far = { 21, 22, 23 };
            int[] ord = { 1, 2, 25 }; // 4
            int[] kho = { 4, 5, 26, 27 }; //6
            int[] sha = { 9, 30 }; //9
            int[] aba = { 9, 19, 27 };//11
            int[] bah = { 11, 20 };//2

            foreach (var item in far)
            {
                if (dt.Day == item && dt.Month == 3) return true;
            }
            foreach (var item in ord)
            {
                if (dt.Day == item && dt.Month == 4) return true;
            }
            foreach (var item in kho)
            {
                if (dt.Day == item && dt.Month == 6) return true;
            }

            foreach (var item in sha)
            {
                if (dt.Day == item && dt.Month == 9) return true;
            }

            if (dt.Day == 20 && dt.Month == 3) return true;
            if (dt.Day == 20 && dt.Month == 7) return true;
            if (dt.Day == 1 && dt.Month == 10) return true;
            if (dt.Day == 6 && dt.Month == 12) return true;

            foreach (var item in aba)
            {
                if (dt.Day == item && dt.Month == 11) return true;
            }
            foreach (var item in bah)
            {
                if (dt.Day == item && dt.Month == 2) return true;
            }

            return false;
        }
#endregion

    }
}
