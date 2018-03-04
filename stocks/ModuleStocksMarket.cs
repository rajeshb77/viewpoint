using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace viewpoint
{
    public class smItem
    {
        public string trdVolumesumMil;
        public string time;
        public IList<smLatestData> latestData;
        public string declines;
        public string trdValueSum;
        public IList<smData> data;
        public string trdValueSumMil;
        public string unchanged;
        public string trdVolumesum;
        public string advances;
    }

    public class smLatestData
    {
        public string indexName;
        public string open;
        public string high;
        public string low;
        public string ltp;
        public string ch;
        public string per;
        public string yCls;
        public string mCls;
        public string yHigh;
        public string yLow;
    }

    public class smData
    {
        public string symbol;
        public string open;
        public string high;
        public string low;
        public string ltP;
        public string ptsC;
        public string per;
        public string trdVol;
        public string trdVolM;
        public string ntP;
        public string mVal;
        public string wkhi;
        public string wklo;
        public string wkhicm_adj;
        public string wklocm_adj;
        public string xDt;
        public string cAct;
        public string previousClose;
        public string dayEndClose;
        public string iislPtsChange;
        public string iislPercChange;
        public string yPC;
        public string mPC;
    }

    public class ModuleStocksMarket : Module
    {
        public string[] categories = { "nifty", "juniorNifty", "niftyMidcap50", "cnxAuto", "bankNifty", "cnxEnergy", "cnxFinance", "cnxFMCG", 
                "cnxit", "cnxMedia", "cnxMetal", "cnxPharma", "cnxPSU", "cnxRealty", "niftyPvtBank", "cnxCommodities", 
                "cnxConsumption", "cpse", "cnxInfra", "cnxMNC", "ni15", "cnxPSE", "cnxService", "nseliquid", 
                "niftyMidcapLiq15", "cnxDividendOppt", "nv20", "niftyQuality30",
                "nifty50EqualWeight", "nifty100EqualWeight", "nifty100LowVol30", "niftyAlpha50" };

        public string[] categoriesNames = { "Nifty 50", "Nifty Next 50", "Nifty Midcap 50", "Nifty Auto", 	"Nifty Bank", "Nifty Energy", "Nifty Financial Services", "Nifty FMCG", 
                "Nifty IT", "Nifty Media", "Nifty Metal", "Nifty Pharma", "Nifty PSU Bank", "Nifty Realty", "Nifty Private Bank", "Nifty Commodities", 
                "Nifty India Consumption", "Nifty CPSE", "Nifty Infrastructure", "Nifty MNC", "Nifty Growth Sector 15", "Nifty PSE", "Nifty Services Sector", 	"Nifty 100 Liquid 15", 
                "Nifty Midcap Liquid 15", "Nifty Dividend Opportunities 50", "Nifty 50 Value 20", "Nifty Quality 30",
                "Nifty 50 Equal Weight", "Nifty 100 Equal Weight", "Nifty 100 Low Volatility 30", "Nifty Alpha 50" };

        private int catId;
        private string SelectedIndice;
        public ModuleStocksMarket(string name)
            : base(name, true)
        {
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
            Console.Write(" " + this.Title);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" >> {0}", categoriesNames[pageid]);
            Console.ResetColor();
            Console.WriteLine("------------------------------------------------------------------------------------------");

            var url = "https://www.nseindia.com/live_market/dynaContent/live_watch/stock_watch/" + categories[pageid] + "StockWatch.json";
            var json = HttpGet(url);

            if (string.IsNullOrEmpty(json))
            {
                return;
            }

            SelectedIndice = categories[pageid];
            smItem items = JsonConvert.DeserializeObject<smItem>(json);

            activeSubPageId = subPageid;
            maxSubPages = (items.data.Count / 25) + ((items.data.Count % 25 > 0) ? 1 : 0);
            int firstPage = ((activeSubPageId * 25) - 24);
            int lastPage = ((activeSubPageId * 25) > items.data.Count) ? items.data.Count : (activeSubPageId * 25);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{9,9}{10,8}{0,12}{1,5}{2,6} {3,9} {6,8}  {7,6:N2} {8,6:N2}",
                "symbol", "tnd", "52w", "ltP", "-", "-", "vol", "  low %", "  high %", "chg", "%");
            Console.ResetColor();

            int skip = 0;

            foreach (var s in items.data)
            {
                skip++;
                if (!(skip >= firstPage && skip <= lastPage))
                {
                    continue;
                }

                var dl = (float.Parse(s.low) - float.Parse(s.previousClose)) * 100 / float.Parse(s.previousClose);
                var dh = (float.Parse(s.high) - float.Parse(s.previousClose)) * 100 / float.Parse(s.previousClose);

                var pl = float.Parse(s.previousClose) - float.Parse(s.low);
                var ph = float.Parse(s.previousClose) - float.Parse(s.high);

                var tu = (pl == 0 || ph <= 0) && dh >= 2 ? "[++]" : "";
                var td = (pl > 0 || ph == 0) && dl <= -2 ? "[--]" : "";
                var tt = tu + td;
                tt = tt.Replace("++][--", "+-");

                string w52l = (float.Parse(s.low) - float.Parse(s.wklo)) <= 0 ? "|<···" : "";
                string w52h = (float.Parse(s.wkhi) - float.Parse(s.high)) <= 0 ? "···>|" : "";

                if (string.IsNullOrEmpty(w52l))
                {
                    var wklo = float.Parse(s.wklo);
                    var lo = float.Parse(s.low);
                    if ((lo <= (1.10 * wklo)) && (lo >= (1.05 * wklo)))
                    {
                        w52l = "···<·";
                    }
                    else if ((lo <= (1.07 * wklo)) && (lo >= (1.05 * wklo)))
                    {
                        w52l = "··<··";
                    }
                    else if ((lo <= (1.05 * wklo)) && (lo >= (1.02 * wklo)))
                    {
                        w52l = "·<···";
                    }
                    else if ((lo <= (1.02 * wklo)) && (lo >= (1 * wklo)))
                    {
                        w52l = "<····";
                    }
                }

                if (string.IsNullOrEmpty(w52h))
                {
                    var wkhi = float.Parse(s.wkhi);
                    var hi = float.Parse(s.high);
                    if ((hi >= (0.90 * wkhi)) && (hi <= (0.93 * wkhi)))
                    {
                        w52h = "·>···";
                    }
                    else if ((hi >= (0.93 * wkhi)) && (hi <= (0.95 * wkhi)))
                    {
                        w52h = "··>··";
                    }
                    else if ((hi >= (0.95 * wkhi)) && (hi <= (0.98 * wkhi)))
                    {
                        w52h = "···>·";
                    }
                    else if ((hi >= (0.98 * wkhi)) && (hi <= (1 * wkhi)))
                    {
                        w52h = "····>";
                    }
                }

                string w52 = w52h + w52l;
                if (string.IsNullOrEmpty(w52))
                {
                    w52 = "·····";
                }

                Console.Write("{0,9}", s.iislPtsChange);
                Console.ForegroundColor = (float.Parse(s.iislPtsChange) > 0 ? ConsoleColor.Green : ConsoleColor.Red);
                Console.Write("{0} %", ((float.Parse(s.iislPtsChange) >= 0) ? " +" : " ") + s.iislPercChange);
                Console.ResetColor();

                Console.WriteLine("{0,12}{1,5}{2,6} {3,9} {6,8} {7,6:N2} % {8,6:N2} % ",
                    s.symbol, tt, w52, s.ltP, s.iislPtsChange, s.iislPercChange,
                    s.trdVol,
                    dl, dh);

                //Console.WriteLine("{0,12}{1,5}{2,6} {3,9} {4,9} {5,9} % {6,8} :VOL {7,6:N2} % {8,6:N2} % ",
                //    s.symbol, tt, w52, s.ltP, s.iislPtsChange, s.iislPercChanges.iislPercChange
                //    s.trdVol,
                //    dl, dh);
            }
            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" {0} - {1} of {2}", firstPage, lastPage, items.data.Count);
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
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" " + this.Title);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" >> NSE Index");
            Console.ResetColor();
            Console.WriteLine("------------------------------------------------------------------------------------------");

            int ind = 1;
            foreach (string category in categoriesNames)
            {
                Console.Write(" {0,2}. {1,-35}", ind, category);
                if (ind % 2 == 0)
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
