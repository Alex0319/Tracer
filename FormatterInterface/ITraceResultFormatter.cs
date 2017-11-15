using Tracer.TraceResultData;

namespace FormatterInterface
{
    public interface ITraceResultFormatter
    {
        string GetFormat();
        string Format(TraceResult traceResult);
    }
}
