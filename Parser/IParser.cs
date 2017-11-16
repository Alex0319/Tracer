
namespace Parser
{
    public interface IParser
    {
        bool Parse(string[] args);
        string GetFormat();
        string GetOutputFilePath();
    }
}
