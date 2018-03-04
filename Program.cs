using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace viewpoint
{
    class Program
    {
        public static Module homeScreen;
        static void Main(string[] args)
        {
            InitializeMenu();
            Console.ReadLine();
        }

        public static void InitializeMenu()
        {
            homeScreen = new Module("Home");
            Module.root = homeScreen;

            Module modStocks = new Module("Stocks");
            Module modMutualFunds = new Module("Mutual Funds");
            Module modIPO = new Module("IPO");
            Module modBonds = new Module("Bonds");

            homeScreen.AddMenu(modStocks);
            homeScreen.AddMenu(modMutualFunds);
            homeScreen.AddMenu(modIPO);
            homeScreen.AddMenu(modBonds);

            Module modNifty = new ModuleStocksMarket("Nifty");
            Module modPortfolio = new ModulePortfolio("My Portfolio");
            Module modSmallCase = new ModuleWatchlistEx("SmallCase");
            Module modTop100 = new ModuleWatchlistEx("top companies");

            Module modScreener = new Module("Screener");

            Module mod52Weeks = new ModuleStocks52Weeks("52 Weeks");
            Module modGainLose = new ModuleStocksGainLose("Gainers & Losers");
            Module modValVol = new ModuleStocksQtyVal("Active Securities");

            modStocks.AddMenu(modPortfolio);
            modStocks.AddMenu(modSmallCase);
            modStocks.AddMenu(modTop100);            
            modStocks.AddMenu(modNifty);
            modStocks.AddMenu(modScreener);
            modStocks.AddMenu(mod52Weeks);
            modStocks.AddMenu(modGainLose);
            modStocks.AddMenu(modValVol);

            Module modSMA = new ModuleTechSMA("SMA");
            Module modEMA = new ModuleTechEMA("EMA");
            Module modRSI = new ModuleTechRSI("RSI (14)");
            Module modMACD = new ModuleTechMACD("MACD (12,26,9)");
            Module modBollingerBand = new ModuleTechBollingerBand("Bollinger Band (20,2)");
            Module modStochastic = new ModuleTechStochastic("Stochastic (14)");

            modScreener.AddMenu(modSMA);
            modScreener.AddMenu(modEMA);
            modScreener.AddMenu(modRSI);
            modScreener.AddMenu(modMACD);
            modScreener.AddMenu(modBollingerBand);
            modScreener.AddMenu(modStochastic);

            homeScreen.ShowMenu();
        }
    }
}
