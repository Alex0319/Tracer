
namespace Parser
{
    public interface IParser
    {
        bool Parse(string[] args);
        string GetArgValue(char argName);
    }
}
