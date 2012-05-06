using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        protected virtual string[] ReadFile(string path)
        {
            return File.ReadAllLines(path);
        }

        public override IEnumerable<TranslateUnit> Parse(string file)
        {
            string[] rows = ReadFile(file);
            string keyRegex = @"""([\w\d\s_\-]+)""";
            string valueRegex = @"=\s*\""(.+)\"";";

            int i = 0;
            return rows
                .Where(r => !r.StartsWith("/*"))
                .Select(r =>
                    {
                        TranslateUnit result;

                        // now to parse the row
                        if (r.StartsWith("//"))
                        {
                            result = TranslateUnit.Comment(r);
                        }
                        else if (r.Length < 8) // 8 is the # of chars in '"a"="b";'
                        {
                            result = TranslateUnit.Whitespace();
                        }
                        else
                        {
                            var keymatch = Regex.Match(r, keyRegex);
                            var valuematch = Regex.Match(r, valueRegex);

                            result = new TranslateUnit();
                            result.Key = keymatch.Groups[1].Value;
                            result.Value = valuematch.Groups[1].Value;
                            result.IsValid = keymatch.Success && valuematch.Success;


                        }

                        result.Sort = i;
                        Interlocked.Increment(ref i);
                        return result;
                    });
        }

        public override void Write(string sourcefile, string targetLang, IEnumerable<TranslateUnit> translated)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var result in translated)
            {
                if (result is TranslateUnit.CommentTranslateUnit)
                {
                    sb.AppendFormat("{0}\n", result.Value);
                }
                else if (result is TranslateUnit.WhitespaceTranslateUnit)
                {
                    sb.Append("\n");
                }
                else
                {
                    sb.AppendFormat("\"{0}\" = \"{1}\";\n", result.Key, result.EscapedValue);
                }
            }

            string dpath = Path.Combine(
                Path.GetDirectoryName(sourcefile),
                string.Format("{0}.{1}{2}", Path.GetFileNameWithoutExtension(sourcefile), targetLang, this.FileType));

            File.WriteAllText(dpath, sb.ToString());
        }
    }
}
