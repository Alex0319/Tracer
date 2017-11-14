﻿using System.Collections.Generic;

namespace Tracer.TraceResultInfo
{
    public class TraceMethodInfo
    {
        public string Name { get; set; }
        public string ClassName { get; set; }
        public long ExecutionTime { get; set; }
        public int ParamsCount { get; set; }
        public List<TraceMethodInfo> ChilMethods { get; set; }
    }
}
