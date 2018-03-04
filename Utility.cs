using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viewpoint
{
    public class Utility
    {
        public static List<string> allSymbols = new List<string>();

        public static void ajaxCompanySearch(string inPath, string ouPath)
        {
            string[] mymf = System.IO.File.ReadAllLines(inPath);
            List<string> mysy = new List<string>();

            foreach (var name in mymf)
            {
                string resp = Module.HttpGet("https://www.nseindia.com/live_market/dynaContent/live_watch/get_quote/ajaxCompanySearch.jsp?search=" + name.Substring(0, (name.Length > 10 ? 10 : name.Length)));

                if (resp.Contains("No Match Found"))
                {
                    continue;
                }

                int start = resp.IndexOf("<span class='symbol'>") + "<span class='symbol'>".Length;
                int end = resp.IndexOf("</span>");

                string sym = resp.Substring(start, end - start);
                mysy.Add(sym);
            }

            System.IO.File.WriteAllLines(ouPath, mysy.Distinct().ToArray());
        }

        public static void loadMySymbols(string folderName) 
        {
            string[] files = System.IO.Directory.GetFiles("../../" + folderName + "/").ToArray();

            List<string> symbols = new List<string>();

            foreach(var file in files)
            {
                symbols.AddRange(System.IO.File.ReadAllLines(file).ToList());
            }

            allSymbols.AddRange(symbols.Distinct());            
        }

        public static bool findMySymbol(string sym)
        {
            return allSymbols.Contains(sym);
        }
    }
}
