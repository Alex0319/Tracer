using System;
using System.Threading;
using Tracer;
using Parser;

namespace TracerApp
{
    class Program
    {
        private static readonly IParser parser = Parser.Parser.Instance;
          
        static void Main(string[] args)
        {
            parser.Parse(args);
            TestClass testClass = new TestClass();
            Console.WriteLine(testClass.FirstTest());
            Console.ReadLine();
        }
    }

    class TestClass
    {
        private static readonly ITracer tracer = Tracer.Tracer.Instance;

        public string FirstTest()
        {
            tracer.StartTrace();
            Thread.Sleep(3);
            SecondTest();
            tracer.StopTrace();
            return tracer.GetTraceResult().ToString();
        }

        private void SecondTest()
        {
            tracer.StartTrace();
            ThirdTest();
            Thread.Sleep(3);
            tracer.StopTrace();
        }

        private void ThirdTest()
        {
            tracer.StartTrace();
            FourthTest();
            Thread.Sleep(3);
            tracer.StopTrace();
        }

        private void FourthTest()
        {
            tracer.StartTrace();
            Thread.Sleep(3);
            tracer.StopTrace();
        }
    }
}
