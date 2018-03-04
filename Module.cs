using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace viewpoint
{
    public class ModuleCollections<T> where T : ModuleCollections<T>
    {
        protected List<T> children;
        protected T parent;
        protected T next;
        protected T prev;

        public ModuleCollections()
        {
            this.children = new List<T>();
        }

        public virtual void AddMenu(T newChild)
        {
            newChild.next = null;
            newChild.prev = null;
            newChild.parent = (T)this;

            if (newChild.parent.children.Count > 1)
            {
                newChild.prev = newChild.parent.children[newChild.parent.children.Count - 1];
            }

            this.children.Add(newChild);
        }

        public void Traverse(Action<int, T> visitor)
        {
            this.traverse(0, visitor);
        }

        protected virtual void traverse(int depth, Action<int, T> visitor)
        {
            visitor(depth, (T)this);
            foreach (T child in this.children)
                child.traverse(depth + 1, visitor);
        }
    }

    public class Module : ModuleCollections<Module>
    {
        public static Module root;
        protected bool Page { get; set; }
        protected bool PageActive { get; set; }
        protected string Name { get; set; }

        protected string Title
        {
            get
            {
                return ((this.parent == null) ? "" : (this.parent.Title + " > ")) + this.Name;
            }
        }
        protected int activeChildScreenId;
        protected int activePageId;
        protected int activeSubPageId;
        protected int maxSubPages;
        protected int maxPages;
        protected int Column { get; set; }
        public static object peItems = null;

        public Module(string name)
        {
            this.Name = name;
            this.Page = false;
            this.PageActive = false;
            this.Column = 1;
            activeChildScreenId = 0;
            activePageId = 0;
            maxPages = 0;
        }

        protected Module(string name, bool page)
        {
            this.Name = name;
            this.Page = page;
            this.Column = 1;
            activeChildScreenId = 0;
            activePageId = 0;
            maxPages = 0;
        }

        public Module(string name, int column)
        {
            this.Name = name;
            this.Page = false;
            this.Column = column;
        }

        public static string HttpGet(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.168 Safari/537.36";

                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (System.Net.WebException e)
            {
            }

            return string.Empty;
        }

        bool IsFunctionKey(ConsoleKey key)
        {
            ConsoleKey[] supportedKeys = { ConsoleKey.Backspace,
                                           ConsoleKey.Tab,
                                           ConsoleKey.Escape,
                                           ConsoleKey.Spacebar,
                                           ConsoleKey.PageUp,
                                           ConsoleKey.PageDown,
                                           ConsoleKey.End,
                                           ConsoleKey.Home,
                                           ConsoleKey.LeftArrow,
                                           ConsoleKey.UpArrow,
                                           ConsoleKey.RightArrow,
                                           ConsoleKey.DownArrow,
                                           ConsoleKey.F1,
                                           ConsoleKey.F2,
                                           ConsoleKey.F3,
                                           ConsoleKey.F4,
                                           ConsoleKey.F5,
                                           ConsoleKey.F6,
                                           ConsoleKey.F7,
                                           ConsoleKey.F8,
                                           ConsoleKey.F9,
                                           ConsoleKey.F10,
                                           ConsoleKey.F11,
                                           ConsoleKey.F12 };
            return supportedKeys.Contains(key);
        }

        public virtual void ProcessFunctionKeyEx(ConsoleKey key)
        {
        }

        void ProcessFunctionKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.Backspace:
                    if (this.Page && this.PageActive)
                    {
                        activeChildScreenId = 0;
                        activePageId = 0;
                        this.PageActive = false;
                        this.ShowMenu();
                    }
                    else
                    {
                        this.parent.ShowMenu();
                    }
                    break;
                case ConsoleKey.Escape:
                    if (this.Page && this.PageActive)
                    {
                        activeChildScreenId = 0;
                        activePageId = 0;
                        this.PageActive = false;
                    }
                    if (this != Module.root)
                    {
                        Module.root.ShowMenu();
                    }
                    break;
                case ConsoleKey.PageUp:
                case ConsoleKey.UpArrow:
                case ConsoleKey.LeftArrow:
                    if (this.Page && this.PageActive)
                    {
                        this.PageActive = true;
                        --this.activePageId;
                        if (this.activePageId < 0)
                        {
                            this.activePageId = this.maxPages - 1;
                        }
                        this.ShowPage(this.activePageId);
                    }
                    else
                    {
                        --this.parent.activeChildScreenId;
                        if (this.parent.activeChildScreenId < 0)
                        {
                            this.parent.activeChildScreenId = this.parent.children.Count - 1;
                        }
                        this.parent.ShowChildMenu(this.parent.activeChildScreenId);
                    }
                    break;
                case ConsoleKey.Tab:
                case ConsoleKey.Spacebar:
                case ConsoleKey.PageDown:
                case ConsoleKey.DownArrow:
                case ConsoleKey.RightArrow:
                    if (this.Page && this.PageActive)
                    {
                        this.PageActive = true;
                        ++this.activePageId;
                        if (this.activePageId >= this.maxPages)
                        {
                            this.activePageId = 0;
                        }
                        this.ShowPage(this.activePageId);
                    }
                    else
                    {
                        ++this.parent.activeChildScreenId;
                        if (this.parent.activeChildScreenId >= this.parent.children.Count)
                        {
                            this.parent.activeChildScreenId = 0;
                        }
                        this.parent.ShowChildMenu(this.parent.activeChildScreenId);
                    }
                    break;
                case ConsoleKey.F1:
                case ConsoleKey.F2:
                case ConsoleKey.F3:
                case ConsoleKey.F4:
                case ConsoleKey.F5:
                case ConsoleKey.F6:
                case ConsoleKey.F7:
                    ProcessFunctionKeyEx(key);
                    break;
            }
        }

        protected void ReadInput()
        {
            ConsoleKeyInfo consoleKey;

            do
            {
                consoleKey = Console.ReadKey();
                if (IsFunctionKey(consoleKey.Key))
                {
                    ProcessFunctionKey(consoleKey.Key);
                    break;
                }
                else
                {
                    if (consoleKey.KeyChar >= 48 && consoleKey.KeyChar <= 57)
                    {
                        if (this.Page)
                        {
                            if (this.maxPages < 9)
                            {
                                activeChildScreenId = consoleKey.KeyChar - 48;
                                break;
                            }
                        }
                        else
                        {
                            if (this.children.Count < 9)
                            {
                                activeChildScreenId = consoleKey.KeyChar - 48;
                                break;
                            }
                        }

                        activeChildScreenId = (activeChildScreenId * 10);
                        activeChildScreenId += consoleKey.KeyChar - 48;
                    }
                    else if (consoleKey.Key != ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (consoleKey.Key != ConsoleKey.Enter);

            if (consoleKey.Key == ConsoleKey.Escape)
            {
                ReadInput();
                return;
            }

            activeChildScreenId -= (activeChildScreenId > 0 ? 1 : 0);

            if (this.Page)
            {
                this.PageActive = true;
                activePageId = activeChildScreenId;
                this.ShowPage(activePageId);
            }

            if (this.children.Count > 0 && this.children[activeChildScreenId].Page)
            {
                this.children[activeChildScreenId].ShowMenu();
            }
            else
            {
                if (activeChildScreenId >= 0 && activeChildScreenId <= this.children.Count)
                {
                    this.ShowChildMenu(activeChildScreenId);
                }
                else
                {
                    ShowMenu();
                }
            }
        }

        public void ShowChildMenu(int screenId)
        {
            activeChildScreenId = screenId;
            this.children[activeChildScreenId].ShowMenu();
        }

        public virtual void ShowPage(int menuId)
        {

        }

        public virtual void ShowMenu()
        {
            if (Module.peItems == null)
            {
                string urlpe = "https://www.nseindia.com/homepage/peDetails.json";
                var jsonpe = HttpGet(urlpe);
                Module.peItems = JsonConvert.DeserializeObject(jsonpe);
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(69, 1);
            Console.WriteLine(System.DateTime.Now);
            Console.ResetColor();
            Console.SetCursorPosition(0, 0);

            Console.WriteLine("------------------------------------------------------------------------------------------");
            Console.WriteLine(" " + this.Title);
            Console.WriteLine("------------------------------------------------------------------------------------------");

            activeChildScreenId = 0;

            try
            {
                int ind = 1;
                foreach (Module child in this.children)
                {
                    Console.Write(" {0,2}. {1,-30}", ind, child.Name);
                    if (ind % this.Column == 0)
                    {
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.Write("|");
                    }
                    ind++;
                }
                Console.WriteLine("------------------------------------------------------------------------------------------");

                ReadInput();
            }
            catch (Exception e)
            {
                ShowMenu();
            }
        }
    }
}
