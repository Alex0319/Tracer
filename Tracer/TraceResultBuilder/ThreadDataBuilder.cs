using System.Collections.Generic;
using Tracer.TraceResultData;

namespace Tracer.TraceResultBuilder
{
    internal class ThreadDataBuilder
    {
        private ThreadController thread;

        internal ThreadDataBuilder(ThreadController thread)
        {
            this.thread = thread;
        }

        internal TraceThreadData GetResult()
        {
            return CreateThreadDataResult();
        }

        private TraceThreadData CreateThreadDataResult()
        {
            List<TraceMethodData> childList = GetChildList();
            TraceThreadData threadData = new TraceThreadData()
            {
                ExecutionTime = thread.ExecutionTime,
                ChildMethods = childList
            };
            return threadData;
        }

        private List<TraceMethodData> GetChildList()
        {
            List<TraceMethodData> traceMethods = new List<TraceMethodData>();
            foreach (var childMethod in thread.ChildMethods)
            {
                traceMethods.Add(new MethodDataBuilder(childMethod).GetResult());
            }
            return traceMethods;
        }
    }
}
