using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public interface IParser
    {
        bool Parse(string[] args);
        string GetArgValue(char argName);
    }
}
