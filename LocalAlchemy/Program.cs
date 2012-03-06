using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Options;
using System.IO;

namespace LocalAlchemy
{
    class Program
    {
        private static string slang;
        private static string dlang;
        private static string sfile;
        private static bool show_help = false;
        private static string bingkey;

        static void Main(string[] args)
        {
            var p = ConfigureOptions();

            try
            {
                var extra = p.Parse(args);

                if (show_help)
                    ShowHelp();
                else
                    Process();

            }
            catch (OptionException e)
            {
                ProcessException(e);
            }
        }

        private static void Process()
        {
            
        }

        private static OptionSet ConfigureOptions()
        {
            var p = new OptionSet() {
                { "s|src=", "the source you want to translate.",
                   v => sfile = v },
                { "slang=", 
                   "the source language",
                    v => slang = v ?? "en" },
                { "d|dest", "the target langue",
                   v => dlang = v ?? "sp" },
                { "h|help",  "show this message and exit", 
                   v => show_help = v != null },
                { "k|key", "the api key. can optionally be provided in ~/.localalchemy",
                    v => 
                    {
                        if (string.IsNullOrWhiteSpace(v))
                        {
                            string dotfilepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".localalchemy");

                            if (File.Exists(dotfilepath)) 
                            {
                                bingkey = File.ReadAllText(dotfilepath);
                                return;
                            }
                        }
                        else 
                        {
                            bingkey = v;
                            return;
                        }

                        throw new InvalidDataException("Must provide the k|key parameter with the bing api key.");
                    }}

            };
            return p;
        }

        private static void ShowHelp()
        {
            Console.WriteLine(@"usage: LocalAlchemy -s strings.xml -slang en -d sp");
        }

        private static void ProcessException(OptionException e)
        {
            Console.Write("LocalAlchemy: ");
            Console.WriteLine(e.Message);
            Console.WriteLine("Try `LocalAlchemy --help' for more information.");
            return;
        }
    }
}
