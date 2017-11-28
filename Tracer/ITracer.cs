using Utilities.Tracer.TraceResultData;

namespace Utilities.Tracer
{
    public interface ITracer
    {
        void StartTrace();
        void StopTrace();
        TraceResult GetTraceResult();
    }
}
