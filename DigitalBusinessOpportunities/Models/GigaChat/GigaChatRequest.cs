using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBusinessOpportunities.Models.GigaChat
{
    internal class GigaChatRequest
    {
            public string model { get; set; }
            public List<Messages> messages { get; set; }
            public bool stream { get; set; }
            public int repetition_penalty { get; set; }
    }
    public class Messages
    {
        public string role { get; set; }
        public string content { get; set; }
    }

}
