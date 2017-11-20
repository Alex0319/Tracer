using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fclp;
using Fclp.Internals.Extensions;

namespace Parser
{
    public class Parser: IParser
    {
        private static Parser _instance;
        private readonly Dictionary<string, string> _argsDictionary;

        private Parser()
        {
            _argsDictionary = new Dictionary<string, string>()
            {
                { "f", null },
                { "o", null },
                { "h", null }
            };
        }

        public static Parser Instance => _instance ?? (_instance = new Parser());

        public bool Parse(string[] args)
        {
            bool isNotHelpArg = true;
            if (args.Length == 0 || !CheckArgsCount(Array.FindAll(args, x => x.Contains("--"))))
            {
                return false;
            }
            var parser = new FluentCommandLineParser();
            foreach (var param in _argsDictionary)
            {
                parser.Setup<string>(param.Key[0]).Callback(val => _argsDictionary[param.Key] = val);
            }
            parser.SetupHelp("?", "help", "h").Callback(() =>
            {
                isNotHelpArg = false;
                if(args.Length > 1)
                    _argsDictionary["h"] = "help";
            });
            parser.Parse(args);
            return isNotHelpArg;
        }

        public string GetArg(string argName)
        {
            if(_argsDictionary.ContainsKey(argName))
                return _argsDictionary[argName];
            return null;
        }

        public string GetArgsInfo(string formatInfo)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (_argsDictionary.Values.Any(x => !x.IsNullOrEmpty()))
            {
                stringBuilder.AppendLine("Uncorrect command format\n");
            }
            stringBuilder.AppendLine("  --f      Select format for trace result");
            stringBuilder.Append((char) 32, 11);
            stringBuilder.AppendLine(formatInfo);
            stringBuilder.Append(
                "  --o      Select output file for trace result. If file is not specified result will be shown in the console");
            return stringBuilder.ToString();
        }

        private bool CheckArgsCount(string[] args)
        {
            foreach (var arg in args)
            {
                if (!_argsDictionary.ContainsKey(arg.Substring(2)))
                    return false;
            }
            return true;
        }
    }
}
