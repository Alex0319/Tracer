using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fclp;

namespace Parser
{
    public class Parser: IParser
    {
        private static Parser _instance;
        private Dictionary<char, string> argsDictionary;

        private Parser()
        {
            argsDictionary = new Dictionary<char, string>()
            {
                { 'f', null },
                { 'o', null },
                { 'h', null }
            };
        }

        public static Parser Instance => _instance ?? (_instance = new Parser());

        public Dictionary<char, string> Parse(string[] args)
        {
            if (args.Length == 0)
            {
                return null;
            }
            var parser = new FluentCommandLineParser();
            foreach (var param in argsDictionary)
            {
                parser.Setup<string>(param.Key).Callback(val => argsDictionary[param.Key] = val);
            }
            parser.Parse(args);
            return argsDictionary;
        }
    }
}
