
namespace LocalAlchemy
{
    public class TranslateUnit
    {
        public TranslateUnit()
        {
            IsValid = true;
        }

        public string Key { get; set; }
        public string Value { get; set; }

        public virtual string CleanValue
        {
            get
            {
                return this.Value
                    .Replace("\\\"", "\"")
                    .Replace("\\'", "'");
            }
        }

        public virtual string EscapedValue
        {
            get
            {
                return this.Value
                    .Replace("\"", "\\\"")
                    .Replace("'", "\\'");
            }
        }

        public int Sort { get; set; }
        public bool IsValid { get; set; }

        public override string ToString()
        {
            return string.Format("{0}={1}", Key, Value);
        }

        public static TranslateUnit Comment(string value)
        {
            CommentTranslateUnit unit = new CommentTranslateUnit
            {
                Value = value
            };

            return unit;
        }

        public static TranslateUnit Whitespace()
        {
            return new WhitespaceTranslateUnit();
        }

        public class CommentTranslateUnit : TranslateUnit
        {
            public CommentTranslateUnit()
            {
                IsValid = false;
            }

            public override string CleanValue
            {
                get
                {
                    return Value;
                }
            }

            public override string EscapedValue
            {
                get
                {
                    return Value;
                }
            }
        }

        public class WhitespaceTranslateUnit : TranslateUnit
        {
            public WhitespaceTranslateUnit()
            {
                IsValid = false;
            }

            public override string CleanValue
            {
                get
                {
                    return Value;
                }
            }

            public override string EscapedValue
            {
                get
                {
                    return Value;
                }
            }
        }
    }
}
