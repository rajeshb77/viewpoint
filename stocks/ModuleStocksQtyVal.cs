using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace dashboard
{
    class ModuleStocksQtyVal : Module
    {
        public class valvolItem
        {
            public string time;
            public IList<valvolData> data;
        }

        public class valvolData
        {
            public string symbol;
            public string series;
            public string openPrice;
            public string highPrice;
            public string lowPrice;
            public string ltp;
            public string previousPrice;
            public string netPrice;
            public string tradedQuantity;
            public string turnoverInLakhs;
            public string lastCorpAnnouncementDate;
            public string lastCorpAnnouncement;
        }

        public string[] urls = { 
            "https://www.nseindia.com/live_market/dynaContent/live_analysis/most_active/allTopVolume1.json",
            "https://www.nseindia.com/live_market/dynaContent/live_analysis/most_active/allTopValue1.json"
        };

        public string[] categoriesNames = { "Most Active (Qty)", "Most Active (Value)" };

        public ModuleStocksQtyVal(string name)
            : base(name, true)
        {
            maxPages = categoriesNames.Length;
        }

        public override void ShowPage(int pageid)
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
            Console.Write(" " + this.Title);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" >> " + categoriesNames[pageid]);
            Console.ResetColor();
            Console.WriteLine("------------------------------------------------------------------------------------------");

            var json = HttpGet(urls[pageid]);

            if (string.IsNullOrEmpty(json))
            {
                return;
            }

            valvolItem valvolItem = JsonConvert.DeserializeObject<valvolItem>(json);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  {0,9} {1,12} {2,9} {3,15} {4,15}",
                "chg %", "symbol", "ltp", "vol", "val");
            Console.ResetColor();

            foreach (var s in valvolItem.data)
            {
                Console.ForegroundColor = (float.Parse(s.netPrice) > 0 ? ConsoleColor.Green : ConsoleColor.Red);
                Console.Write("{0,9} %", ((float.Parse(s.netPrice) >= 0) ? " +" : " ") + s.netPrice.Trim());
                Console.ResetColor();

                Console.WriteLine(" {0,12} {1,9} {2,15} {3,15}", s.symbol, s.ltp,
                    s.tradedQuantity, s.turnoverInLakhs);
            }

            Console.WriteLine("------------------------------------------------------------------------------------------");

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
