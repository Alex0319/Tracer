using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
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
        
        private Formatter()
        {
            var loader = new PluginLoader();
            _formatters = loader.LoadPlugins();
            ITraceResultFormatter formatter = new ConsoleTraceResultFormatter();
            _consoleTypeName = formatter.GetFormat();
            _formatters.Add(formatter.GetFormat(), formatter);
            formatter = new XmlTraceResultFormatter();
            _formatters.Add(formatter.GetFormat(), formatter);
            ResourceManager rm = new ResourceManager("Formatter.Resource",
                Assembly.GetExecutingAssembly());
            Console.WriteLine(rm.GetString("FileNotSpecifiedMessage"));
            foreach (DictionaryEntry res in rm.GetResourceSet(CultureInfo.CurrentUICulture, true, true))
            {
                Console.WriteLine($@"{res.Key} - {res.Value}");
            }
        }

        public static Formatter Instance => _instance ?? (_instance = new Formatter());

        public string GetAvailableFormatters()
        {
            return _formatters.Keys.ToString();
        }

        public string Format(TraceResult traceResult, string formatType, string filePath)
        {
            bool isFilePathValid = CheckFilePath(filePath);
            if (formatType == null)
            {
                return $"Format is not specified\n{GetInfo()}";
            }
            if (!_formatters.ContainsKey(formatType))
            {
                return $"{formatType} format is not available\n{GetInfo()}";
            }
            if (formatType.Equals(_consoleTypeName))
            {
                _formatters[formatType].Format(traceResult, Console.OpenStandardOutput());
            }
            if (!isFilePathValid)
            {
                if (filePath == null)
                    return "Output is not specified";
                return filePath  + " : file or file directory not found";
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                _formatters[formatType].Format(traceResult, fileStream);
            }
            return "Traced info succesfully saved";
        }

        private bool CheckFilePath(string filePath)
        {
            if (filePath != null)
            {
                string directoryName = Path.GetDirectoryName(filePath);
                return string.IsNullOrEmpty(directoryName) || Directory.Exists(directoryName);
            }
            return false;
        }

        public string GetInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Available formats:");
            foreach (var format in _formatters.Keys)
            {
                stringBuilder.Append("   ");
                stringBuilder.Append(format);
            }
            return stringBuilder.ToString();
        }
    }
}
