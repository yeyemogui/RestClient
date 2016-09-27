using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace MSMQTest
{
    public class MSMQT
    {
        private string _queuePath { get; set; }

        public MSMQT(string path)
        {
            _queuePath = path;
        }

        public void SendeMessage(string message)
        {
            Message data = new Message();
            data.Label = "Test";
            data.Body = message;
            data.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            var queue = GetMsgQueue();
            queue.Send(data);
        }

        public void ReceiveMessage()
        {
            var queue = GetMsgQueue();
            queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            while(true)
            {
                Message message = queue.Receive();
                Console.WriteLine("Received Message {0}", message.Body);
            }
        }

        private MessageQueue GetMsgQueue()
        {
            
           return new MessageQueue(_queuePath);

        }
    }
}
