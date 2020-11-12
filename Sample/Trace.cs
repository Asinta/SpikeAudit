using Audit.Core;

namespace AuditSpike
{
    public class Trace
    {
        public void TryAudit()
        {
            var sample = new Sample(20, "Smith", true);

            using (var audit = AuditScope.Create("SAMPLE::test", () => sample))
            {
                sample.Age = 0;
                sample.Name = "Jone";
                sample.IsValid = false;
                audit.Comment("this is a comment.");
            }
        }
    }
}