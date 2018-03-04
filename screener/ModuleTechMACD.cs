using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace viewpoint
{
    class ModuleTechMACD : ModuleTech
    {
        public ModuleTechMACD(string name)
            : base(name, new string[] { 
            "https://sas.indiatimes.com/TechnicalsClient/getMACD.htm?crossovertype=MACD_CROSSED_ABOVE_SIGNAL&pagesize=25&pid=237&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=MACD&totalpages=1",
            "https://sas.indiatimes.com/TechnicalsClient/getMACD.htm?crossovertype=MACD_CROSSED_BELOW_SIGNAL&pagesize=25&pid=238&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=MACD&totalpages=2"
        },
        new string[] { 
                "Crossed Below Signal", 
                "Crossed Above Signal"
        })
        {
        }
    }
}
