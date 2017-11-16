using System.IO;
using Tracer.TraceResultData;

namespace FormatterInterface
{
    public interface ITraceResultFormatter
    {
        string GetFormat();
        void Format(TraceResult traceResult, Stream stream);
    }
}
