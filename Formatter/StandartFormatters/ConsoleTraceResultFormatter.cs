using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FormatterInterface;
using Utilities.Tracer.TraceResultData;

namespace Utilities.Formatter.StandartFormatters
{
    public class ConsoleTraceResultFormatter: ITraceResultFormatter
    {
        private const string Name = "console";
        private const int Indent = 4;

        public string GetFormat()
        {
            return Name;
        }

        public void Format(TraceResult traceResult, Stream stream)
        {
            if (traceResult == null)
            {
                throw new ArgumentNullException(nameof(traceResult));
            }
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            using (var streamWriter = new StreamWriter(stream))
            {
                streamWriter.Write(GetTraceResultString(traceResult));
            } 
        }

        private string GetTraceResultString(TraceResult traceResult)
        {
            StringBuilder traceResultBuilder = new StringBuilder();
            StringBuilder threadElementBuilder = new StringBuilder();
            int nestedLevel = 0;
            var threadsInfo = traceResult.ResultInfo;

            foreach (var threadInfo in threadsInfo)
            {
                threadElementBuilder.Clear();
                threadElementBuilder.Append("thread");
                threadElementBuilder.AppendFormat("  id=\"{0}\"  time=\"{1}ms\"", threadInfo.Key,
                    threadInfo.Value.ExecutionTime);
                threadElementBuilder.Append(Environment.NewLine);
                FormatMethodsInfo(threadElementBuilder, threadInfo.Value.ChildMethods, nestedLevel + Indent);
                threadElementBuilder.Append("thread");

                traceResultBuilder.AppendLine(threadElementBuilder.ToString());
            }
            return traceResultBuilder.ToString();
        }

        private void FormatMethodsInfo(StringBuilder parentElement, IEnumerable<TraceMethodData> childMethods, int nestedLevel)
        {
            StringBuilder childBuilder = new StringBuilder();
            foreach (var method in childMethods)
            {
                childBuilder.Clear();
                childBuilder.Append((char)32, nestedLevel);
                childBuilder.Append("method");
                childBuilder.AppendFormat("  name=\"{0}\"  time=\"{1}ms\"  class=\"{2}\"  params=\"{3}\" ", method.Name, method.ExecutionTime, method.ClassName, method.ParamsCount);
                if (method.ChildMethods.Count != 0)
                {
                    childBuilder.Append(Environment.NewLine);
                    FormatMethodsInfo(childBuilder, method.ChildMethods, nestedLevel + Indent);
                    childBuilder.Append((char)32, nestedLevel);
                }
                childBuilder.Append("method");
                parentElement.AppendLine(childBuilder.ToString());
            }
        }
    }
}
