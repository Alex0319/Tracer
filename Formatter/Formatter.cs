using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using Formatter.StandartFormatters;
using FormatterInterface;
using Tracer.TraceResultData;

namespace Formatter
{
    public class Formatter
    {
        private static Formatter _instance;
        private static Dictionary<string, ITraceResultFormatter> _formatters;
        private static Dictionary<string, string> errorMessages;
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

            errorMessages = new Dictionary<string, string>();
            ResourceManager rm = new ResourceManager("Formatter.Resources.Resource",
                Assembly.GetExecutingAssembly());
            foreach (DictionaryEntry res in rm.GetResourceSet(Thread.CurrentThread.CurrentCulture, true, true))
            {
                errorMessages.Add(res.Key.ToString(), res.Value.ToString());
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
                return $"{errorMessages["FormatNotSpecifiedMessage"]}\n{GetInfo()}";
            }
            if (!_formatters.ContainsKey(formatType))
            {
                return $"{formatType}{errorMessages["FormatNotAvailableMessage"]}\n{GetInfo()}";
            }
            if (formatType.Equals(_consoleTypeName))
            {
                _formatters[formatType].Format(traceResult, Console.OpenStandardOutput());
            }
            if (!isFilePathValid)
            {
                if (filePath == null)
                    return errorMessages["FileNotSpecifiedMessage"];
                return filePath  + errorMessages["FilePathNotFoundMessage"];
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                _formatters[formatType].Format(traceResult, fileStream);
            }
            return errorMessages["SuccessfullyTracedMessage"];
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
            stringBuilder.Append(errorMessages["FormatsHeader"]);
            foreach (var format in _formatters.Keys)
            {
                stringBuilder.Append("   ");
                stringBuilder.Append(format);
            }
            return stringBuilder.ToString();
        }
    }
}
