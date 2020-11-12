using System;
using Newtonsoft.Json;

namespace AuditSpike
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ConfigureAudit();

            var trace = new Trace();
            trace.TryAudit();
        }
        
        private static void ConfigureAudit()
        {
            Audit.Core.Configuration.Setup()
                .UseCustomProvider(new CustomFileDataProvider(config => config
                    .DirectoryBuilder(_ => "./")
                    .FilenameBuilder(auditEvent => $"{auditEvent.EventType}_{DateTime.Now.Ticks}.json")
                    ));
        }
    }
}