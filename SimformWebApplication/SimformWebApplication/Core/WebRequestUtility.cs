using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace SimformWebApplication.Core
{
    public class WebRequestUtility
    {
        /// <summary>
        /// Process the web response.
        /// </summary>
        /// <param name="webRequest">The request object.</param>
        /// <returns>The response data.</returns>
        public string WebResponseGet(HttpWebRequest webRequest)
        {
            StreamReader responseReader = null;
            string responseData = "";

            try
            {
                responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                responseData = responseReader.ReadToEnd();
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            string error = reader.ReadToEnd();
                            throw new WebException(error);
                        }
                    }
                }

                throw;
            }
            finally
            {
                webRequest.GetResponse().GetResponseStream().Close();
                responseReader.Close();
                responseReader = null;
            }

            return responseData;
        }

        /// <summary>
        /// Web Request Wrapper
        /// </summary>
        /// <param name="method">Http Method</param>
        /// <param name="url">Full url to the web resource</param>
        /// <param name="postData">Data to post in querystring format</param>
        /// <param name="headers">Additional Header Data</param>
        /// <returns>The web server response.</returns>
        public string WebRequest(Enums.Method method, string url, string postData, List<KeyValuePair<string, string>> headers, string contentType)
        {
            HttpWebRequest webRequest = null;
            StreamWriter requestWriter = null;
            string responseData = "";

            webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                {
                    webRequest.Headers.Add(header.Key, header.Value);
                }
            }

            webRequest.Method = method.ToString();
            webRequest.ServicePoint.Expect100Continue = false;

            if (method == Enums.Method.POST || method == Enums.Method.DELETE)
            {
                webRequest.ContentType = contentType;
                //POST the data.
                using (requestWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    requestWriter.Write(postData);
                    requestWriter.Close();
                }
            }

            responseData = WebResponseGet(webRequest);
            webRequest = null;

            return responseData;

        }
    }
}