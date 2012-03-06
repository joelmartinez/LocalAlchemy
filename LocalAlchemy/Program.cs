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
                {
                    if (Validate())
                        Process();
                }

            }
            catch (OptionException e)
            {
                ProcessException(e);
            }
        }

        private static bool Validate()
        {
            bool success = true;

            if (string.IsNullOrWhiteSpace(bingkey))
            {
                string dotfilepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".localalchemy");

                if (File.Exists(dotfilepath))
                {
                    bingkey = File.ReadAllText(dotfilepath);
                }
                else
                {
                    success = false;
                    Console.WriteLine("Must provide the k|key parameter with the bing api key.");
                }
            }

            if (string.IsNullOrWhiteSpace(sfile) || !File.Exists(sfile))
            {
                success = false;
                Console.WriteLine("Must provide source file in -s|sfile");
            }

            if (string.IsNullOrWhiteSpace(slang))
            {
                slang = "en";
            }

            if (string.IsNullOrWhiteSpace(dlang))
            {
                success = false;
                Console.WriteLine("Must provide destination language in -d|dlang");
            }

            return success;
        }

        private static void Process()
        {
            Console.WriteLine("processing ..." + bingkey );
            Console.WriteLine("sfile: " + sfile);


            //Bing.LanguageServiceClient client = new Bing.LanguageServiceClient();
            //string translatedText = client.Translate(bingkey, textToTranslate, slang, dlang);
   
        }

        private static OptionSet ConfigureOptions()
        {
            var p = new OptionSet() {
                { "s|src=", "the source you want to translate.",
                   v => sfile = v },
                { "slang=", 
                   "the source language",
                    v => slang = v ?? "en" },
                { "d|dest=", "the target langue",
                   v => dlang = v ?? "sp" },
                { "h|help",  "show this message and exit", 
                   v => show_help = v != null },
                { "k|key=", "the api key. can optionally be provided in ~/.localalchemy",
                    v => bingkey = v}

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
