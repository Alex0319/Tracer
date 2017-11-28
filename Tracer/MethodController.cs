using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Utilities.Tracer.TraceResultData;

namespace Utilities.Tracer
{
    internal class MethodController
    {
        private readonly List<MethodController> _firstLevelChildMethods;
        private readonly MethodBase _methodBase;
        private readonly Stopwatch _stopWatch;

        internal MethodController(MethodBase methodBase)
        {
            this._methodBase = methodBase;
            _firstLevelChildMethods = new List<MethodController>();
            _stopWatch = Stopwatch.StartNew();
        }

        internal TraceMethodData TraceMethodData => new TraceMethodData()
        {
            Name = _methodBase.Name,
            ClassName = _methodBase.DeclaringType.Name,
            ParamsCount = _methodBase.GetParameters().Length,
            ExecutionTime = ExecutionTime
        };

        internal List<MethodController> ChildMethods => _firstLevelChildMethods;
        internal long ExecutionTime => _stopWatch.ElapsedMilliseconds;

        internal void AddChild(MethodController childMethod)
        {
            _firstLevelChildMethods.Add(childMethod);
        }

        internal void StopMethodTrace()
        {
            _stopWatch.Stop();
        }

        internal bool IsEquals(MethodController method)
        {
            return _methodBase == method._methodBase;
        }
    }
}
