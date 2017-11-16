using System;
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
            parser.Parse(args);
            TestClass testClass = new TestClass(tracer);
            testClass.FirstTest();
            string result = formatter.Format(tracer.GetTraceResult(), parser.GetFormat(), parser.GetOutputFilePath());
            if(result != null)
                Console.WriteLine(result);
            Console.ReadLine();
        }
     }

    internal class TestClass
    {
        private static ITracer tracer;

        public TestClass(ITracer tracer)
        {
            TestClass.tracer = tracer;
        }

        public void FirstTest()
        {
            tracer.StartTrace();
            Thread.Sleep(3);
            SecondTest();
            tracer.StopTrace();
        }

        private void SecondTest()
        {
            tracer.StartTrace();
            for (int i = 0; i < 4; i++)
                ThirdTest();
            Thread.Sleep(3);
            tracer.StopTrace();
        }

        private void ThirdTest()
        {
            tracer.StartTrace();
            for (int i = 0; i < 10; i++)
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
