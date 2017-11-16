using System.IO;
using FormatterInterface;
using Tracer.TraceResultData;

namespace JsonFormatter
{
    public class JsonTraceResultFormatter: ITraceResultFormatter
    {
        private const string Name = "json";

        public string GetFormat()
        {
            return Name;
        }

        public void Format(TraceResult traceResult, Stream stream)
        {
            
        }
    }
}
