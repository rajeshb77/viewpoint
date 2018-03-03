using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace dashboard
{
    class ModuleTechRSI : ModuleTech
    {
        public ModuleTechRSI(string name)
            : base(name, new string[] { 
            "https://sas.indiatimes.com/TechnicalsClient/getRSI.htm?crossovertype=RSI_BELOW_20&pagesize=25&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=RSI&totalpages=1&col_show=20",
            "https://sas.indiatimes.com/TechnicalsClient/getRSI.htm?crossovertype=RSI_BETWEEN_20_AND_30&pagesize=25&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=RSI&totalpages=1&col_show=both",
            "https://sas.indiatimes.com/TechnicalsClient/getRSI.htm?crossovertype=RSI_BETWEEN_30_AND_70&pagesize=25&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=RSI&totalpages=1&col_show=both",
            "https://sas.indiatimes.com/TechnicalsClient/getRSI.htm?crossovertype=RSI_BETWEEN_70_AND_80&pagesize=25&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=RSI&totalpages=1&col_show=both",
            "https://sas.indiatimes.com/TechnicalsClient/getRSI.htm?crossovertype=RSI_ABOVE_80&pagesize=25&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=RSI&totalpages=1&col_show=80",
            "https://sas.indiatimes.com/TechnicalsClient/getRSI.htm?crossovertype=RSI_CROSSED_BELOW_20&pagesize=25&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=RSI&totalpages=1&col_show=20",
            "https://sas.indiatimes.com/TechnicalsClient/getRSI.htm?crossovertype=RSI_CROSSED_ABOVE_20&pagesize=25&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=RSI&totalpages=1&col_show=20",
            "https://sas.indiatimes.com/TechnicalsClient/getRSI.htm?crossovertype=RSI_CROSSED_BELOW_30&pagesize=25&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=RSI&totalpages=1&col_show=30",
            "https://sas.indiatimes.com/TechnicalsClient/getRSI.htm?crossovertype=RSI_CROSSED_ABOVE_30&pagesize=25&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=RSI&totalpages=1&col_show=30",
            "https://sas.indiatimes.com/TechnicalsClient/getRSI.htm?crossovertype=RSI_CROSSED_BELOW_70&pagesize=25&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=RSI&totalpages=1&col_show=70",
            "https://sas.indiatimes.com/TechnicalsClient/getRSI.htm?crossovertype=RSI_CROSSED_BELOW_70&pagesize=25&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=RSI&totalpages=1&col_show=70",
            "https://sas.indiatimes.com/TechnicalsClient/getRSI.htm?crossovertype=RSI_CROSSED_ABOVE_70&pagesize=25&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=RSI&totalpages=1&col_show=70",
            "https://sas.indiatimes.com/TechnicalsClient/getRSI.htm?crossovertype=RSI_CROSSED_BELOW_80&pagesize=25&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=RSI&totalpages=1&col_show=80",
            "https://sas.indiatimes.com/TechnicalsClient/getRSI.htm?crossovertype=RSI_CROSSED_ABOVE_80&pagesize=25&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=RSI&totalpages=1&col_show=80"        
        },
        new string[] { 
                "Below 20",
                "Between 20 & 30",
                "Between 30 & 70",
                "Between 70 & 80",
                "Above 80",
                "Crossed Below 20",
                "Crossed Above 20",
                "Crossed Below 30",
                "Crossed Above 30",
                "Crossed Below 70",
                "Crossed Below 70",
                "Crossed Above 70",
                "Crossed Below 80",
                "Crossed Above 80"
        })
        {
        }
    }
}
