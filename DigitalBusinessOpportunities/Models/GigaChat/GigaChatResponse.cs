using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalBusinessOpportunities.Models.GigaChat
{
    public class GigaChatResponse
    {

        public Choice[] choices { get; set; }
        public int created { get; set; }
        public string model { get; set; }
        public Usage usage { get; set; }
        public string _object { get; set; }

    }

    public class Usage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int precached_prompt_tokens { get; set; }
        public int total_tokens { get; set; }
    }

    public class Choice
    {
        public Message message { get; set; }
        public int index { get; set; }
        public string finish_reason { get; set; }
    }

    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
       
    }

    
}
