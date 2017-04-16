using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using System.IO;
using System.Net;
using System.Reflection;
namespace International_Ryze_Addon
{
    class Program
    {
        private static string addonName = "International Ryze";
        private static string dllPath = @"C:\Users\" + Environment.UserName + @"\AppData\Roaming\EloBuddy\Addons\Libraries\" + addonName + ".dll";
        private static string dllAddress = "https://raw.githubusercontent.com/WeinerCH/EBData/master/Ryze/International Ryze.dll";
        private static string dllVersion = "https://raw.githubusercontent.com/WeinerCH/EBData/master/Ryze/version.txt";

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += eventArgs =>
            {
                WebClient web = new WebClient();
                if (!File.Exists(dllPath))
                {
                    Chat.Print("[Loader] Downloading " + addonName + "...");
                    web.DownloadFile(dllAddress, dllPath);
                    Chat.Print("[Loader] " + addonName + "was successfully downloaded!");
                }

                var version = web.DownloadString(dllVersion);
                Assembly addon = Assembly.LoadFrom(dllPath);

                if (addon.GetName().Version.ToString() != version)
                {
                    if (File.Exists(dllPath))
                    {
                        File.Delete(dllPath);
                    }

                    Chat.Print("[Loader] New version found!, Downloading " + version + "update...");
                    web.DownloadFile(dllAddress, dllPath);
                    Chat.Print("[Loader] " + addonName + "was successfully updated!");
                }

                Type myType = addon.GetType("A");

                var main = myType.GetMethod("a", BindingFlags.NonPublic | BindingFlags.Static);
                main.Invoke(null, null);
            };
        }
    }
}
