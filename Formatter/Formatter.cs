using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using FormatterInterface;
using Utilities.Formatter.StandartFormatters;
using Utilities.Tracer.TraceResultData;

namespace Utilities.Formatter
{
    public class Formatter
    {
        private static Formatter _instance;
        private static Dictionary<string, ITraceResultFormatter> _formatters;
        private static Dictionary<string, string> _errorMessages;
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

            _errorMessages = new Dictionary<string, string>();
            ResourceManager rm = new ResourceManager(GetType().Namespace + ".Resources.Resource",
                Assembly.GetExecutingAssembly());
            try
            {
                LoadResources(rm, Thread.CurrentThread.CurrentCulture);
            }
            catch (MissingManifestResourceException)
            {
                LoadResources(rm, CultureInfo.GetCultureInfo("en"));
            }
            finally
            {
                rm.ReleaseAllResources();
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
                return $"{_errorMessages["FormatNotSpecifiedMessage"]}\n{GetInfo()}";
            }
            if (!_formatters.ContainsKey(formatType))
            {
                return $"{formatType}{_errorMessages["FormatNotAvailableMessage"]}\n{GetInfo()}";
            }
            if (formatType.Equals(_consoleTypeName))
            {
                _formatters[formatType].Format(traceResult, Console.OpenStandardOutput());
            }
            if (!isFilePathValid)
            {
                if (filePath == null)
                {
                    return _errorMessages["FileNotSpecifiedMessage"];
                }
                return filePath + _errorMessages["FilePathNotFoundMessage"];
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
            {
                _formatters[formatType].Format(traceResult, fileStream);
            }
            return _errorMessages["SuccessfullyTracedMessage"];
        }

        private void LoadResources(ResourceManager rm, CultureInfo cultureInfo)
        {
            foreach (DictionaryEntry res in rm.GetResourceSet(cultureInfo, true, true))
            {
                _errorMessages.Add(res.Key.ToString(), res.Value.ToString());
            }           
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
            stringBuilder.Append(_errorMessages["FormatsHeader"]);
            foreach (var format in _formatters.Keys)
            {
                stringBuilder.Append("   ");
                stringBuilder.Append(format);
            }
            return stringBuilder.ToString();
        }
    }
}
