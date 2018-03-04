using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace viewpoint
{
    class ModuleTechSMA : ModuleTech
    {
        public ModuleTechSMA(string name) :
            base(name, new string[] { 
                "https://sas.indiatimes.com/TechnicalsClient/getSMA.htm?crossovertype=CLOSE_ABOVE_SMA_20&pagesize=25&pid=204&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=SMA&totalpages=51&col_show=20",
                "https://sas.indiatimes.com/TechnicalsClient/getSMA.htm?crossovertype=CLOSE_ABOVE_SMA_50&pagesize=25&pid=205&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=SMA&totalpages=14&col_show=50",
                "https://sas.indiatimes.com/TechnicalsClient/getSMA.htm?crossovertype=CLOSE_BELOW_SMA_20&pagesize=25&pid=206&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=SMA&totalpages=15&col_show=20",
                "https://sas.indiatimes.com/TechnicalsClient/getSMA.htm?crossovertype=CLOSE_BELOW_SMA_50&pagesize=25&pid=207&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=SMA&totalpages=52&col_show=50",
                "https://sas.indiatimes.com/TechnicalsClient/getSMA.htm?crossovertype=CROSSED_ABOVE_SMA_20&pagesize=25&pid=208&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=SMA&totalpages=51&col_show=20",
                "https://sas.indiatimes.com/TechnicalsClient/getSMA.htm?crossovertype=CROSSED_ABOVE_SMA_50&pagesize=25&pid=209&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=SMA&totalpages=2&col_show=50",
                "https://sas.indiatimes.com/TechnicalsClient/getSMA.htm?crossovertype=CROSSED_BELOW_SMA_20&pagesize=25&pid=210&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=SMA&totalpages=1&col_show=20",
                "https://sas.indiatimes.com/TechnicalsClient/getSMA.htm?crossovertype=CROSSED_BELOW_SMA_50&pagesize=25&pid=211&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=SMA&totalpages=6&col_show=50",
                "https://sas.indiatimes.com/TechnicalsClient/getSMA.htm?crossovertype=SMA_50_ABOVE_SMA_20&pagesize=25&pid=202&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=SMA&totalpages=4&col_show=both",
                "https://sas.indiatimes.com/TechnicalsClient/getSMA.htm?crossovertype=SMA_20_ABOVE_SMA_50&pagesize=25&pid=203&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=SMA&totalpages=3&col_show=both"
        }, new string[] { 
                "Close Above SMA 20",
                "Close Above SMA 50",
                "Close Below SMA 20",
                "Close Below SMA 50",
                "Crossed Above SMA 20",
                "Crossed Above SMA 50",
                "Crossed Below SMA 20",
                "Crossed Below SMA 50",
                "SMA 50 Above SMA 20",
                "SMA 20 Above SMA 50"
        })
        {
        }
    }
}
