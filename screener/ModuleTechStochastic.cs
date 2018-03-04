using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace viewpoint
{
    class ModuleTechStochastic : ModuleTech
    {
        public ModuleTechStochastic(string name)
            : base(name, new string[] { 
              "https://sas.indiatimes.com/TechnicalsClient/getStochastic.htm?crossovertype=STOCHASTIC_OVER_BOUGHT&pagesize=25&pid=248&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=STOCHASTIC&totalpages=1&col_show=fast",
              "https://sas.indiatimes.com/TechnicalsClient/getStochastic.htm?crossovertype=STOCHASTIC_OVER_SOLD&pagesize=25&pid=249&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=STOCHASTIC&totalpages=5&col_show=fast",
              "https://sas.indiatimes.com/TechnicalsClient/getStochastic.htm?crossovertype=STOCHASTIC_BULLISH_CROSSOVER&pagesize=25&pid=250&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=STOCHASTIC&totalpages=9&col_show=both",
              "https://sas.indiatimes.com/TechnicalsClient/getStochastic.htm?crossovertype=STOCHASTIC_BEARISH_CROSSOVER&pagesize=25&pid=251&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=STOCHASTIC&totalpages=3&col_show=both",
        },
        new string[] { 
              "Fast Stochastic Above 80",
              "Fast Stochastic Below 20",
              "Fast Stochastic Crossed Above Slow Stochastic",
              "Fast Stochastic Crossed Below Slow Stochastic"
        })
        {
        }
    }
}
