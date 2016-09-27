using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citrix.Csm.RestClient
{
    public class RestClientContext
    {
        public static XMLClient GetXMLClient(string baseUri, string userName = "administrator", string password = "citrix")
        {
            return new XMLClient(baseUri, userName, password);
        }

        public static JSONClient GetJSONClient(string baseUri, string userName = "administrator", string password = "citrix")
        {
            return new JSONClient(baseUri, userName, password);
        }
    }
}
