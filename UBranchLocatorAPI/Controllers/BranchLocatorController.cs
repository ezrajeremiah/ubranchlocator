using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UBranchLocator.Models;
using UBranchLocator.Utilities;
using System.Device.Location;

namespace UBranchLocator.Controllers
{
    public class BranchLocatorController : ApiController
    {
        [HttpGet]
        public List<BranchInfo> getBestBranch(double latitude, double longitude)
        {
            List<BranchInfo> brInfo = new List<BranchInfo>();
            string actionUrl = "https://api-uat.unionbankph.com/partners/sb/locators/v1/branches";

            HttpWebRequest request = WebRequest.Create(actionUrl) as HttpWebRequest;

            request.Method = "GET"; // no post data, act as get request.
            request.ContentLength = 0;
            request.Accept = "application/json"; 
            request.Headers.Add("x-ibm-client-id", "1fbdb694-ae20-462b-8184-99469314a081");
            request.Headers.Add("x-ibm-client-secret", "aO6uY0tQ0eD8lX2xO6qY3tU0iK6iG7vB4qY3pD3lE4iR1xH1aU");
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string responseData = string.Empty;

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseData = reader.ReadToEnd();
                    reader.Close();
                }

                response.Close();
            }


            var curLoc = new GeoCoordinate(latitude, longitude);
            int radius = 5000;
            brInfo = JsonConvert.DeserializeObject<List<BranchInfo>>(responseData);
            List<BranchInfo> newBr = new List<BranchInfo>();
            foreach(BranchInfo br in brInfo)
            {
                var brLoc = new GeoCoordinate(br.latitude, br.longitude);
                double distance = curLoc.GetDistanceTo(brLoc);
                if (distance<radius)
                {
                    br.distance = Math.Round(distance/ 1000,2);
                    br.Volume = getRand();
                    newBr.Add(br);
                }
            }

            return newBr.OrderBy(o => o.id).ToList();
        }

        [HttpGet]
        public List<BranchInfo> searchBranch(string searchValue, double latitude, double longitude)
        {
            List<BranchInfo> brInfo = new List<BranchInfo>();
            string actionUrl = "https://api-uat.unionbankph.com/partners/sb/locators/v1/branches";

            HttpWebRequest request = WebRequest.Create(actionUrl) as HttpWebRequest;

            request.Method = "GET"; // no post data, act as get request.
            request.ContentLength = 0;
            request.Accept = "application/json";
            request.Headers.Add("x-ibm-client-id", "1fbdb694-ae20-462b-8184-99469314a081");
            request.Headers.Add("x-ibm-client-secret", "aO6uY0tQ0eD8lX2xO6qY3tU0iK6iG7vB4qY3pD3lE4iR1xH1aU");
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            string responseData = string.Empty;

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseData = reader.ReadToEnd();
                    reader.Close();
                }

                response.Close();
            }

            
            brInfo = JsonConvert.DeserializeObject<List<BranchInfo>>(responseData);
            List<BranchInfo> newBr = new List<BranchInfo>();
            foreach (BranchInfo br in brInfo)
            {
                var curLoc = new GeoCoordinate(latitude, longitude);
                var brLoc = new GeoCoordinate(br.latitude, br.longitude);
                double distance = curLoc.GetDistanceTo(brLoc);
                if (br.name.ToUpper().Contains(searchValue.ToUpper()))
                {
                    br.distance = Math.Round(distance / 1000, 2);
                    br.Volume = getRand();
                    newBr.Add(br);
                }
            }

            return newBr.OrderBy(o => o.id).ToList();
        }

        Random rnd = new Random();
        private int getRand()
        {
            return rnd.Next(1, 100);
            
        }
    }
}
