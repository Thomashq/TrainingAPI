using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models
{
    public class GptInputModel
    {
        public GptInputModel(string prompt) {
            this.prompt = $"Me dê um fato sobre: {prompt}";
            temperature = 0.2m;
            max_tokens = 1000;
            model = "text-davinci-003";
        }
        public string model { get; set; }

        public string prompt { get; set; }

        public int max_tokens { get; set; }

        public decimal temperature { get; set; }
    }
}
