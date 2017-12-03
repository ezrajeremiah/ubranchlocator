using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace UBranchLocator.Utilities
{
    public class WebAPIRequest
    {
        public static string GetPost(string actionUrl, Dictionary<string, string> headers,string accept, string postData = "")
        {
            HttpWebRequest request = WebRequest.Create(actionUrl) as HttpWebRequest;
            request.ContentType = "application/json";

            if (postData.Length > 0)
            {
                request.Method = "POST"; // we have some post data, act as post request.

                // write post data to request stream, and dispose streamwriter afterwards.
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(postData);
                    writer.Close();
                }
            }
            else
            {
                request.Method = "GET"; // no post data, act as get request.
                request.ContentLength = 0;
                request.Accept = accept;
                foreach(KeyValuePair<string,string> head in headers)
                {
                    request.Headers.Add(head.Key,head.Value);
                }
            }

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

            return responseData;
        }
    }
}