using System.Collections.Generic;
using Utilities.Tracer.TraceResultData;

namespace Utilities.Tracer.TraceResultBuilder
{
    internal class TraceDataBuilder
    {
        private readonly IDictionary<int, ThreadController> _threadsInfo;

        internal TraceDataBuilder(IDictionary<int, ThreadController> threadsInfo)
        {
            this._threadsInfo = threadsInfo;
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
            foreach (var threadInfo in _threadsInfo)
            {
                TraceThreadData traceThreadData = new ThreadDataBuilder(threadInfo.Value).GetResult();
                traceResult.Add(threadInfo.Key, traceThreadData);
            }
            return traceResult;
        }
    }
}
