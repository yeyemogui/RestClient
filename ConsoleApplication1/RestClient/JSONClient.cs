using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Newtonsoft.Json;

namespace Citrix.Csm.RestClient
{
    public class AjaxWrapper<T>
    {
        public T d;
    }
    public class JSONClient: IClientWrapper
    {
        private RestClient _client;
        private string _contentType = ContentType.JSON;
        public JSONClient(string baseUri, string userName = "administrator", string password = "citrix")
        {
            _client = new RestClient(baseUri, userName, password);
        }

        public T Invoke<T>(Hashtable content, string url, string method = "POST")
        {
            string para = null;
            if (content != null && content.Count > 0)
            {
                para = JsonConvert.SerializeObject(content);
            }
            try
            {
                var request = _client.CreateRequest(para, url, method, _contentType);
                var rawResult = _client.GetWebResponse(request);
                var result = JsonConvert.DeserializeObject<AjaxWrapper<T>>(rawResult).d;
                //return (T)result["d"];
                return (T)result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
