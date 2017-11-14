using System.Collections.Generic;
using Tracer.TraceResultData;

namespace Tracer.TraceResultBuilder
{
    internal class TraceDataBuilder
    {
        private readonly IDictionary<int, ThreadController> threadsInfo;

        internal TraceDataBuilder(IDictionary<int, ThreadController> threadsInfo)
        {
            this.threadsInfo = threadsInfo;
        }

        internal TraceResult GetResult()
        {
            return new TraceResult()
            {
                ResultInfo = GetTraceResult()
            };
        }

        private Dictionary<int, TraceThreadData> GetTraceResult()
        {
            Dictionary<int, TraceThreadData> traceResult = new Dictionary<int, TraceThreadData>();
            foreach (var threadInfo in threadsInfo)
            {
                TraceThreadData traceThreadData = new ThreadDataBuilder(threadInfo.Value).GetResult();
                traceResult.Add(threadInfo.Key, traceThreadData);
            }
            return traceResult;
        }
    }
}
