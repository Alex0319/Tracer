using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public interface IParser
    {
        Dictionary<char, string> Parse(string[] args);
    }
}
