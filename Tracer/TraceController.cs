using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Tracer
{
    internal class TraceController
    {
        private readonly ConcurrentDictionary<int, ThreadController> threadControllers;

        internal TraceController()
        {
            threadControllers = new ConcurrentDictionary<int, ThreadController>();
        }

        internal ConcurrentDictionary<int, ThreadController> ThreadControllers => threadControllers;

        internal void StartMethodTrace(int threadId, MethodBase methodBase)
        {
            ThreadController threadController = threadControllers.GetOrAdd((int) threadId, new ThreadController());
            threadController.StartMethodTrace(new MethodController(methodBase));
        }

        internal void StopMethodTrace(int threadId, MethodBase methodBase)
        {
            if (!threadControllers.TryGetValue(threadId, out var threadController))
            {
                throw new ArgumentException("There is no thread with id = " + threadId);
            }
            threadController.StopMethodTrace(new MethodController(methodBase));
        }
    }
}
