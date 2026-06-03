using OpenTelekomCloud.API.Signing.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace request_tool
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Header> Headers { get; set; }
        Signer signer = new Signer();
        public MainWindow()
        {
            InitializeComponent();
            Headers = new ObservableCollection<Header>();
            dataGrid1.DataContext = Headers;
            textKey.Text = System.Environment.GetEnvironmentVariable("OTC_SDK_AK") ?? "";
            textSecret.Password = System.Environment.GetEnvironmentVariable("OTC_SDK_SK") ?? "";
            textUrl.Text = "https://";

            this.DataContext = App.config;
            proxyEnableChanged(null, null);
        }

        private void buttonDeleteHeader_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedIndex < Headers.Count)
            {
                Headers.RemoveAt(dataGrid1.SelectedIndex);
            }
        }

        private async void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            textResp.Text = "";
            buttonSend.IsEnabled = false;

            List<Header> headers = new List<Header>();
            foreach (var h in Headers)
            {
                headers.Add(new Header
                {
                    Key = h.Key,
                    Value = h.Value,
                });
            }
            App.config.history.Insert(0, new HisRequest
            {
                Method = comboMethod.Text,
                Url = textUrl.Text,
                Headers = headers,
                Body = textBody.Text,
            });
            if (App.config.history.Count > 30)
            {
                App.config.history.RemoveAt(App.config.history.Count - 1);
            }

            signer.Key = textKey.Text;
            signer.Secret = textSecret.Password;

            Console.WriteLine(textSecret.Password);
            HttpRequestMessage req;
            HttpClient client;
            try
            {
                var url = textUrl.Text;
                if (!url.Contains("://"))
                {
                    url = "http://" + url;
                }
                HttpRequest r = new HttpRequest(comboMethod.Text, new Uri(url));
                foreach (var h in Headers)
                {
                    if (!string.IsNullOrWhiteSpace(h.Key))
                    {
                        r.headers.Add(h.Key, h.Value);
                    }
                }
                r.body = textBody.Text;

                req = signer.SignHttp(r);
                textDebugInfo.Text = $"-----canonicalRequest-----\n{r.canonicalRequest}\n\n-----stringToSign-----\n{r.stringToSign}";

                var reqHeaders = "";
                foreach (var h in req.Headers)
                {
                    foreach (var v in h.Value)
                    {
                        reqHeaders += h.Key + ": " + v + "\n";
                    }
                }
                textReqHeaders.Text = reqHeaders;

                HttpClientHandler httpClientHandler;
                if (App.config.proxyEnable)
                {
                    var proxyUrl = App.config.proxyUrl;
                    if (!proxyUrl.Contains("://"))
                    {
                        proxyUrl = "http://" + proxyUrl;
                    }
                    var proxy = new WebProxy
                    {
                        Address = new Uri(proxyUrl),
                        BypassProxyOnLocal = false,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(userName: App.config.proxyUser, password: textProxyPassword.Password)
                    };
                    httpClientHandler = new HttpClientHandler
                    {
                        Proxy = proxy,
                    };
                    client = new HttpClient(httpClientHandler);
                }
                else
                {
                    httpClientHandler = new HttpClientHandler
                    {
                        UseProxy = false,
                    };
                }
                if (!App.config.sslVerify)
                {
                    httpClientHandler.ServerCertificateCustomValidationCallback = delegate { return true; };
                }
                client = new HttpClient(httpClientHandler);
                client.Timeout = TimeSpan.FromSeconds(App.config.timeOutSec);
            }
            catch (Exception ec)
            {
                textResp.Text = ec.Message;
                buttonSend.IsEnabled = true;
                return;
            }

            HttpResponseMessage response;
            try
            {
                response = await client.SendAsync(req);
            }
            catch (HttpRequestException ec)
            {
                textResp.Text = ec.Message + ec.InnerException.Message;
                buttonSend.IsEnabled = true;
                return;
            }
            catch (TaskCanceledException)
            {
                textResp.Text = "request timed out";
                buttonSend.IsEnabled = true;
                return;
            }
            catch (Exception ec)
            {
                textResp.Text = ec.Message;
                buttonSend.IsEnabled = true;
                return;
            }
            textResp.Text = (int)response.StatusCode + " " + response.ReasonPhrase + "\n";
            foreach (var h in response.Headers)
            {
                foreach (var v in h.Value)
                {
                    textResp.Text += h.Key + ": " + v + "\n";
                }
            }
            textResp.Text += "\n";
            string body = await response.Content.ReadAsStringAsync();
            textResp.Text += body;
            buttonSend.IsEnabled = true;
        }

        private void proxyEnableChanged(object sender, RoutedEventArgs e)
        {
            var v = Visibility.Hidden;
            if (App.config.proxyEnable)
            {
                v = Visibility.Visible;
            }
            proxyUrl.Visibility = v;
            proxyUser.Visibility = v;
            proxyPassword.Visibility = v;
        }

        private void history_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var his = App.config.history[history.SelectedIndex];
            comboMethod.Text = his.Method;
            textUrl.Text = his.Url;
            textBody.Text = his.Body;
            Headers.Clear();
            foreach(var h in his.Headers)
            {
                Headers.Add(h);
            }
        }
        private void addRow_Click(object sender, RoutedEventArgs e)
        {
            Headers.Add(new Header());
        }
    }

}
