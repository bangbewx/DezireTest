using System;

namespace DezireDhimasTestApi.Model
{

    public class MQueTwo
    {
        public int id { get; set; }
        public int category { get; set; }
        public string description { get; set; }
        public string footer { get; set; }
        public string createdAt { get; set; }
    }

    public class TwoResp
    {
        public int id { get; set; }
        public int category { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string footer { get; set; }
        public string[] tags { get; set; }
        public string createdAt { get; set; }
    }
}
