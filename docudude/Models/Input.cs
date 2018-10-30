using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace docudude.Models
{
    public class Input
    {
        public string SourceBucket { get; set; }
        public string SourceFile { get; set; }
        public List<InputStep> Steps { get; set; }
        public bool DoSign { get; set; }
    }
}
