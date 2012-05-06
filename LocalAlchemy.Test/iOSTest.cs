using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace LocalAlchemy.Test
{

    [TestFixture]
    public class iOSTest
    {
        [Test]
        public void SimpleParse()
        {
            Parser ios = new MockAppleParser(@"""All Contacts"" = ""All Contacts"";");
            var units = ios.Parse("blah");

            Assert.AreEqual(1, units.Count());
            Assert.AreEqual("All Contacts", units.First().Key);
        }

        [Test]
        public void NonAlphaParse()
        {
            Parser ios = new MockAppleParser(@"""All Contacts."" = ""All Contacts"";");
            var units = ios.Parse("blah");

            Assert.AreEqual(1, units.Count());
            Assert.AreEqual("All Contacts.", units.First().Key);
        }

        private class MockAppleParser : AppleParser
        {
            private string contents;

            public MockAppleParser(string content)
            {
                this.contents = content;
            }

            protected override string[] ReadFile(string path)
            {
                return this.contents.Split(new string[] 
                    {
                        Environment.NewLine
                    }, StringSplitOptions.None);
            }
        }
    }
}
