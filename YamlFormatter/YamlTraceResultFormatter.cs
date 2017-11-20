using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FormatterInterface;
using Tracer.TraceResultData;

namespace YamlFormatter
{
    public class YamlTraceResultFormatter : ITraceResultFormatter
    {
        private const string Name = "yaml";
        private readonly int Indent = 4;

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
            var threadsInfo = traceResult.ResultInfo;

            traceResultBuilder.AppendLine("---");
            traceResultBuilder.AppendLine("threads:");

            foreach (var threadInfo in threadsInfo)
            {
                threadElementBuilder.Clear();
                threadElementBuilder.Append((char)32, Indent);
                threadElementBuilder.AppendFormat("id: {0}\n", threadInfo.Key);
                threadElementBuilder.Append(FormatObjectProperties(threadInfo.Value, Indent));
                if (threadInfo.Value.ChildMethods.Count != 0)
                {
//                    FormatMethodsInfo(threadElementBuilder, threadInfo.Value.ChildMethods, nestedLevel + 2);
                }

                traceResultBuilder.AppendLine(threadElementBuilder.ToString());
            }
            traceResultBuilder.AppendLine("...");
            return traceResultBuilder.ToString();
        }

        private string FormatObjectProperties(object obj, int indent)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var property in obj.GetType().GetProperties())
            {
                stringBuilder.Append((char)32, indent);
                stringBuilder.Append(property.Name);
                stringBuilder.Append(": ");
                var value = property.GetValue(obj);
                if (value.GetType().IsPrimitive)
                    stringBuilder.Append(value);
                else
                {
                    stringBuilder.AppendLine();
                    FormatMethodsInfo(stringBuilder, (IEnumerable<TraceMethodData>)value, indent + Indent);
                }
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString();
        }


        private void FormatMethodsInfo(StringBuilder parentElement, IEnumerable<TraceMethodData> childMethods, int nestedLevel)
        {
            if (childMethods == null || !childMethods.Any())
            {
                parentElement.Append("[]");
                return;
            }
            StringBuilder childBuilder = new StringBuilder();
            foreach (var method in childMethods)
            {
                childBuilder.Clear();
                childBuilder.Append((char)32, nestedLevel);
                childBuilder.Append("- ");

                if (method.ChildMethods.Count != 0)
                {
                    childBuilder.Append(Environment.NewLine);
                    FormatMethodsInfo(childBuilder, method.ChildMethods, nestedLevel + 3);
                    childBuilder.Append((char)32, nestedLevel + 2);
                }

                parentElement.AppendLine(childBuilder.ToString());
            }
        }
    }
}

