using System.Collections.Generic;
using Tracer.TraceResultData;

namespace Tracer.TraceResultBuilder
{
    internal class MethodDataBuilder
    {
        private readonly MethodController method;

        internal MethodDataBuilder(MethodController method)
        {
            this.method = method;
        }

        internal TraceMethodData GetResult()
        {
            return CreateMethodDataResult();
        }

        private TraceMethodData CreateMethodDataResult()
        {
            TraceMethodData traceMethodData = method.TraceMethodData;
            traceMethodData.ChildMethods = GetChildList();
            return traceMethodData;
        }

        private List<TraceMethodData> GetChildList()
        {
            List<TraceMethodData> traceMethodDatas = new List<TraceMethodData>();
            foreach (var childMethod in method.ChildMethods)
            {
                traceMethodDatas.Add(new MethodDataBuilder(childMethod).GetResult());
            }
            return traceMethodDatas;
        }
    }
}
