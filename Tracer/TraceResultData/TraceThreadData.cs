using System.Collections.Generic;

namespace Utilities.Tracer.TraceResultData
{
    public class TraceThreadData
    {
        public long ExecutionTime { get; set; }
        public List<TraceMethodData> ChildMethods { get; set; }
    }
}
