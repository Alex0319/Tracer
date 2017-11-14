using System;
using System.Collections.Generic;
using System.Linq;

namespace Tracer
{
    internal class ThreadController
    {
        private readonly List<MethodController> firstNestedTracedMethods;
        private readonly Stack<MethodController> methodsCallStack;

        internal ThreadController()
        {
            firstNestedTracedMethods = new List<MethodController>();
            methodsCallStack = new Stack<MethodController>();
        }

        internal long ExecutionTime => firstNestedTracedMethods.Sum(method => method.ExecutionTime);
        internal List<MethodController> ChildMethods => firstNestedTracedMethods;

        internal void StartMethodTrace(MethodController method)
        {
            if (methodsCallStack.Count == 0)
            {
                firstNestedTracedMethods.Add(method);
            }
            else
            {
                MethodController lastAddedMethod = methodsCallStack.Peek();
                lastAddedMethod.AddChild(method);
            }
            methodsCallStack.Push(method);
        }

        internal void StopMethodTrace(MethodController method)
        {
            if (methodsCallStack.Count == 0)
            {
                throw new InvalidOperationException("There is no method in stack");
            }
            if (!methodsCallStack.Peek().IsEquals(method))
            {
                throw new ArgumentException("StartTrace and StopTrace can't be callled from different methods");
            }
            methodsCallStack.Pop().StopMethodTrace();
        }

    }
}
