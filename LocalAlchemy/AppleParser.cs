using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace LocalAlchemy
{
    public class AppleParser : Parser
    {
        public AppleParser()
            : base(".strings")
        {
        }

        public override IEnumerable<TranslateUnit> Parse(string file)
        {
            string[] rows = File.ReadAllLines(file);
            string keyRegex = @"""([\w\d\s_\-]+)""";
            string valueRegex = @"=\s*\""(.+)\"";";

            int i=0;
            return rows
                .Where(r => !r.StartsWith("//") || !r.StartsWith("/*"))
                .Select(r => 
                    {
                        // now to parse the row
                        
                        var keymatch = Regex.Match(r, keyRegex);
                        var valuematch = Regex.Match(r, valueRegex);

                        var result = new TranslateUnit();
                        result.Key = keymatch.Groups[1].Value;
                        result.Value = valuematch.Groups[1].Value;
                        result.Sort = i;
                        result.IsValid = keymatch.Success && valuematch.Success;

                        Interlocked.Increment(ref i);

                        return result;
                    });
        }

        public override void Write(string sourcefile, string targetLang, IEnumerable<TranslateUnit> translated)
        {
            
        }
    }
}
