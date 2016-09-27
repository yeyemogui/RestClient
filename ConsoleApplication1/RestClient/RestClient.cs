using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Web;
using Newtonsoft.Json;

namespace Citrix.Csm.RestClient
{
    public class RestClient 
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="baseUrl"></param>
        public RestClient(string baseUri, string userName = "administrator", string password = "citrix")
        {
            _baseUri = baseUri;
            _userName = userName;
            _password = password;
        }

        /// <summary>
        /// base url
        /// </summary>
        private readonly string _baseUri;
        private readonly string _userName;
        private readonly string _password;


        /// <summary>
        /// Create a Http Request
        /// </summary>
        /// <param name="json"></param>
        /// <param name="uri"></param>
        /// <returns>HttpWebRequest</returns>
        public HttpWebRequest CreateRequest(string para, string uri = null, string method = "POST", string contentType = "application/json")     //CreateRequest
        {
            //Http url
            string serviceUrl = string.Format("{0}/{1}", this._baseUri, uri);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);

            // Authentication Setting
            var credCache = new CredentialCache();
            var netcreds = new NetworkCredential(_userName, _password);
            var uriObj = new Uri(serviceUrl);
            credCache.Add(uriObj, "Basic", netcreds);
            myRequest.Credentials = credCache;
            myRequest.Timeout = 300000; // 5min


            myRequest.Method = method;
            myRequest.ContentType = contentType;
            if (!string.IsNullOrEmpty(para))
            {
                byte[] buf = UnicodeEncoding.UTF8.GetBytes(para);
                myRequest.ContentLength = buf.Length;
                Stream newStream = myRequest.GetRequestStream();
                newStream.Write(buf, 0, buf.Length);
                newStream.Close();
            }
            else
            {
                myRequest.ContentLength = 0;
            }

            return myRequest;
        }


        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //Always accept
        }


        /// <summary>
        /// Get HttpWebRequest's Response
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public string GetWebResponse(HttpWebRequest request, object content = null)
        {
            HttpWebResponse response;
            
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                using (WebResponse myResponse = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)myResponse;
                    if (httpResponse == null || httpResponse.StatusCode == HttpStatusCode.ServiceUnavailable)
                    {
                        throw new Exception("Connection Lost");
                    }
                    using (Stream data = myResponse.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        string text = reader.ReadToEnd();

                        throw new Exception(text);

                    }
                }
            }

            //Do more operations

            if (response.StatusCode == HttpStatusCode.ResetContent)
            {
                // Create a whole new request, but this time the sessionID will have been set.

                //var s = request.RequestUri.OriginalString.Clone().ToString();
                //var method = request.Method.Clone().ToString();
                //request.Abort();
                //var request2 = CreateRequest(s, method, content);
                //response = (HttpWebResponse)request2.GetResponse();
            }
            string retValue = GetReponseContent(response);
            response.Close();
            return retValue;
        }

        /// <summary>
        /// Get the Response Content or return the empty string.
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static string GetReponseContent(HttpWebResponse response)
        {
            if (response == null) throw new ArgumentNullException("response");

            using (var stream = response.GetResponseStream())
            {
                if (stream != null)
                {
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        var content = reader.ReadToEnd();
                        return content;
                    }
                }
            }
            return string.Empty;
        }

        public void PingCitrixWebService()
        {
            try
            {
                //var request = CreateRequest(null, string.Format(@"{0}/{1}", _baseUri, ServiceDefinitions.EchoAccepLanguageEntity),
                //                            "GET");
                //var output = GetWebResponse(request);
                //JavaScriptSerializer serializer = new JavaScriptSerializer();

            }
            catch (Exception ex)
            {

            }
        }
    }
}

