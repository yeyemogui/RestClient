using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using MSMQTest;
using Citrix.Csm.RestClient;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.IO;
using System.DirectoryServices;
using System.Configuration;


namespace ConsoleApplication1
{

    public enum DirectoryType
    {
        PublicFolder = 0,
        User = 1,
        Group = 2,
        Contact = 3,
        Other = 4,
    }

    [Serializable]
    public class ADResultItem
    {
        public ADResultItem(){}
        public ADResultItem(System.DirectoryServices.DirectoryEntry de){}
        public ADResultItem(System.DirectoryServices.SearchResult sr){}

        public string DisplayName { get; set; }
        public string Domain { get; set; }
        public string Email { get; set; }
        public string FullAccountName { get; set; }
        public string Path { get; set; }
        public string sAmAccountName { get; set; }
        public DirectoryType Type { get; set; }

        protected virtual void Load(object obj)
        { }
    }

    [System.CodeDom.Compiler.GeneratedCode("System.Xml", "4.0.30319.233")]
    [System.Serializable()]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(Namespace = "http://www.ems-cortex.com/CortexDirectoryWS/Directory")]
    public partial class AdGroup
    {
        private string displayNameField;
        private string sAmAccountNameField;
        private string customerKeyField;
        private string customerKeyAttributesField;
        private string customerAdPathField;
        private string groupOuNameField;
        private string groupTypeField;
        private string pathField;
        private ADResultItem[] membersField { get; set; }

        public string DisplayName
        {
            get { return displayNameField; }
            set { displayNameField = value; }
        }

        public string sAmAccountName
        {
            get { return sAmAccountNameField; }
            set { sAmAccountNameField = value; }
        }

        public string CustomerKey
        {
            get { return customerKeyField; }
            set { customerKeyField = value; }
        }

        public string CustomerKeyAttributes
        {
            get { return customerKeyAttributesField; }
            set { customerKeyAttributesField = value; }
        }

        public string CustomerAdPath
        {
            get { return customerAdPathField; }
            set { customerAdPathField = value; }
        }

        public string GroupOuName
        {
            get { return groupOuNameField; }
            set { groupOuNameField = value; }
        }

        public string GroupType
        {
            get { return groupTypeField; }
            set { groupTypeField = value; }
        }

        public string Path
        {
            get { return pathField; }
            set { pathField = value; }
        }

        public ADResultItem[] Members
        {
            get { return membersField; }
            set { membersField = value; }
        }
    }
    public class Program
    {
        public struct ttt
        {
            public string a;
            public int b;
        };

       

        static void Main(string[] args)
        {
            /*
            var _initialSessionState = InitialSessionState.CreateDefault();
            PSSnapInException snapEx;
            _initialSessionState.ImportPSSnapIn("Microsoft.Exchange.Management.PowerShell.E2010", out snapEx);
            var space1 = RunspaceFactory.CreateRunspace(_initialSessionState);
           // var space2 = RunspaceFactory.CreateRunspace(_initialSessionState);

            space1.Open();
           // space2.Open();

            var _runspaceInvoke = new RunspaceInvoke(space1);
            IList errors;
            ICollection<PSObject> output = _runspaceInvoke.Invoke(@"c:\debug\GetMoveRequest.ps1", null, out errors); */
            /*
            var p1 = space1.CreatePipeline();
            var p2 = space2.CreatePipeline();

            p1.Commands.AddScript(@"$pass = ConvertTo-SecureString -string 'citrix#123' -AsPlainText -Force;");
            p1.Commands.AddScript(@" $cred = New-Object System.Management.Automation.PSCredential('admin@citrixd1.onmicrosoft.com', $pass);");
            p1.Commands.AddScript(@"$s = New-PSSession -ConfigurationName Microsoft.Exchange -ConnectionUri 'https://outlook.office365.com/powershell-liveid' -Credential $cred -Authentication basic -AllowRedirection;");
            p1.Commands.AddScript(@"Export-PSSession -Session $s -OutputModule hhh -AllowClobber -Force");
            p1.Commands.AddScript(@"Remove-PSSession -Session $s");
            p1.Invoke();*/

//p2.Commands.AddScript(@"Import-Module hhh");
//p2.Commands.AddScript(@"Get-Mailbox");

//p2.Invoke();
            /*
            var queue = new MSMQT(@"FormatName:DIRECT=http://10.108.36.192/msmq/private$/test");
            var queue2 = new MSMQT(@"FormatName:DIRECT=http://10.108.36.192/msmq/private$/test");
            queue2.SendeMessage("HelloWorld");

            queue.ReceiveMessage();*/

            Console.WriteLine("input key to contineu: ");
            Console.ReadKey();
            /////////////rest client test

            


            ttt x = new ttt{a = "jjhj", b = 1};

            //var ll = new List<object>();
            var ad = new AdGroup();
            ad.sAmAccountName = "hello";
            ad.DisplayName = "world";
            ad.Members = new ADResultItem[1];

            //ll.Add(ad);

            //using(var sw = new StringWriter())
            //{
            //    foreach (var i in ll)
            //    {
            //        XmlSerializer ss = new XmlSerializer(i.GetType());
            //        ss.Serialize(sw, i);
            //        var xx = sw.ToString();
            //    }
            //}

            Console.WriteLine(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
            foreach(var item in ConfigurationManager.AppSettings.Keys)
            {
                
                Console.WriteLine(item.ToString());
                Console.WriteLine(ConfigurationManager.AppSettings[item.ToString()].ToString());
            }
            
            Hashtable parm = new Hashtable() 
            {
                {"LoginName", "cspadmin@csp.local"},
                {"Password", "citrix#123"}
            };
            try
            {
                var client = RestClientContext.GetJSONClient(@"http://mwnrv-csm:8095/Directory/Directory.asmx/", @"CPSMW.local\administrator", "citrix");
                var result = client.Invoke<string>(null, "getDomainComputers");


                //DataSet ttttt = result["d"];
                //var yyy = string.Join("",result["d"]);

                //using (StringReader sr = new StringReader(ttttt))
                //{
                //    XmlSerializer xml = new XmlSerializer(typeof(DataSet));
                //    var rr = (DataSet)xml.Deserialize(sr);

                //    var tt = rr.Tables[0];
                //    var ooo = tt.Rows;
                //    foreach(DataRow row in tt.Rows)
                //    {
                //        var name = (string)row["LdapPath"];
                //    }
                //}
                //var xxx = (Hashtable)result;
                //var y = xxx["d"].ToString();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            Console.WriteLine("input key to exist: ");
            Console.ReadKey();
            //space1.Close();
            //space2.Close();
 
      
        }
    }
}
