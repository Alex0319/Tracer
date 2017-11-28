using System.Collections.Generic;

namespace Utilities.Tracer.TraceResultData
{
    public class TraceMethodData
    {
        public string Name { get; set; }
        public string ClassName { get; set; }
        public long ExecutionTime { get; set; }
        public int ParamsCount { get; set; }
        public List<TraceMethodData> ChildMethods { get; set; }
    }
}
