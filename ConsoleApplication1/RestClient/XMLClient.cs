using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Net;
using System.IO;
using System.Xml.Serialization;

namespace Citrix.Csm.RestClient
{
    
    public class XMLClient: IClientWrapper
    {
        private RestClient _client;
        private string _contentType = ContentType.URLEncoded;
        public XMLClient(string baseUri, string userName = "administrator", string password = "citrix")
        {
            _client = new RestClient(baseUri, userName, password);
        }

        public T Invoke<T>(Hashtable content, string url, string method = "POST")
        {
            string para = null;
            if (content != null && content.Count > 0)
            {
                var data = new StringBuilder();
                foreach(var key in content.Keys)
                {
                    data.Append(string.Format("{0}={1}&", key, content[key]));
                }
                para = data.ToString();
            }
            try
            {
                var request = _client.CreateRequest(para, url, method, _contentType);
                var rawResult = _client.GetWebResponse(request);
                T result;
                using (StringReader sr = new StringReader(rawResult))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(T), "http://www.ems-cortex.com/CortexDirectoryWS/Directory");
                    result = (T)xml.Deserialize(sr);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
