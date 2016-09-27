using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Citrix.Csm.RestClient
{
    public interface IClientWrapper
    {
        T Invoke<T>(Hashtable content, string url, string method = "POST");
    }
}
