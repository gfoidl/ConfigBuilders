using System;

namespace gfoidl.ConfigBuilders.Tests.WCF
{
    public class MyService : IMyService
    {
        public string SayHi() => $"Hi from {Environment.MachineName}";
    }
}
