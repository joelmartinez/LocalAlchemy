using System.Collections.Generic;

namespace LocalAlchemy
{
    public abstract class Parser
    {
        public Parser(string extension)
        {
            FileType = extension;
        }

        public string FileType { get; private set; }

        public abstract IEnumerable<TranslateUnit> Parse(string file);
        public abstract void Write(string sourcefile, string targetLang, IEnumerable<TranslateUnit> translated);

        public static Parser Create(string extension)
        {
            var instances = new Parser[] { new AndroidParser(), new AppleParser() };

            foreach (var v in instances)
            {
                if (v.FileType.Equals(extension)) return v;
            }

            throw new KeyNotFoundException();
        }
    }
}
