using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace dashboard
{
    class ModuleTechBollingerBand : ModuleTech
    {
        public ModuleTechBollingerBand(string name)
            : base(name, new string[] { 
                "https://sas.indiatimes.com/TechnicalsClient/getBollingerBand.htm?crossovertype=CLOSE_ABOVE_UPPER_BOLLINGER_BAND&pagesize=25&pid=240&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=BOLLINGER&totalpages=1&col_show=upper",
                "https://sas.indiatimes.com/TechnicalsClient/getBollingerBand.htm?crossovertype=CLOSE_CROSSED_ABOVE_UPPER_BOLLINGER_BAND&pagesize=25&pid=241&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=BOLLINGER&totalpages=1&col_show=upper",
                "https://sas.indiatimes.com/TechnicalsClient/getBollingerBand.htm?crossovertype=CLOSE_CROSSED_ABOVE_LOWER_BOLLINGER_BAND&pagesize=25&pid=242&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=BOLLINGER&totalpages=1&col_show=lower",
                "https://sas.indiatimes.com/TechnicalsClient/getBollingerBand.htm?crossovertype=CLOSE_BELOW_LOWER_BOLLINGER_BAND&pagesize=25&pid=243&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=BOLLINGER&totalpages=1&col_show=lower",
                "https://sas.indiatimes.com/TechnicalsClient/getBollingerBand.htm?crossovertype=CLOSE_CROSSED_BELOW_UPPER_BOLLINGER_BAND&pagesize=25&pid=244&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=BOLLINGER&totalpages=2&col_show=upper",
                "https://sas.indiatimes.com/TechnicalsClient/getBollingerBand.htm?crossovertype=CLOSE_CROSSED_BELOW_LOWER_BOLLINGER_BAND&pagesize=25&pid=245&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=BOLLINGER&totalpages=1&col_show=lower"
        },
        new string[]  { 
              "Above Upper BB",
              "Crossed Above Upper BB",
              "Crossed Above Lower BB",
              "Below Lower BB",
              "Crossed Below Upper BB",
              "Crossed Below Lower BB"
        })
        {
        }
    }
}
