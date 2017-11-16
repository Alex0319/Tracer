using System.Collections.Generic;
using Tracer.TraceResultData;

namespace Tracer.TraceResultBuilder
{
    internal class MethodDataBuilder
    {
        private readonly MethodController _method;

        internal MethodDataBuilder(MethodController method)
        {
            _method = method;
        }

        internal TraceMethodData GetResult()
        {
            return CreateMethodDataResult();
        }

        private TraceMethodData CreateMethodDataResult()
        {
            TraceMethodData traceMethodData = _method.TraceMethodData;
            traceMethodData.ChildMethods = GetChildList();
            return traceMethodData;
        }

        private List<TraceMethodData> GetChildList()
        {
            List<TraceMethodData> traceMethodDatas = new List<TraceMethodData>();
            foreach (var childMethod in _method.ChildMethods)
            {
                traceMethodDatas.Add(new MethodDataBuilder(childMethod).GetResult());
            }
            return traceMethodDatas;
        }
    }
}
