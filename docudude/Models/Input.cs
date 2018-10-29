using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/**
 * {
	"sourceBucket": "jd_test",
	"sourceFile": "",
	"actions": [
		{
			"type": "addText",
			"data": {
				"page": 1,
				"xpos": 1,
				"ypos": 1,
				"content": "Hello World"
			}
		}
	],
	"doSign": false
}
 **/

namespace docudude.Models
{
    public class Input
    {
        // s3 bucket and key

        // mutations

        // should sign key

        public string SourceBucket { get; set; }
        public string sourceFile { get; set; }

        public List<InputStep> Steps { get; set; }

        public bool DoSign { get; set; } // ignroe for now
    }
}
