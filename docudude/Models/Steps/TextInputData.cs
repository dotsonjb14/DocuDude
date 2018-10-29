using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace docudude.Models.Steps
{
    public class TextInputData
    {
        public int Page { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int FontSize { get; set; } = 10;
        public string Content { get; set; }
    }
}
