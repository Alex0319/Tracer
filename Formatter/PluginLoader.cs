using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FormatterInterface;

namespace Formatter
{
    internal class PluginLoader
    {
        private static Dictionary<string ,ITraceResultFormatter> _plugins;

        internal Dictionary<string,ITraceResultFormatter> Plugins => _plugins;

        internal void LoadPlugins()
        {
            _plugins = new Dictionary<string, ITraceResultFormatter>();
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            string[] files = Directory.GetFiles(folder, "*.dll");

            foreach (var file in files)
            {
                ITraceResultFormatter formatterPlugin = IsPlugin(file);
                if (formatterPlugin != null)
                {
                    _plugins.Add(formatterPlugin.GetFormat(), formatterPlugin);
                }
            }
        }

        private ITraceResultFormatter IsPlugin(byte[] file)
        {
            try
            {
                Assembly assembly = Assembly.Load(file);
                foreach (Type type in assembly.GetTypes())
                {
                    Type interfaceType = type.GetInterface("ITraceResultFormatter");
                    if (interfaceType != null)
                    {
                        return (ITraceResultFormatter)Activator.CreateInstance(type);
                    }
                }
            }
            catch (Exception e)
            {
                ToLog(e.Message);
            }
            return null;
        }

        private ITraceResultFormatter IsPlugin(string fileUrl)
        {
            try
            {
                byte[] b = File.ReadAllBytes(fileUrl);
                return IsPlugin(b);
            }
            catch (Exception e)
            {
                ToLog(e.Message);
            }
            return null;
        }

        private void ToLog(string text)
        {
            try
            {
                string folder = AppDomain.CurrentDomain.BaseDirectory;
                using (StreamWriter streamWriter = new StreamWriter(folder + @"\Log.txt", true))
                {
                    streamWriter.WriteLine(DateTime.Now + " : " + text);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
