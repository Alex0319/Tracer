using System;
using System.Collections.Generic;
using Tracer.TraceResultData;

namespace Formatter
{
    public class Formatter
    {
        private static Formatter _instance;
        private static PluginLoader _loader;

        public Formatter()
        {
            _loader = new PluginLoader();
            _loader.LoadPlugins();
        }

        public static Formatter Instance => _instance ?? (_instance = new Formatter());

        public string GetAvailableFormatters()
        {
            return _loader.Plugins.Keys.ToString();
        }

        public bool Format(TraceResult traceResult, string formatType)
        {
            if (_loader.Plugins.ContainsKey(formatType))
            {
                _loader.Plugins[formatType].Format(traceResult);
                return true;
            }
            return false;
        }
    }
}
