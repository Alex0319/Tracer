using System.Collections.Generic;
using Fclp;
using Fclp.Internals.Extensions;

namespace Parser
{
    public class Parser: IParser
    {
        private static Parser _instance;
        private readonly Dictionary<char, string> _argsDictionary;

        private Parser()
        {
            _argsDictionary = new Dictionary<char, string>()
            {
                { 'f', null },
                { 'o', null },
                { 'h', null }
            };
        }

        public static Parser Instance => _instance ?? (_instance = new Parser());

        public bool Parse(string[] args)
        {
            if (args.Length == 0)
            {
                return false;
            }
            var parser = new FluentCommandLineParser();
            foreach (var param in _argsDictionary)
            {
                parser.Setup<string>(param.Key).Callback(val => _argsDictionary[param.Key] = val);
            }
            parser.Parse(args);
            return true;
        }

        public string GetFormat()
        {
           return _argsDictionary['f'];
        }

        public string GetOutputFilePath()
        {
            return _argsDictionary['o'];
        }
    }
}
