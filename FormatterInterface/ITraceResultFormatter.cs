using System.IO;
using Utilities.Tracer.TraceResultData;

namespace FormatterInterface
{
    public interface ITraceResultFormatter
    {
        string GetFormat();
        void Format(TraceResult traceResult, Stream stream);
    }
}
