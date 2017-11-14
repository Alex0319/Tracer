using System;
using System.CodeDom.Compiler;
using Tracer;
using Parser;

namespace TracerApp
{
    class Program
    {
        private static readonly ITracer tracer = Tracer.Tracer.Instance;
        private static readonly IParser parser = Parser.Parser.Instance;

           
        static void Main(string[] args)
        {
            parser.Parse(args);
            tracer.StartTrace();
            Console.ReadLine();
        }
    }
}
