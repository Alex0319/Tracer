
namespace Parser
{
    public interface IParser
    {
        bool Parse(string[] args);
        string GetArg(string argName);
        string GetArgsInfo(string getInfo);
    }
}
