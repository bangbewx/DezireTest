using System;
using System.Collections.Generic;

namespace DezireDhimasTestApi.Model
{

    public class MQueThree
    {
        public int id { get; set; }
        public int category { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string footer { get; set; }
        public string createdAt { get; set; }
    }

    public class ThreeResp
    {
        public int id { get; set; }
        public int category { get; set; }
        public List<Item> items { get; set; }
        public string[] tags { get; set; }
        public string createdAt { get; set; }
    }

    public class InputThree
    {
        public int id { get; set; }
        public int category { get; set; }
        public List<Item> items { get; set; }
        public string createdAt { get; set; }
    }

    public class Item
    {
        public string title { get; set; }
        public string description { get; set; }
        public string footer { get; set; }
    }

}
