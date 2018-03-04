using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace viewpoint
{
    public class Utility
    {
        public static List<string> allSymbols = new List<string>();

        public static void ajaxCompanySearch(string inPath, string ouPath)
        {
            try
            {
                string[] mymf = System.IO.File.ReadAllLines(inPath);
                List<string> mysy = new List<string>();

                int i = 1;
                foreach (var name in mymf)
                {
                    string resp = Module.HttpGet("https://www.nseindia.com/live_market/dynaContent/live_watch/get_quote/ajaxCompanySearch.jsp?search=" + name.Substring(0, (name.Length > 10 ? 10 : name.Length)));

                    if (resp.Contains("No Match Found"))
                    {
                        continue;
                    }

                    int start = resp.IndexOf("<span class='symbol'>") + "<span class='symbol'>".Length;
                    int end = resp.IndexOf("</span>");

                    string sym = resp.Substring(start, end - start).Replace("<b >", "").Replace("</b>", "");
                    mysy.Add(sym);
                    if (i % 10 == 0) Console.Write("#");
                    i++;
                }

                System.IO.File.WriteAllLines(ouPath, mysy.Distinct().ToArray());
            }
            catch (Exception e)
            {
                Console.Write("x");
            }
        }

        public static void loadMySymbols(string folderName)
        {
            string[] files = System.IO.Directory.GetFiles("../../" + folderName + "/").ToArray();

            List<string> symbols = new List<string>();

            foreach (var file in files)
            {
                symbols.AddRange(System.IO.File.ReadAllLines(file).ToList());
            }

            allSymbols.AddRange(symbols.Distinct());
        }

        public static bool findMySymbol(string sym)
        {
            return allSymbols.Contains(sym);
        }

        public class fiItem
        {
            public string sector;
            public string symbol;
            public string PE;
            public string date;
            public string sectorPE;
        }

        public static void GetFinResults()
        {
            string urlpe = "https://www.nseindia.com/homepage/peDetails.json";
            var jsonpe = Module.HttpGet(urlpe);
            Module.peItems = JsonConvert.DeserializeObject(jsonpe);

            List<string> companies = new List<string>();

            companies.Add(string.Format("symbol\tsector\tPE\tsectorPE\tEPS\tincome\tprofit"));

            foreach (var item in (Newtonsoft.Json.Linq.JContainer)(Module.peItems))
            {
                try
                {
                    string sym = item.Path;
                    string url = "https://www.nseindia.com/live_market/dynaContent/live_watch/get_quote/companySnapshot/getFinancialResults" + sym + ".json";

                    fiItem obj1 = JsonConvert.DeserializeObject<fiItem>((((Newtonsoft.Json.Linq.JContainer)(Module.peItems)))[sym].ToString());
                    ModulePortfolio.fiItem obj2 = JsonConvert.DeserializeObject<ModulePortfolio.fiItem>(Module.HttpGet(url));

                    companies.Add(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}",
                        obj1.symbol, obj1.sector, obj1.PE, obj1.sectorPE,
                        obj2.results0.reDilEPS, obj2.results0.income, obj2.results0.proLossAftTax));
                }
                catch (Exception e)
                {
                    Console.Write('x');
                }

                Console.Write(".");
            }

            System.IO.File.WriteAllLines("nse_pe_eps_profit.csv", companies.ToArray());
        }
    }
}
