using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace dashboard
{
    class ModuleStocks52Weeks : Module
    {
        public class n52wItem
        {
            public string time;
            public IList<n52wData> data;
        }

        public class n52wData
        {
            public string symbol;
            public string symbolDesc;
            public string value;
            public string year;
            public string ltp;
            public string value_old;
            public string dt;
            public string prev;
            public string change;
            public string pChange;
        }

        public string[] urls = { 
            "https://www.nseindia.com/products/dynaContent/equities/equities/json/online52NewHigh.json",
            "https://www.nseindia.com/products/dynaContent/equities/equities/json/online52NewLow.json"
        };

        public string[] categoriesNames = { "New 52 Weeks High", "New 52 Weeks Low" };

        public ModuleStocks52Weeks(string name)
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

            n52wItem items52 = JsonConvert.DeserializeObject<n52wItem>(json);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0,9}  {1,9} {2,12} {3,9} {4,9} {5,9} {6,11}",
                "chg", "chg %", "symbol", "ltp", "new", "old", "date");
            Console.ResetColor();

            foreach (var s in items52.data)
            {
                Console.Write("{0,9}", s.pChange.Trim());
                Console.ForegroundColor = (float.Parse(s.pChange) > 0 ? ConsoleColor.Green : ConsoleColor.Red);
                Console.Write("{0,9} %", ((float.Parse(s.pChange) >= 0) ? " +" : " ") + s.change.Trim());
                Console.ResetColor();

                Console.WriteLine(" {0,12} {1,9} {2,9} {3,9} {4,15}", s.symbol, s.ltp,
                    s.value, s.value_old, s.dt);
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
