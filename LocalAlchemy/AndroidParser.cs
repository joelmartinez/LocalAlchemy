using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace LocalAlchemy
{
    public class AndroidParser : Parser
    {
        public AndroidParser()
            : base(".xml")
        {
        }

        public override IEnumerable<TranslateUnit> Parse(string file)
        {
            XDocument doc = XDocument.Load(file, LoadOptions.None);

            return doc
                .Descendants("resources")
                .Descendants("string")
                .Select(x => new TranslateUnit { Key = x.Attribute("name").Value, Value = x.Value });
        }

        public override void Write(string sourcefile, string targetLang, IEnumerable<TranslateUnit> translated)
        {
            var items = translated.Select(t => new XElement("string",
                new XAttribute("name", t.Key),
                new XText(t.Value)));

            var xml = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement("resources", items.ToArray())
                );

            string dpath = Path.Combine(
                Path.GetDirectoryName(sourcefile),
                string.Format("{0}.{1}.{2}", Path.GetFileNameWithoutExtension(sourcefile), targetLang, this.FileType));

            File.WriteAllText(dpath, xml.ToString());
        }
    }
}
