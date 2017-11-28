using System.Diagnostics;
using System.Reflection;
using System.Threading;
using Utilities.Tracer.TraceResultBuilder;
using Utilities.Tracer.TraceResultData;

namespace Utilities.Tracer
{
    public class Tracer: ITracer
    {
        private static Tracer _instance;
        private static TraceController _traceController;

        private Tracer()
        {
            _traceController = new TraceController();
        }

        public static Tracer Instance => _instance ?? (_instance = new Tracer());

        public void StartTrace()
        {
            StackFrame frame = new StackFrame(1);
            MethodBase currentMethod = frame.GetMethod();
            _traceController.StartMethodTrace(Thread.CurrentThread.ManagedThreadId, currentMethod);
        }

        public void StopTrace()
        {
            StackFrame frame = new StackFrame(1);
            MethodBase currentMethod = frame.GetMethod();
            _traceController.StopMethodTrace(Thread.CurrentThread.ManagedThreadId, currentMethod);
        }

        public TraceResult GetTraceResult()
        {
            return new TraceDataBuilder(_traceController.ThreadControllers).GetResult();
        }
    }
}
