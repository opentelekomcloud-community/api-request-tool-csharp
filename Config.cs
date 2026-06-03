using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace request_tool
{
    public class Config
    {
        public bool sslVerify { get; set; } = true;
        public double timeOutSec { get; set; } = 5;
        // public string key { get; set; } = Environment.GetEnvironmentVariable("OTC_SDK_AK");
        public bool proxyEnable { get; set; } = false;
        public string proxyUrl { get; set; } = "";
        public string proxyUser { get; set; } = "";
        public ObservableCollection<HisRequest> history { get; set; } = new ObservableCollection<HisRequest>();
    }
    public class Header
    {
        public String Key { get; set; }
        public String Value { get; set; }
    }
    public class HisRequest
    {
        public string Method { get; set; }
        public string Url { get; set; }
        public List<Header> Headers { get; set; }
        public string Body { get; set; }
    }
}
