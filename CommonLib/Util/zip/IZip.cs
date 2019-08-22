namespace CommonLib.Util.zip
{
    public interface IZip
    {
        void ExtractZip(string source, string destination);
        void ExtractZip(string source);
    }
}
