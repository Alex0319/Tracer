using System;
using FormatterInterface;
using Tracer.TraceResultData;

namespace Formatter
{
    public class XmlTraceResultFormatter : ITraceResultFormatter
    {
        private const string Name = "xml";

        public string GetFormat()
        {
            return Name;
        }

        public string Format(TraceResult traceResult)
        {
            throw new NotImplementedException();
        }
    }
}
