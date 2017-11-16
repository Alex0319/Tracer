using System;
using System.Xml.Linq;

using FormatterInterface;
using Tracer.TraceResultData;
using System.Collections.Generic;
using System.IO;

namespace Formatter
{
    public class XmlTraceResultFormatter : ITraceResultFormatter
    {
        private const string Name = "xml";

        public string GetFormat()
        {
            return Name;
        }

        public void Format(TraceResult traceResult, Stream stream)
        {
            if (traceResult == null)
            {
               throw new ArgumentNullException(nameof(traceResult));
            }
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(traceResult));
            }

            XDocument xmlDoc = new XDocument();
            XElement rootElement = new XElement("root");

            var threadsInfo = traceResult.ResultInfo;

            foreach (var threadInfo in threadsInfo)
            {
                XElement threadElement = new XElement("thread");
                threadElement.Add(new XAttribute("id", threadInfo.Key), new XAttribute("time", threadInfo.Value.ExecutionTime + "ms"));
                FormatMethodsInfo(threadElement, threadInfo.Value.ChildMethods);
                rootElement.Add(threadElement);
            }

            xmlDoc.Add(rootElement);
            xmlDoc.Save(stream);
        }

        private void FormatMethodsInfo(XElement parentElement, IEnumerable<TraceMethodData> childMethods)
        {
            foreach(var method in childMethods)
            {
                XElement methodElement = new XElement("method");
                methodElement.Add(new XAttribute("name", method.Name),
                    new XAttribute("time", method.ExecutionTime + "ms"),
                    new XAttribute("class", method.ClassName),
                    new XAttribute("params", method.ParamsCount));
                parentElement.Add(methodElement);
                FormatMethodsInfo(methodElement, method.ChildMethods);
            }
        }
    }
}
