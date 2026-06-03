using System;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace request_tool
{
    public partial class App : Application
    {
        public static Config config = new Config();
        string configFile = "config";
        XmlSerializer xs = new XmlSerializer(typeof(Config));

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Application.Current.StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
            try
            {
                FileStream fs = new FileStream(configFile, FileMode.Open);
                config = (Config)xs.Deserialize(fs);
                fs.Close();
            }
            catch { }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            FileStream fs = new FileStream(configFile, FileMode.Create);
            xs.Serialize(fs, config);
            fs.Close();
        }
    }
}
