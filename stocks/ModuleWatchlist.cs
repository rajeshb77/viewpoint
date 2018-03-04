using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace viewpoint
{
    public class ModuleWatchlist : Module
    {
        public class quItem
        {
            public IList<quData> data;
        }

        public class quData
        {
            public string extremeLossMargin;
            public string cm_ffm;
            public string bcStartDate;
            public string change;
            public string buyQuantity3;
            public string sellPrice1;
            public string buyQuantity4;
            public string sellPrice2;
            public string priceBand;
            public string buyQuantity1;
            public string deliveryQuantity;
            public string buyQuantity2;
            public string sellPrice5;
            public string quantityTraded;
            public string buyQuantity5;
            public string sellPrice3;
            public string sellPrice4;
            public string open;
            public string low52;
            public string securityVar;
            public string marketType;
            public string pricebandupper;
            public string totalTradedValue;
            public string faceValue;
            public string ndStartDate;
            public string previousClose;
            public string symbol;
            public string varMargin;
            public string lastPrice;
            public string pChange;
            public string adhocMargin;
            public string companyName;
            public string averagePrice;
            public string secDate;
            public string series;
            public string isinCode;
            public string surv_indicator;
            public string indexVar;
            public string pricebandlower;
            public string totalBuyQuantity;
            public string high52;
            public string purpose;
            public string cm_adj_low_dt;
            public string closePrice;
            public string isExDateFlag;
            public string recordDate;
            public string cm_adj_high_dt;
            public string totalSellQuantity;
            public string dayHigh;
            public string exDate;
            public string sellQuantity5;
            public string bcEndDate;
            public string css_status_desc;
            public string ndEndDate;
            public string sellQuantity2;
            public string sellQuantity1;
            public string buyPrice1;
            public string sellQuantity4;
            public string buyPrice2;
            public string sellQuantity3;
            public string applicableMargin;
            public string buyPrice4;
            public string buyPrice3;
            public string buyPrice5;
            public string dayLow;
            public string deliveryToTradedQuantity;
            public string basePrice;
            public string totalTradedVolume;
        }

        public class fiItem
        {
            public string symbol;
            public string success;
            public fiData results0;
            public fiData results1;
        }

        public class fiData
        {
            public string expenditure;
            public string reProLossBefTax;
            public string proLossAftTax;
            public string fromDate;
            public string income;
            public string reDilEPS;
            public string toDate;
        }

        public ModuleWatchlist(string name)
            : base(name, true)
        {
        }

        private static string GetJson(string sym)
        {
            string tok1 = "<div id=\"responseDiv\" style=\"display:none\">";
            string tok2 = "</div>";
            string response = HttpGet("https://www.nseindia.com/live_market/dynaContent/live_watch/get_quote/GetQuote.jsp?symbol=" + sym + "&illiquid=0&smeFlag=0&itpFlag=0");

            int i = response.IndexOf(tok1) + tok1.Length;
            string substr1 = response.Substring(i, response.Length - i);

            int j = substr1.IndexOf(tok2);
            return substr1.Substring(0, j).Trim();
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
                    ShowPage(24);
                    break;
                case ConsoleKey.F4:
                    ShowPage(25);
                    break;
                case ConsoleKey.F5:
                    ShowPage(activePageId);
                    break;
                case ConsoleKey.F6:
                    ShowPage(26);
                    break;
                case ConsoleKey.F7:
                    ShowPage(27);
                    break;
            }
        }

        public override void ShowMenu()
        {
            ShowPage(1);
        }

        public override void ShowPage(int pageid)
        {
            ShowSubPage(pageid, 1);
        }

        public static void ShowSymbol(object sym)
        {
            var json1 = GetJson(((string)sym).Replace("&", "%26"));
            if (string.IsNullOrEmpty(json1)) { return; }

            var url2 = "https://www.nseindia.com/live_market/dynaContent/live_watch/get_quote/companySnapshot/getFinancialResults" + sym + ".json";
            var json2 = HttpGet(url2);
            if (string.IsNullOrEmpty(json2)) { return; }

            string format = "{0,8}{1,8}{2,8}{3,8}{4,12}{5,9}{6,9}{7,9}{8,9}{9,9}{10,9}{11,14}{12,14:N2}{13,14}{14,14}";
            quItem quItems = JsonConvert.DeserializeObject<quItem>(json1);
            fiItem fiItems = JsonConvert.DeserializeObject<fiItem>(json2);

            if (quItems.data.Count <= 0) return;

            Console.WriteLine(format, (((Newtonsoft.Json.Linq.JContainer)(Module.peItems)))[sym]["PE"].ToString().Trim(),
                fiItems.results0.reDilEPS, quItems.data[0].change, quItems.data[0].pChange,
                sym, quItems.data[0].lastPrice, quItems.data[0].averagePrice,
                quItems.data[0].dayHigh, quItems.data[0].dayLow,
                quItems.data[0].high52, quItems.data[0].low52,
                quItems.data[0].totalTradedVolume, quItems.data[0].totalTradedValue,
                fiItems.results0.income, fiItems.results0.proLossAftTax);
        }

        public void ShowSubPage(int pageid, int subPageid)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(69, 1);
            Console.WriteLine(System.DateTime.Now);
            Console.ResetColor();

            string[] symbols = System.IO.File.ReadAllLines("../../folio/mywatchlist");

            activeSubPageId = subPageid;

            int maxItem = 25;
            maxSubPages = (symbols.Length / maxItem) + ((symbols.Length % maxItem > 0) ? 1 : 0);
            int firstPage = ((activeSubPageId * maxItem) - maxItem + 1);
            int lastPage = ((activeSubPageId * maxItem) > symbols.Length) ? symbols.Length : (activeSubPageId * maxItem);

            Console.SetCursorPosition(99, 1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" {0} - {1} of {2}", firstPage, lastPage, symbols.Length);
            Console.ResetColor();
            Console.SetCursorPosition(122, Console.CursorTop);
            Console.WriteLine("   << Prev (F1)  |  Next (F2) >>");

            Console.SetCursorPosition(0, 0);
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(" " + this.Title);
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------------------------------------------------");
            string format = "{0,8}{1,8}{2,8}{3,8}{4,12}{5,9}{6,9}{7,9}{8,9}{9,9}{10,9}{11,14}{12,14:N2}{13,14}{14,14}";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(format, "p/e", "eps", "chg", "chg %",
                "sym", "ltp", "vwap", "hi", "lo", "52-hi", "52-lo", "qty", "val", "inc", "prof");
            Console.ResetColor();

            int skip = 0;

            foreach (var sym in symbols)
            {
                skip++;
                if (!(skip >= firstPage && skip <= lastPage))
                {
                    continue;
                }

                System.Threading.ThreadPool.QueueUserWorkItem(ShowSymbol, sym);
            }

            ReadInput();
        }
    }
}
