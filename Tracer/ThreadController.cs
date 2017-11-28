using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities.Tracer
{
    internal class ThreadController
    {
        private readonly List<MethodController> _firstNestedTracedMethods;
        private readonly Stack<MethodController> _methodsCallStack;

        internal ThreadController()
        {
            _firstNestedTracedMethods = new List<MethodController>();
            _methodsCallStack = new Stack<MethodController>();
        }

        internal long ExecutionTime => _firstNestedTracedMethods.Sum(method => method.ExecutionTime);
        internal List<MethodController> ChildMethods => _firstNestedTracedMethods;

        internal void StartMethodTrace(MethodController method)
        {
            if (_methodsCallStack.Count == 0)
            {
                _firstNestedTracedMethods.Add(method);
            }
            else
            {
                MethodController lastAddedMethod = _methodsCallStack.Peek();
                lastAddedMethod.AddChild(method);
            }
            _methodsCallStack.Push(method);
        }

        internal void StopMethodTrace(MethodController method)
        {
            if (_methodsCallStack.Count == 0)
            {
                throw new InvalidOperationException("There is no method in stack");
            }
            if (!_methodsCallStack.Peek().IsEquals(method))
            {
                throw new ArgumentException("StartTrace and StopTrace can't be callled from different methods");
            }
            _methodsCallStack.Pop().StopMethodTrace();
        }

    }
}
