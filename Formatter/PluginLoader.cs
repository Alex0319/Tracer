using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FormatterInterface;

namespace Formatter
{
    internal class PluginLoader
    {
        internal Dictionary<string, ITraceResultFormatter> LoadPlugins()
        {
            Dictionary<string, ITraceResultFormatter> formatters = new Dictionary<string, ITraceResultFormatter>();
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Plugins");

            if (Directory.Exists(folder))
            {
                string[] files = Directory.GetFiles(folder, "*.dll");

                foreach (var file in files)
                {
                    var formatterPlugin = IsPlugin(file);
                    if (formatterPlugin != null)
                    {
                        formatters.Add(formatterPlugin.GetFormat(), formatterPlugin);
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(folder);
            }
            return formatters;
        }

        private ITraceResultFormatter IsPlugin(byte[] file)
        {
            try
            {
                Assembly assembly = Assembly.Load(file);
                foreach (Type type in assembly.GetExportedTypes())
                {
                    if(type.IsClass && typeof(ITraceResultFormatter).IsAssignableFrom(type))
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
