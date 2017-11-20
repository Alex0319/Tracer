using System;
using System.IO;
using FormatterInterface;
using Newtonsoft.Json;
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
            if (traceResult == null)
            {
                throw new ArgumentNullException(nameof(traceResult));
            }
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(traceResult));
            }
            using (var streamWriter = new StreamWriter(stream))
            {
                streamWriter.Write(GetTraceResultString(traceResult));
            }

        }

        private string GetTraceResultString(TraceResult traceResult)
        {
            return JsonConvert.SerializeObject(traceResult);
        }
    }
}
