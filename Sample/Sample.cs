namespace AuditSpike
{
    public class Sample
    {
        public int Age { get; set; }
        
        [Custom]
        public string Name { get; set; }
        
        public bool IsValid { get; set; }

        public Sample(int age, string name, bool isValid)
        {
            Age = age;
            Name = name;
            IsValid = isValid;
        }
    }
}