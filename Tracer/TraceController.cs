using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Utilities.Tracer
{
    internal class TraceController
    {
        private readonly ConcurrentDictionary<int, ThreadController> _threadControllers;

        internal TraceController()
        {
            _threadControllers = new ConcurrentDictionary<int, ThreadController>();
        }

        internal ConcurrentDictionary<int, ThreadController> ThreadControllers => _threadControllers;

        internal void StartMethodTrace(int threadId, MethodBase methodBase)
        {
            ThreadController threadController = _threadControllers.GetOrAdd(threadId, new ThreadController());
            threadController.StartMethodTrace(new MethodController(methodBase));
        }

        internal void StopMethodTrace(int threadId, MethodBase methodBase)
        {
            if (!_threadControllers.TryGetValue(threadId, out var threadController))
            {
                throw new ArgumentException("There is no thread with id = " + threadId);
            }
            threadController.StopMethodTrace(new MethodController(methodBase));
        }
    }
}
