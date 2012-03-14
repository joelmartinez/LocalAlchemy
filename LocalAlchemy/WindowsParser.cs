using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalAlchemy
{
    public class WindowsParser : Parser
    {
        public WindowsParser()
            : base(".resx")
        {
        }

        public override IEnumerable<TranslateUnit> Parse(string file)
        {
            throw new NotImplementedException();
        }

        public override void Write(string sourcefile, string targetLang, IEnumerable<TranslateUnit> translated)
        {
            throw new NotImplementedException();
        }
    }
}
