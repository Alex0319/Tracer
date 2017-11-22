using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Tracer.TraceResultData;

namespace Tracer
{
    internal class MethodController
    {
        private readonly List<MethodController> firstLevelChildMethods;
        private readonly MethodBase methodBase;
        private readonly Stopwatch stopWatch;

        internal MethodController(MethodBase methodBase)
        {
            this.methodBase = methodBase;
            firstLevelChildMethods = new List<MethodController>();
            stopWatch = Stopwatch.StartNew();
        }

        internal TraceMethodData TraceMethodData => new TraceMethodData()
        {
            Name = methodBase.Name,
            ClassName = methodBase.DeclaringType.Name,
            ParamsCount = methodBase.GetParameters().Length,
            ExecutionTime = this.ExecutionTime
        };

        internal List<MethodController> ChildMethods => firstLevelChildMethods;
        internal long ExecutionTime => stopWatch.ElapsedMilliseconds;

        internal void AddChild(MethodController childMethod)
        {
            firstLevelChildMethods.Add(childMethod);
        }

        internal void StopMethodTrace()
        {
            stopWatch.Stop();
        }

        internal bool IsEquals(MethodController method)
        {
            return methodBase == method.methodBase;
        }
    }
}
