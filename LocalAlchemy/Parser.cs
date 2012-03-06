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
    }
}
