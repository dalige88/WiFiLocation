﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WiFiLocationServer.Models
{
    public class data_test
    {
        public int id { set; get; }
        public string coord { set; get; }
        public string memory { set; get; }
        public string addtime { set; get; }
        public int rssi { set; get; }
        public int flag { set; get; }
        public string actual_coord { set; get; }

    }
}