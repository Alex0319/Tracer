using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using FormatterInterface;
using Tracer.TraceResultData;

namespace JsonFormatter
{
    public class JsonTraceResultFormatter: ITraceResultFormatter
    {
        private const string Name = "json";
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
                throw new ArgumentNullException(nameof(traceResult));
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

            traceResultBuilder.AppendLine("{");
            traceResultBuilder.Append((char)32, nestedLevel + Indent);
            traceResultBuilder.Append("\"threads\"");
            traceResultBuilder.AppendLine(" : [");
            foreach (var threadInfo in threadsInfo)
            {
                threadElementBuilder.Clear();
                threadElementBuilder.Append((char)32, nestedLevel + Indent*2);
                threadElementBuilder.AppendLine("{");
                threadElementBuilder.AppendFormat("\"id\" : {0},\n\"time\" : \"{1}ms\",\n\"ChildMethods\" : [", threadInfo.Key,
                    threadInfo.Value.ExecutionTime);
                if (threadInfo.Value.ChildMethods.Count != 0)
                {
                    threadElementBuilder.Append(Environment.NewLine);
                    FormatMethodsInfo(threadElementBuilder, threadInfo.Value.ChildMethods, nestedLevel + Indent*3);
                    threadElementBuilder.Append((char)32, nestedLevel + Indent*2);
                }
                threadElementBuilder.AppendLine("]");
                threadElementBuilder.Append((char)32, nestedLevel + Indent*2);
                threadElementBuilder.Append("},");
                traceResultBuilder.AppendLine(threadElementBuilder.ToString());
            }
            traceResultBuilder.Remove(traceResultBuilder.Length - 3, 1);
            traceResultBuilder.Append((char)32, nestedLevel + Indent);
            traceResultBuilder.AppendLine("]");
            traceResultBuilder.Append("}");
            Console.WriteLine(traceResultBuilder.ToString());
            return traceResultBuilder.ToString();
        }

        private void FormatMethodsInfo(StringBuilder parentElement, IEnumerable<TraceMethodData> childMethods, int nestedLevel)
        {
            StringBuilder childBuilder = new StringBuilder();
            foreach (var method in childMethods)
            {
                childBuilder.Clear();
                childBuilder.Append((char)32, nestedLevel);
                childBuilder.AppendLine("{");
                childBuilder.AppendFormat("\"Name\" : \"{0}\",\n\"ClassName\" : \"{1}\",\n\"ExecutionTime\" : \"{2}ms\",\n\"ParamsCount\" : {3},\n\"ChildMethods\" : [", method.Name, method.ClassName, method.ExecutionTime, method.ParamsCount);
                if (method.ChildMethods.Count != 0)
                {
                    childBuilder.Append(Environment.NewLine);
                    FormatMethodsInfo(childBuilder, method.ChildMethods, nestedLevel + Indent);
                    childBuilder.Append((char)32, nestedLevel);
                }
                childBuilder.AppendLine("]");
                childBuilder.Append((char)32, nestedLevel);
                childBuilder.Append("},");
                parentElement.AppendLine(childBuilder.ToString());
            }
            parentElement.Remove(parentElement.Length - 3, 1);
        }
    }
}
