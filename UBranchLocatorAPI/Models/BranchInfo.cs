using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UBranchLocator.Models
{
    public class BranchInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string contactNo { get; set; }
        public string email { get; set; }
        public int Volume { get; set; }
        public double distance { get; set; }
    }
}