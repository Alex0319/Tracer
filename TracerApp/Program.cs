using System;
using System.Globalization;
using System.Threading;
using Parser;
using Tracer;

namespace TracerApp
{
    internal class Program
    {
        private static readonly IParser parser = Parser.Parser.Instance;
        private static readonly ITracer tracer = Tracer.Tracer.Instance;
        private static readonly Formatter.Formatter formatter = Formatter.Formatter.Instance;

        static void Main(string[] args)
        {
            if (!parser.CanBeParsed(args))
            {
                Console.WriteLine(parser.GetArgsInfo(formatter.GetInfo()));
            }
            else
            {
                TestClass testClass = new TestClass(tracer);
                testClass.FirstTest();
                string result = formatter.Format(tracer.GetTraceResult(), parser.GetArgumentValue("f"), parser.GetArgumentValue("o"));
                if (result != null)
                    Console.WriteLine(result);
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
            FourthTest();
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
