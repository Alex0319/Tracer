using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using Tracer.TraceResultBuilder;
using Tracer.TraceResultData;

namespace Tracer
{
    public class Tracer: ITracer
    {
        private static Tracer instance;
        private static TraceController traceController;

        private Tracer()
        {
            traceController = new TraceController();
        }

        public static Tracer Instance => instance ?? (instance = new Tracer());

        public void StartTrace()
        {
            StackFrame frame = new StackFrame(1);
            MethodBase currentMethod = frame.GetMethod();
            traceController.StartMethodTrace(Thread.CurrentThread.ManagedThreadId, currentMethod);
        }

        public void StopTrace()
        {
            StackFrame frame = new StackFrame(1);
            MethodBase currentMethod = frame.GetMethod();
            traceController.StopMethodTrace(Thread.CurrentThread.ManagedThreadId, currentMethod);
        }

        public TraceResult GetTraceResult()
        {
            return new TraceDataBuilder(traceController.ThreadControllers).GetResult();
        }
    }
}
