using System;
using System.Threading;
using Utilities.Parser;
using Utilities.Formatter;
using Utilities.Tracer;

namespace TracerApp
{
    internal class Program
    {
        private static readonly IParser Parser = Utilities.Parser.Parser.Instance;
        private static readonly ITracer Tracer = Utilities.Tracer.Tracer.Instance;
        private static readonly Formatter Formatter = Formatter.Instance;

        static void Main(string[] args)
        {
            if (!Parser.CanBeParsed(args))
            {
                Console.WriteLine(Parser.GetArgsInfo(Formatter.GetInfo()));
            }
            else
            {
                TestClass testClass = new TestClass(Tracer);
                testClass.FirstTest();
                string result = Formatter.Format(Tracer.GetTraceResult(), Parser.GetArgumentValue("f"), Parser.GetArgumentValue("o"));
                if (result != null)
                {
                    Console.WriteLine(result);
                }
            }
            Console.WriteLine("Press any char to exit...");
            Console.ReadKey();
        }
     }

    internal class TestClass
    {
        private static ITracer _tracer;

        public TestClass(ITracer tracer)
        {
           _tracer = tracer;
        }

        public void FirstTest()
        {
            _tracer.StartTrace();
            Thread.Sleep(3);
            SecondTest();
            _tracer.StopTrace();
        }

        private void SecondTest()
        {
            _tracer.StartTrace();
            for (int i = 0; i < 4; i++)
            {
                ThirdTest();
                FourthTest();
            }
            Thread.Sleep(3);
            _tracer.StopTrace();
        }

        private void ThirdTest()
        {
            _tracer.StartTrace();
            Thread.Sleep(3);
            for (int i = 0; i < 20; i++)
            {
                FourthTest();
            }
            _tracer.StopTrace();
        }

        private void FourthTest()
        {
            _tracer.StartTrace();
            Thread.Sleep(3);
            _tracer.StopTrace();
        }
    }
}
