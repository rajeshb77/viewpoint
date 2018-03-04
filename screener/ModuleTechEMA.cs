using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace viewpoint
{
    class ModuleTechEMA : ModuleTech
    {
        public ModuleTechEMA(string name)
            : base(name, new string[]  { 
                "https://sas.indiatimes.com/TechnicalsClient/getEMA.htm?crossovertype=CLOSE_ABOVE_EMA_20&pagesize=25&pid=215&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=EMA&totalpages=16&col_show=20",
                "https://sas.indiatimes.com/TechnicalsClient/getEMA.htm?crossovertype=CLOSE_ABOVE_EMA_50&pagesize=25&pid=216&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=EMA&totalpages=16&col_show=50",
                "https://sas.indiatimes.com/TechnicalsClient/getEMA.htm?crossovertype=CLOSE_BELOW_EMA_20&pagesize=25&pid=217&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=EMA&totalpages=16&col_show=20",
                "https://sas.indiatimes.com/TechnicalsClient/getEMA.htm?crossovertype=CLOSE_BELOW_SMA_50&pagesize=25&pid=218&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=EMA&totalpages=16&col_show=50",
                "https://sas.indiatimes.com/TechnicalsClient/getEMA.htm?crossovertype=CROSSED_ABOVE_EMA_20&pagesize=25&pid=219&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=EMA&totalpages=16&col_show=20",
                "https://sas.indiatimes.com/TechnicalsClient/getEMA.htm?crossovertype=CROSSED_ABOVE_EMA_50&pagesize=25&pid=220&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=EMA&totalpages=2&col_show=50",
                "https://sas.indiatimes.com/TechnicalsClient/getEMA.htm?crossovertype=CROSSED_BELOW_EMA_20&pagesize=25&pid=221&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=EMA&totalpages=1&col_show=20",
                "https://sas.indiatimes.com/TechnicalsClient/getEMA.htm?crossovertype=CROSSED_BELOW_EMA_50&pagesize=25&pid=222&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=EMA&totalpages=7&col_show=50",
                "https://sas.indiatimes.com/TechnicalsClient/getEMA.htm?crossovertype=EMA_50_ABOVE_EMA_20&pagesize=25&pid=213&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=EMA&totalpages=6&col_show=both",
                "https://sas.indiatimes.com/TechnicalsClient/getEMA.htm?crossovertype=EMA_20_ABOVE_EMA_50&pagesize=25&pid=214&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=EMA&totalpages=2&col_show=both"
        },
        new string[] { 
                "Close Above EMA 20",
                "Close Above EMA 50",
                "Close Below EMA 20",
                "Close Below SMA 50",
                "Crossed Above EMA 20",
                "Crossed Above EMA 50",
                "Crossed Below EMA 20",
                "Crossed Below EMA 50",
                "EMA 50 Above EMA 20",
                "EMA 20 Above EMA 50"
        })
        {
        }
    }
}
