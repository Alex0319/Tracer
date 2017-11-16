using System;
using System.Collections.Generic;
using System.IO;
using Formatter.StandartFormatters;
using FormatterInterface;
using Tracer.TraceResultData;

namespace Formatter
{
    public class Formatter
    {
        private static Formatter _instance;
        private static Dictionary<string, ITraceResultFormatter> _formatters;
        private readonly string _consoleTypeName;

        internal Dictionary<string, ITraceResultFormatter> Formatters => _formatters;


        public Formatter()
        {
            var loader = new PluginLoader();
            _formatters = loader.LoadPlugins();
            ITraceResultFormatter formatter = new ConsoleTraceResultFormatter();
            _consoleTypeName = formatter.GetFormat();
            _formatters.Add(formatter.GetFormat(), formatter);
            formatter = new XmlTraceResultFormatter();
            _formatters.Add(formatter.GetFormat(), formatter);
        }

        public static Formatter Instance => _instance ?? (_instance = new Formatter());

        public string GetAvailableFormatters()
        {
            return _formatters.Keys.ToString();
        }

        public string Format(TraceResult traceResult, string formatType, string filePath)
        {
            bool isFilePathValid = CheckFilePath(filePath);
            if (!_formatters.ContainsKey(formatType))
            {
                formatType = _consoleTypeName;
            }
            if (formatType.Equals(_consoleTypeName))
            {
                _formatters[formatType].Format(traceResult, Console.OpenStandardOutput());
            }
            if (!isFilePathValid)
            {
                return filePath + "File or file directory not found";
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                _formatters[formatType].Format(traceResult, fileStream);

            }
            return null;
        }

        private bool CheckFilePath(string filePath)
        {
            return filePath != null && Directory.Exists(Path.GetDirectoryName(filePath));
        }
    }
}
