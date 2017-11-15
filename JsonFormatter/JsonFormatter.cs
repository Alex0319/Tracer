using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormatterInterface;
using Newtonsoft.Json;
using Tracer.TraceResultData;

namespace JsonFormatter
{
    public class JsonFormatter: ITraceResultFormatter
    {
        private const string name = "json";

        public string GetFormat()
        {
            return name;
        }

        public string Format(TraceResult traceResult)
        {
            return JsonConvert.SerializeObject(traceResult);
        }
    }
}
