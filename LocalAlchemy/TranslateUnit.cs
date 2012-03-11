
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
        public int Sort { get; set; }
        public bool IsValid { get; set; }

        public override string ToString()
        {
            return string.Format("{0}={1}", Key, Value);
        }
    }
}
