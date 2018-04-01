using System;

namespace PrintPDV.Utility.Models
{
    public class Command
    {
        public Command(string name, Func<int> function)
        {
            Name = name;
            Function = function;
        }

        public string Name { get; set; }

        public Func<int> Function { get; set; }
    }
}
