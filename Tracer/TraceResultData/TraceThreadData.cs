using System.Collections.Generic;

namespace Tracer.TraceResultData
{
    public class TraceThreadData
    {
        public List<TraceMethodData> ChildMethods { get; set; }
        public long ExecutionTime { get; set; }
    }
}
