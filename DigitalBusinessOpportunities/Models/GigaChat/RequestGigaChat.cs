using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBusinessOpportunities.Models.GigaChat
{
    public class RequestGigaChat
    {

        public string model { get; set; }
        public Message[] messages { get; set; }
        public int n { get; set; }
        public bool stream { get; set; }
        public int max_tokens { get; set; }
        public int repetition_penalty { get; set; }
        public int update_interval { get; set; }


    }
    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }
}
