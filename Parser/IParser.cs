namespace Utilities.Parser
{
    public interface IParser
    {
        bool CanBeParsed(string[] args);
        string GetArgumentValue(string argName);
        string GetArgsInfo(string getInfo);
    }
}
