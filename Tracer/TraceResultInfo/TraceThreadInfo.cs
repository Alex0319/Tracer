using System.Collections.Generic;

namespace Tracer.TraceResultInfo
{
    public class TraceThreadInfo
    {
        public List<TraceMethodInfo> ChildMethods { get; set; }
        public long ExecutionTime { get; set; }
    }
}
