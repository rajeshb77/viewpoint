using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace viewpoint
{
    class ModuleTech : Module
    {
        public class tmItem
        {
            public IList<tmData> searchResult;
            public tmSummary pageSummary;
        }

        public class tmData
        {
            public string absoluteChange;
            public string percentageChange;
            public string previousClose;
            public string seoName;
            public string companyShortName;
            public string scripCode;
            public string currentPrice;
            public string currentMacdSignal;
            public string companyName;
            public string currentMacd;
            public string volume;
            public string updatedDateTime;
            public string companyId;
        }

        public class tmSummary
        {
            public int pageNo;
            public int pageSize;
            public int totalRecords;
            public int totalPages;
            public string lasttradeddate;
        }

        public string[] urls = { 
                "https://sas.indiatimes.com/TechnicalsClient/getBollingerBand.htm?crossovertype=CLOSE_ABOVE_UPPER_BOLLINGER_BAND&pagesize=25&pid=240&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=BOLLINGER&totalpages=1&col_show=upper",
                "https://sas.indiatimes.com/TechnicalsClient/getBollingerBand.htm?crossovertype=CLOSE_CROSSED_ABOVE_UPPER_BOLLINGER_BAND&pagesize=25&pid=241&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=BOLLINGER&totalpages=1&col_show=upper",
                "https://sas.indiatimes.com/TechnicalsClient/getBollingerBand.htm?crossovertype=CLOSE_CROSSED_ABOVE_LOWER_BOLLINGER_BAND&pagesize=25&pid=242&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=BOLLINGER&totalpages=1&col_show=lower",
                "https://sas.indiatimes.com/TechnicalsClient/getBollingerBand.htm?crossovertype=CLOSE_BELOW_LOWER_BOLLINGER_BAND&pagesize=25&pid=243&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=BOLLINGER&totalpages=1&col_show=lower",
                "https://sas.indiatimes.com/TechnicalsClient/getBollingerBand.htm?crossovertype=CLOSE_CROSSED_BELOW_UPPER_BOLLINGER_BAND&pagesize=25&pid=244&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=BOLLINGER&totalpages=2&col_show=upper",
                "https://sas.indiatimes.com/TechnicalsClient/getBollingerBand.htm?crossovertype=CLOSE_CROSSED_BELOW_LOWER_BOLLINGER_BAND&pagesize=25&pid=245&exchange=50&pageno=1&sortby=volume&sortorder=desc&ctype=BOLLINGER&totalpages=1&col_show=lower"
        };

        public string[] categoriesNames = { 
              "Above Upper BB",
              "Crossed Above Upper BB",
              "Crossed Above Lower BB",
              "Below Lower BB",
              "Crossed Below Upper BB",
              "Crossed Below Lower BB"
        };

        public ModuleTech(string name, string[] endpoints, string[] categories)
            : base(name, true)
        {
            urls = endpoints;
            categoriesNames = categories;
            maxPages = categoriesNames.Length;
        }

        public override void ProcessFunctionKeyEx(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.F1:
                    --activeSubPageId;
                    if (activeSubPageId < 1)
                    {
                        activeSubPageId = maxSubPages;
                    }
                    ShowSubPage(activePageId, activeSubPageId);
                    break;
                case ConsoleKey.F2:
                    ++activeSubPageId;

                    if (activeSubPageId > maxSubPages)
                    {
                        activeSubPageId = 1;
                    }

                    ShowSubPage(activePageId, activeSubPageId);
                    break;
                case ConsoleKey.F3:
                case ConsoleKey.F4:
                case ConsoleKey.F5:
                case ConsoleKey.F6:
                case ConsoleKey.F7:
                    break;
            }
        }

        public override void ShowPage(int pageid)
        {
            ShowSubPage(pageid, 1);
        }

        public void ShowSubPage(int pageid, int subPageid)
        {
            this.activePageId = pageid;
            Console.CursorVisible = false;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(69, 1);
            Console.WriteLine(System.DateTime.Now);
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);

            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" " + this.Title.Substring(7, this.Title.Length - 7));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" >> " + categoriesNames[pageid]);
            Console.ResetColor();
            Console.WriteLine("------------------------------------------------------------------------------------------");

            var json = HttpGet(urls[pageid].Replace("pageno=1", "pageno=" + subPageid));

            if (string.IsNullOrEmpty(json))
            {
                return;
            }

            tmItem macdItem = JsonConvert.DeserializeObject<tmItem>(json);

            activeSubPageId = macdItem.pageSummary.pageNo;
            maxSubPages = macdItem.pageSummary.totalPages;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" {0,9}  {1,9} {2,12} {3,9} {4,15} {5,15}",
                "chg", "chg %", "symbol", "ltp", "prev", "vol");
            Console.ResetColor();

            foreach (var s in macdItem.searchResult)
            {
                Console.Write(" {0,9}", s.absoluteChange, "*");
                Console.ForegroundColor = (float.Parse(s.absoluteChange) > 0 ? ConsoleColor.Green : ConsoleColor.Red);
                Console.Write("{0,9} %", ((float.Parse(s.absoluteChange) >= 0) ? " +" : " ") + float.Parse(s.percentageChange).ToString("n2"));
                Console.ResetColor();

                string sym = s.scripCode.Substring(0, s.scripCode.Length - 2);
                Console.ForegroundColor = Utility.findMySymbol(sym) ? ConsoleColor.White : ConsoleColor.DarkGray;

                Console.WriteLine(" {0,12} {1,9} {2,15} {3,15}",
                    sym,
                    float.Parse(s.currentPrice).ToString("n2"),
                    float.Parse(s.previousClose).ToString("n2"),
                    s.volume);

                Console.ResetColor();
            }

            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            int firstPage = ((activeSubPageId * 25) - 24);
            int lastPage = ((activeSubPageId * 25) > macdItem.pageSummary.totalRecords) ? macdItem.pageSummary.totalRecords : (activeSubPageId * 25);
            Console.Write(" {0} - {1} of {2}", firstPage, lastPage, macdItem.pageSummary.totalRecords);
            Console.ResetColor();
            Console.SetCursorPosition(57, Console.CursorTop);
            Console.WriteLine("   << Prev (F1)  |  Next (F2) >>");

            ReadInput();
        }

        public override void ShowMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(69, 1);
            Console.WriteLine(System.DateTime.Now);
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);

            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine(" " + this.Title);
            Console.WriteLine("------------------------------------------------------------------------------------------");

            int ind = 1;
            foreach (string category in categoriesNames)
            {
                Console.Write(" {0,2}. {1,-30}", ind, category);
                if (ind % 1 == 0)
                    Console.WriteLine();
                else
                {
                    Console.Write("|");
                }
                ind++;
            }
            Console.WriteLine("------------------------------------------------------------------------------------------");

            ReadInput();
        }
    }
}
