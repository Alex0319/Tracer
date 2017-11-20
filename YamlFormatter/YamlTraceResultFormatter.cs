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
            var threadsInfo = traceResult.ResultInfo;

            traceResultBuilder.AppendLine("---");
            traceResultBuilder.AppendLine("threads:");

            foreach (var threadInfo in threadsInfo)
                FormatElement(traceResultBuilder, threadInfo.Value, Indent, $"id: {threadInfo.Key}\n");
            traceResultBuilder.AppendLine("...");
            return traceResultBuilder.ToString();
        }

        private void FormatElement(StringBuilder parentElement, object element, int nestedLevel, string initStr)
        {
            StringBuilder childBuilder = new StringBuilder();
            childBuilder.Append((char) 32, nestedLevel);
            childBuilder.Append(initStr);
            FormatObjectProperties(childBuilder, element, nestedLevel);
            parentElement.Append(childBuilder);
        }

        private void FormatObjectProperties(StringBuilder parentElement, object obj, int nestedLevel)
        {
            bool isAddFirstIndent = !parentElement[parentElement.Length - 2].Equals('-');
            nestedLevel = isAddFirstIndent ? nestedLevel : nestedLevel + 2;
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var property in obj.GetType().GetProperties())
            {
                if (isAddFirstIndent)
                    stringBuilder.Append((char) 32, nestedLevel);
                else
                    isAddFirstIndent = true;
                stringBuilder.Append(property.Name);
                stringBuilder.Append(": ");
                var value = property.GetValue(obj);
                if (value.GetType().IsPrimitive || value is string)
                    stringBuilder.Append(value);
                else
                {
                    stringBuilder.AppendLine();
                    FormatMethodsInfo(stringBuilder, (IEnumerable<TraceMethodData>) value, nestedLevel + Indent);
                    stringBuilder.Remove(stringBuilder.Length - 2, 2);
                }
                stringBuilder.AppendLine();
            }
            parentElement.Append(stringBuilder);
        }

        private void FormatMethodsInfo(StringBuilder parentElement, IEnumerable<TraceMethodData> childMethods,
            int nestedLevel)
        {
            if (childMethods == null || !childMethods.Any())
            {
                parentElement.Remove(parentElement.Length - 2, 2);
                parentElement.Append("[]");
                return;
            }
            foreach (var method in childMethods)
                FormatElement(parentElement, method, nestedLevel, "- ");
        }
    }
}
